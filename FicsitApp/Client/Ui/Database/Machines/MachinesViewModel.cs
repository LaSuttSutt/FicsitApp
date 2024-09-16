using Client.Shared.View;
using Client.Ui.Database.Machines.MachinesList;

namespace Client.Ui.Database.Machines;

public class MachinesViewModel : NavigationViewModel
{
    public MachinesListViewModel ListViewModel { get; } = new();
    
    public MachinesViewModel()
    {
        Title = "Machines";
    }
}