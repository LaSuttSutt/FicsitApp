using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning;

public class PlanningViewModel : ViewModelBase
{
    #region Declarations

    public List<ItemListModel> Items { get; }
    public List<RecipeListModel> Recipes { get; set; } = [];
    private decimal _machineCount = 1;

    public decimal MachineCount
    {
        get => _machineCount;
        set
        {
            _machineCount = value;
            RecalculateAmount();
        }
    }

    private decimal _workload = 100;

    public decimal Workload
    {
        get => _workload;
        set
        {
            _workload = value;
            RecalculateAmount();
        }
    }

    public decimal ItemAmount { get; set; }

    private ItemListModel? _selectedItem;

    public ItemListModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            SelectedItemChanged();
        }
    }

    private RecipeListModel? _selectedRecipe;

    public RecipeListModel? SelectedRecipe
    {
        get => _selectedRecipe;
        set
        {
            _selectedRecipe = value;
            SelectedRecipeChanged();
        }
    }

    public ObservableCollection<CalculationEntryViewModel> NeededParts { get; } = [];

    #endregion

    #region C'tors

    public PlanningViewModel()
    {
        Items = DataAccess.GetEntities<Item>(i => !i.IsResource).ToItemList();
        SelectedItem = Items.First();
    }

    #endregion

    #region Methods

    private void SelectedItemChanged()
    {
        if (SelectedItem == null) return;

        Recipes = DataAccess.GetEntities<Recipe>(r => r.ItemId == SelectedItem.Item.Id).ToRecipeList();
        this.RaisePropertyChanged(nameof(Recipes));

        SelectedRecipe = Recipes.First();
        this.RaisePropertyChanged(nameof(SelectedRecipe));
    }

    private void SelectedRecipeChanged()
    {
        if (SelectedRecipe == null) return;

        MachineCount = 1;
        Workload = 100;
        ItemAmount = SelectedRecipe.Recipe.Amount;
        RecalculateNeededParts();

        this.RaisePropertyChanged(nameof(MachineCount));
        this.RaisePropertyChanged(nameof(Workload));
        this.RaisePropertyChanged(nameof(ItemAmount));
    }

    private void RecalculateAmount()
    {
        if (SelectedRecipe == null) return;
        ItemAmount = Math.Round(MachineCount * SelectedRecipe.Recipe.Amount * Workload / 100.0m, 2,
            MidpointRounding.AwayFromZero);
        this.RaisePropertyChanged(nameof(ItemAmount));
    }

    private void RecalculateNeededParts()
    {
        if (SelectedItem == null || SelectedRecipe == null) return;
        NeededParts.Clear();
        var ingredients = DataAccess.GetEntities<Ingredient>(i => i.RecipeId == SelectedRecipe.Recipe.Id);
        ingredients.ForEach(i =>
        {
            var item = DataAccess.GetEntity<Item>(i.ItemId);
            if (item == null) throw new Exception("Item not found");
            NeededParts.Add(new CalculationEntryViewModel(item));
        });
    }

    #endregion
}