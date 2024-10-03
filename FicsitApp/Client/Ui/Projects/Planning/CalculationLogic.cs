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
    }

    private void SubItemRecipeChanged(CalculationEntryViewModel subItem)
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