using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Client.Shared.DomainModel;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning;

public class CalculationLogic
{
    public ObservableCollection<ItemEntryViewModel> SubItems { get; } = [];
    public ObservableCollection<ResourceEntryViewModel> Resources { get; } = [];
    private Dictionary<ItemEntryViewModel, List<ItemEntryViewModel>> UsageMapping { get; } = [];
    private Dictionary<ResourceEntryViewModel, List<ItemEntryViewModel>> ResourceMapping { get; } = [];

    public void InitialCalculation(ObservableCollection<ItemEntryViewModel> mainItems)
    {
        SubItems.Clear();
        Resources.Clear();
        UsageMapping.Clear();
        ResourceMapping.Clear();
        foreach (var mainItem in mainItems)
            FindIngredientsForItem(mainItem); 
        RecalculateRequiredItems();
    }

    public void SubItemRecipeChanged(ItemEntryViewModel subItem)
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

        foreach (var item in ResourceMapping.Keys)
        {
            var neededAmount = 0m;
            
            foreach (var masterItem in ResourceMapping[item])
            {
                if (masterItem.SelectedRecipe == null) continue;
                var ingredient = DataAccess.GetEntity<Ingredient>(i =>
                    i.RecipeId == masterItem.SelectedRecipe.Recipe.Id && i.ItemId == item.ResourceItem.Item.Id);

                var amount = Math.Round(ingredient.Amount * masterItem.MachineCount * masterItem.Workload / 100, 2,
                    MidpointRounding.AwayFromZero);
                neededAmount += amount;
            }
            
            item.SetRequired(neededAmount);
        }
    }

    private void FindIngredientsForItem(ItemEntryViewModel item)
    {
        if (item.SelectedRecipe == null) return;
        var ingredients = DataAccess.GetEntities<Ingredient>(i => i.RecipeId == item.SelectedRecipe.Recipe.Id);
        var newEntrees = new List<ItemEntryViewModel>();

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
            if (subItem == null)
                continue;

            if (subItem.IsResource)
            {
                var newResource = new ResourceEntryViewModel(subItem.ToItemListModel());
                Resources.Add(newResource);
                ResourceMapping.Add(newResource, [item]);
                continue;
            }

            var newItem = new ItemEntryViewModel(subItem);
            newItem.SelectedRecipeChanged += (_, entry) => SubItemRecipeChanged(entry);
            newItem.RecalculationNeeded += (_, _) => RecalculateRequiredItems();
            UsageMapping.Add(newItem, [item]);
            newEntrees.Add(newItem);
            SubItems.Add(newItem);
        }

        // then add the sub/sub items
        newEntrees.ForEach(FindIngredientsForItem);
    }

    private void RemoveUsage(ItemEntryViewModel item)
    {
        List<ItemEntryViewModel> itemsToCheck = [];
        
        foreach (var keyValuePair in UsageMapping)
        {
            if (keyValuePair.Value.Remove(item))
                itemsToCheck.Add(keyValuePair.Key);
        }

        foreach (var keyValuePair in ResourceMapping)
            keyValuePair.Value.Remove(item);

        foreach (var itemToCheck in itemsToCheck)
        {
            if (UsageMapping[itemToCheck].Count > 0)
                continue;

            RemoveUsage(itemToCheck);
        }
    }
}