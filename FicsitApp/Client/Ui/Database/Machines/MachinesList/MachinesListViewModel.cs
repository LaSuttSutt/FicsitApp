using System.Collections.ObjectModel;
using Client.Shared.View;
using Shared.TestData;

namespace Client.Ui.Database.Machines.MachinesList;

public class MachinesListViewModel : ViewModelBase
{
    public ObservableCollection<MachinesEntryViewModel> Machines { get; set; } = [];

    public MachinesListViewModel()
    {
        foreach (var machine in ItemDatabase.Machines)
        {
            Machines.Add(new MachinesEntryViewModel(machine.Id));
        }
    }
}