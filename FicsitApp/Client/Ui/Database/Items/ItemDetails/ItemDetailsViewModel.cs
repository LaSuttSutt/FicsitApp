using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Database.Recipes;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Items.ItemDetails;

public class ItemDetailsViewModel : ViewModelBase
{
    #region Declarations
    
    private Guid _itemId = Guid.Empty;
    public ICommand AddRecipeCommand { get; }
    
    public ReactiveCommand<RecipeDetailViewModel, Unit> OnEditRecipe { get; }
    public ReactiveCommand<RecipeDetailViewModel, Unit> OnDeleteRecipe { get; }
    
    public Interaction<CreateRecipeViewModel, ShowDialogResult> ShowDialog { get; }

    public Guid ItemId
    {
        get => _itemId;
        set
        {
            _itemId = value;
            this.RaisePropertyChanged(nameof(ItemIdNotEmpty));
            LoadItem();
        }
    }
    
    public bool ItemIdNotEmpty => ItemId != Guid.Empty;
    
    public string Name { get; set; } = string.Empty;
    public Bitmap Image { get; set; } = ImageHelper.DefaultImage;
    public bool IsResource { get; set; }
    
    public ObservableCollection<RecipeDetailViewModel> Recipes { get; set; } = [];

    #endregion
    
    #region Constructors
    
    public ItemDetailsViewModel()
    {
        OnEditRecipe = ReactiveCommand.Create<RecipeDetailViewModel>(EditRecipe);
        OnDeleteRecipe = ReactiveCommand.Create<RecipeDetailViewModel>(DeleteRecipe);
        
        ShowDialog = new Interaction<CreateRecipeViewModel, ShowDialogResult>();
        AddRecipeCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var recipe = new Recipe
            {
                ItemId = ItemId
            };

            var viewModel = new CreateRecipeViewModel(recipe);
            var result = await ShowDialog.Handle(viewModel);
            
            if (result.Result == DialogResult.Ok)
            {
                DataAccess.AddEntity(recipe);
                Recipes.Add(new RecipeDetailViewModel(recipe.Id));
            }
        });
    }
    
    #endregion
    
    #region Methods
    
    private void LoadItem()
    {
        var item = DataAccess.GetEntity<Item>(ItemId);
        if (item == null)
        {
            RefreshView("<Item not found>", true);
            return;
        }
        
        RefreshView(item.Name, item.IsResource, item.Image());
        LoadRecipes();
    }
    
    private void LoadRecipes()
    {
        Recipes.Clear();

        foreach (var recipe in DataAccess.GetEntities<Recipe>(r => r.ItemId == ItemId))
        {
            Recipes.Add(new RecipeDetailViewModel(recipe.Id));
        }
    }

    private void RefreshView(string name, bool isResource, Bitmap? image = null)
    {
        Name = name;
        IsResource = isResource;
        Image = image ?? ImageHelper.DefaultImage;
        
        this.RaisePropertyChanged(nameof(Name));
        this.RaisePropertyChanged(nameof(Image));
        this.RaisePropertyChanged(nameof(IsResource));
    }
    
    private async void EditRecipe(RecipeDetailViewModel e)
    {
        var recipe = DataAccess.GetEntity<Recipe>(e.RecipeId);
        if (recipe == null) return;

        var recipeClone = recipe.Clone();
        var viewModel = new CreateRecipeViewModel(recipeClone);
        var result = await ShowDialog.Handle(viewModel);

        if (result.Result != DialogResult.Ok) return;

        recipe.Update(recipeClone);
        DataAccess.UpdateEntity(recipe);
        var model = Recipes.FirstOrDefault(r => r.RecipeId == recipe.Id);
        model?.Reload(recipe.Id);
    }

    private void DeleteRecipe(RecipeDetailViewModel e)
    {
        var recipe = DataAccess.GetEntity<Recipe>(e.RecipeId);
        if (recipe == null) return;

        var model = Recipes.FirstOrDefault(r => r.RecipeId == recipe.Id);
        if (model == null) return;

        Recipes.Remove(model);

        // Delete recipes & ingredients before item
        var ingredients = DataAccess.GetEntities<Ingredient>(i => i.RecipeId == recipe.Id);
        DataAccess.DeleteEntities(ingredients);
        DataAccess.DeleteEntity(recipe);
    }
    
    // ReSharper disable once CollectionNeverQueried.Global
    public static readonly List<Recipe> TestRecipes = [new Recipe(), new Recipe()];
    
    #endregion
}