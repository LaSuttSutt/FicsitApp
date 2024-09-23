using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Database.Machines.MachinesList;
using Client.Ui.Database.Recipes;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;
using Shared.TestData;

namespace Client.Ui.Database.Items.ItemDetails;

public class ItemDetailsViewModel : ViewModelBase
{
    private Guid _itemId = Guid.Empty;
    public ICommand AddRecipeCommand { get; }
    
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

    public ItemDetailsViewModel()
    {
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

        foreach (var recipe in ItemDatabase.Recipes.Where(r => r.ItemId == ItemId))
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
    
    // ReSharper disable once CollectionNeverQueried.Global
    public static readonly List<Recipe> TestRecipes = [new Recipe(), new Recipe()];
}