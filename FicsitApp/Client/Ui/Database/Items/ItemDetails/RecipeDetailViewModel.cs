using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Items.ItemDetails;

public class RecipeDetailViewModel : ViewModelBase
{
    public Guid RecipeId { get; private set; }
    public string RecipeName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public Bitmap Image { get; set; } = ImageHelper.DefaultImage;
    public decimal AmountPerMinute { get; set; }
    public Bitmap MachineImage { get; set; } = ImageHelper.DefaultMachine;

    public ObservableCollection<RecipeDetailViewEntry> Ingredients { get; set; } = [];
    public ObservableCollection<RecipeDetailViewEntry> ByProducts { get; set; } = [];
    
    public bool HasByProducts => ByProducts.Count > 0;
    public bool HasIngredients => Ingredients.Count > 0;
    
    public RecipeDetailViewModel(Guid recipeId)
    {
        Reload(recipeId);
    }

    public void Reload(Guid recipeId)
    {
        Ingredients.Clear();
        ByProducts.Clear();
        RecipeId = recipeId;
        LoadRecipe();
        LoadIngredients();
    }
    
    private void LoadRecipe()
    {
        var recipe = DataAccess.GetEntity<Recipe>(RecipeId);
        if (recipe == null)
        {
            RefreshView("<Recipe not found>", "<Item not found>", 0);
            return;
        }

        var item = DataAccess.GetEntity<Item>(recipe.ItemId);
        if (item == null)
        {
            RefreshView(recipe.Name, "<Item not found>", 0);
            return;
        }
        
        var machine = DataAccess.GetEntity<Machine>(recipe.MachineId);
        MachineImage = machine?.Image() ?? ImageHelper.DefaultMachine;

        RefreshView(recipe.Name, item.Name, recipe.Amount, item.Image());
    }

    private void LoadIngredients()
    {
        var ingredients = DataAccess.GetEntities<Ingredient>(
            i => i.RecipeId == RecipeId && !i.IsByProduct);
        
        var byProducts = DataAccess.GetEntities<Ingredient>(
            i => i.RecipeId == RecipeId && i.IsByProduct);
        
        BuildIngredientsViewModel(ingredients, Ingredients);
        BuildIngredientsViewModel(byProducts, ByProducts);
    }

    private void BuildIngredientsViewModel(
        IEnumerable<Ingredient> ingredients, 
        ObservableCollection<RecipeDetailViewEntry> viewModelList)
    {
        foreach (var ingredient in ingredients)
        {
            var item = DataAccess.GetEntity<Item>(ingredient.ItemId);
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
        this.RaisePropertyChanged(nameof(MachineImage));
    }
}