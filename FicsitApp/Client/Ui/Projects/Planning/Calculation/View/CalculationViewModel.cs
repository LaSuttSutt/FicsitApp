using System.Collections.ObjectModel;
using Avalonia.Controls;
using Client.Shared.View;
using Client.Ui.Projects.Planning.Calculation.DataObjects;

namespace Client.Ui.Projects.Planning.Calculation.View;

public class CalculationViewModel : ViewModelBase
{
    private CalculationLogic CalculationLogic { get; set; } = new();

    public ObservableCollection<CalculationItem> NewItems => CalculationLogic.CalculationItems;
    
    public CalculationViewModel()
    {
    }
}