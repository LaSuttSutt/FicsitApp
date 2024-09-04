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

namespace Client.Ui.Database;

public class ItemDetailsViewModel : ViewModelBase
{
    private Guid _itemId = Guid.Empty;

    public Guid ItemId
    {
        get => _itemId;
        set
        {
            _itemId = value;
            LoadItem();
        }
    }
    
    public string Name { get; set; } = string.Empty;
    public Bitmap Image { get; set; } = ImageHelper.DefaultImage;
    public bool IsResource { get; set; }
    
    public ObservableCollection<RecipeDetailViewModel> Recipes { get; set; } = [];

    private void LoadItem()
    {
        var item = ItemDatabase.Items.FirstOrDefault(i => i.Id == ItemId);
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