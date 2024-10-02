using System;
using System.Collections.Generic;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning;

public class CalculationEntryViewModel : ViewModelBase
{
    #region Declarations
    
    public event EventHandler? EntryHasChanged;
    public ItemListModel ItemModel { get; set; }
    public List<RecipeListModel> Recipes { get; set; } = [];
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

    private decimal _machineCount;
    public decimal MachineCount
    {
        get => _machineCount;
        set
        {
            _machineCount = value;
            CalculateAmount();
        }
    }

    private decimal _workload;
    public decimal Workload 
    { 
        get => _workload;
        set
        {
            _workload = value;
            CalculateAmount();
        } 
    }

    private decimal _requirement;
    public decimal Requirement
    {
        get => _requirement;
        set
        {
            _requirement = value;
            Difference = value - _requirement;
        }
    }

    public decimal Amount { get; set; }
    public decimal Difference { get; set; }

    #endregion
    
    #region C'tor

    public CalculationEntryViewModel()
    {
        ItemModel = null!;
        Recipes = null!;
        _selectedRecipe = null!;
    }

    public CalculationEntryViewModel(Item item)
    {
        ItemModel = item.ToItemListModel();
        if (item.IsResource) return;
        
        Recipes = DataAccess.GetEntities<Recipe>(r => r.ItemId == ItemModel.Item.Id).ToRecipeList();
        _selectedRecipe = Recipes[0];
    }
    
    #endregion
    
    #region Private Methods

    private void SelectedRecipeChanged()
    {
        if (SelectedRecipe == null) return;
        _machineCount = 1;
        _workload = 100;
        Amount = SelectedRecipe.Recipe.Amount;
        Difference = Requirement - Amount;
        RaiseEntryHasChanged();
    }
    
    private void CalculateAmount()
    {
        Amount = Math.Round(MachineCount * SelectedRecipe.Recipe.Amount * Workload / 100.0m, 2,
            MidpointRounding.AwayFromZero);
        this.RaisePropertyChanged(nameof(Amount));
    }

    private void RaiseEntryHasChanged()
    {
        EntryHasChanged?.Invoke(this, EventArgs.Empty);
    }
    
    #endregion
}