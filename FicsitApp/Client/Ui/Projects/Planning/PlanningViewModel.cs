using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Shared.View;
using ReactiveUI;

namespace Client.Ui.Projects.Planning;

public class PlanningViewModel : ViewModelBase
{
    #region Declarations

    private CalculationLogic CalculationLogic { get; } = new();
    public ObservableCollection<ItemEntryViewModel> MainItems { get; } = [];
    public ObservableCollection<ItemEntryViewModel> NeededParts => CalculationLogic.SubItems;
    public ObservableCollection<ResourceEntryViewModel> Resources => CalculationLogic.Resources;
    public ICommand AddMainItemCommand { get; }

    #endregion

    #region C'tors

    public PlanningViewModel()
    {
        AddMainItemCommand = ReactiveCommand.Create(AddMainItem);
    }

    #endregion
    
    #region Methods

    private void AddMainItem()
    {
        var newItem = new ItemEntryViewModel();
        newItem.SelectedItemChanged += (_, _) => CalculationLogic.InitialCalculation(MainItems);
        newItem.SelectedRecipeChanged += (_, item) => CalculationLogic.SubItemRecipeChanged(item);
        newItem.RecalculationNeeded += (_, _) => CalculationLogic.RecalculateRequiredItems();
        newItem.DeleteMainItemClicked += (_, item) =>
        {
            MainItems.Remove(item);
            CalculationLogic.InitialCalculation(MainItems);
        };
        MainItems.Add(newItem);
        CalculationLogic.InitialCalculation(MainItems);
    }
    
    #endregion
}