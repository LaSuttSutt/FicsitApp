using System.Collections.ObjectModel;
using Client.Ui.Projects.Planning.Calculation.DataObjects;

namespace Client.Ui.Projects.Planning.Calculation;

public class CalculationLogic
{
    public ObservableCollection<CalculationItem> CalculationItems { get; set; }
    public ObservableCollection<CalculationItem> NeededItems { get; set; }
    public ObservableCollection<CalculationResource> NeededResources { get; set; }

    public void InitialCalculation()
    {
        NeededItems.Clear();
        NeededResources.Clear();

        foreach (var calculationItem in CalculationItems)
        {
            
        }
    }
}