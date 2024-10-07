using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Shared.View;
using ReactiveUI;

namespace Client.Ui.Projects.Planning;

public class PlanningViewModel : ViewModelBase
{
    #region Declarations

    private CalculationLogic CalculationLogic { get; } = new();
    public ObservableCollection<CalculationEntryViewModel> MainItems { get; } = [];
    public ObservableCollection<CalculationEntryViewModel> NeededParts => CalculationLogic.SubItems;
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
        var newItem = new CalculationEntryViewModel();
        newItem.SelectedItemChanged += (s, item) => CalculationLogic.InitialCalculation(MainItems);
        newItem.SelectedRecipeChanged += (s, item) => CalculationLogic.SubItemRecipeChanged(item);
        newItem.RecalculationNeeded += (s, item) => CalculationLogic.RecalculateRequiredItems();
        newItem.DeleteMainItemClicked += (s, item) =>
        {
            MainItems.Remove(item);
            CalculationLogic.InitialCalculation(MainItems);
        };
        MainItems.Add(newItem);
        CalculationLogic.InitialCalculation(MainItems);
    }
    
    #endregion
}