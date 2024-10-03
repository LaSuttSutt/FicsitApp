using System.Collections.ObjectModel;
using Client.Shared.View;

namespace Client.Ui.Projects.Planning;

public class PlanningViewModel : ViewModelBase
{
    #region Declarations

    private CalculationLogic CalculationLogic { get; } = new();

    public CalculationEntryViewModel MainItemViewModel { get; } = new();
    public ObservableCollection<CalculationEntryViewModel> NeededParts => CalculationLogic.SubItems;

    #endregion

    #region C'tors

    public PlanningViewModel()
    {
        CalculationLogic.InitialCalculation(MainItemViewModel);
        MainItemViewModel.SelectedItemChanged += (s, item) => CalculationLogic.InitialCalculation(MainItemViewModel);
    }

    #endregion
}