using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DomainModel;
using Shared.TestData;

namespace Client.Ui.Database.Items.ItemDetails;

public class RecipeDetailViewModel : ViewModelBase
{
    public string RecipeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Bitmap Image { get; set; } = ImageHelper.DefaultImage;
    public decimal AmountPerMinute { get; set; }

    public ObservableCollection<RecipeDetailViewEntry> Ingredients { get; set; } = [];
    public ObservableCollection<RecipeDetailViewEntry> ByProducts { get; set; } = [];
    
    public bool HasByProducts => ByProducts.Count > 0;
    
    public RecipeDetailViewModel(Guid recipeId)
    {
        LoadRecipe(recipeId);
        LoadIngredients(recipeId);
    }

    private void LoadRecipe(Guid id)
    {
        var recipe = ItemDatabase.Recipes.FirstOrDefault(r => r.Id == id);
        if (recipe == null)
        {
            RefreshView("<Recipe not found>", "<Item not found>", 0);
            return;
        }

        var item = ItemDatabase.Items.FirstOrDefault(i => i.Id == recipe.ItemId);
        if (item == null)
        {
            RefreshView(recipe.Name, "<Item not found>", 0);
            return;
        }

        RefreshView(recipe.Name, item.Name, recipe.Amount, item.Image());
    }

    private void LoadIngredients(Guid recipeId)
    {
        var ingredients = ItemDatabase.Ingredients.Where
            (i => i.RecipeId == recipeId && !i.IsByProduct);
        
        var byProducts = ItemDatabase.Ingredients.Where
            (i => i.RecipeId == recipeId && i.IsByProduct);
        
        BuildIngredientsViewModel(ingredients, Ingredients);
        BuildIngredientsViewModel(byProducts, ByProducts);
    }

    private void BuildIngredientsViewModel(
        IEnumerable<Ingredient> ingredients, 
        ObservableCollection<RecipeDetailViewEntry> viewModelList)
    {
        foreach (var ingredient in ingredients)
        {
            var item = ItemDatabase.Items.FirstOrDefault(i => i.Id == ingredient.ItemId);
            if (item == null)
                continue;

            viewModelList.Add(new()
            {
                Image = item.Image(),
                ItemName = item.Name,
                AmountPerMinute = ingredient.Amount
            });
        }
    }

    private void RefreshView(string recipeName, string itemName, decimal amountPerMinute, Bitmap? image = null)
    {
        RecipeName = recipeName;
        Name = itemName;
        AmountPerMinute = amountPerMinute;
        Image = image ?? ImageHelper.DefaultImage;

        this.RaisePropertyChanged(nameof(RecipeName));
        this.RaisePropertyChanged(nameof(Name));
        this.RaisePropertyChanged(nameof(AmountPerMinute));
        this.RaisePropertyChanged(nameof(Image));
    }
}