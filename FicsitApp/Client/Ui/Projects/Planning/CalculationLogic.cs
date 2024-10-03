using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning;

public class CalculationLogic
{
    public ObservableCollection<CalculationEntryViewModel> SubItems { get; } = [];
    private Dictionary<CalculationEntryViewModel, List<CalculationEntryViewModel>> UsageMapping { get; } = [];

    public void InitialCalculation(CalculationEntryViewModel mainItem)
    {
        SubItems.Clear();
        FindIngredientsForItem(mainItem);
        RecalculateRequiredItems();
    }

    public void SubItemRecipeChanged(CalculationEntryViewModel subItem)
    {
        if (subItem.SelectedRecipe == null) return;
        RemoveUsage(subItem);
        FindIngredientsForItem(subItem);
        var itemsToDelete = UsageMapping.Where(i => i.Value.Count == 0).Select(i => i.Key);
        foreach (var itemToDelete in itemsToDelete)
        {
            SubItems.Remove(itemToDelete);
            UsageMapping.Remove(itemToDelete);
        }
        RecalculateRequiredItems();
    }
    
    public void RecalculateRequiredItems()
    {
        foreach (var item in UsageMapping.Keys)
        {
            if (item.SelectedItem == null) continue;
            var neededAmount = 0m;
            
            foreach (var masterItem in UsageMapping[item])
            {
                if (masterItem.SelectedRecipe == null) continue;
                var ingredient = DataAccess.GetEntity<Ingredient>(i =>
                    i.RecipeId == masterItem.SelectedRecipe.Recipe.Id && i.ItemId == item.SelectedItem.Item.Id);

                var amount = Math.Round(ingredient.Amount * masterItem.MachineCount * masterItem.Workload / 100, 2,
                    MidpointRounding.AwayFromZero);
                neededAmount += amount;
            }
            
            item.SetRequirement(neededAmount);
        }
    }

    private void FindIngredientsForItem(CalculationEntryViewModel item)
    {
        if (item.SelectedRecipe == null) return;
        var ingredients = DataAccess.GetEntities<Ingredient>(i => i.RecipeId == item.SelectedRecipe.Recipe.Id);
        var newEntrees = new List<CalculationEntryViewModel>();

        // First add the sub items
        foreach (var ingredient in ingredients)
        {
            var exitingItem = SubItems.FirstOrDefault(i => i.SelectedItem?.Item.Id == ingredient.ItemId);
            if (exitingItem != null)
            {
                UsageMapping[exitingItem].Add(item);
                continue;
            }

            var subItem = DataAccess.GetEntity<Item>(ingredient.ItemId);
            if (subItem == null || subItem.IsResource)
                continue;

            var newItem = new CalculationEntryViewModel(subItem);
            newItem.SelectedRecipeChanged += (s, entry) => SubItemRecipeChanged(entry);
            newItem.RecalculationNeeded += (s, entry) => RecalculateRequiredItems();
            UsageMapping.Add(newItem, [item]);
            newEntrees.Add(newItem);
            SubItems.Add(newItem);
        }

        // then add the sub/sub items
        newEntrees.ForEach(FindIngredientsForItem);
    }

    private void RemoveUsage(CalculationEntryViewModel item)
    {
        List<CalculationEntryViewModel> itemsToCheck = [];
        foreach (var keyValuePair in UsageMapping)
        {
            if (keyValuePair.Value.Remove(item))
                itemsToCheck.Add(keyValuePair.Key);
        }

        foreach (var itemToCheck in itemsToCheck)
        {
            if (UsageMapping[itemToCheck].Count > 0)
                continue;

            RemoveUsage(itemToCheck);
        }
    }
}