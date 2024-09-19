using System;
using System.Collections.ObjectModel;
using System.Reactive;
using Client.Shared.View;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Machines.MachinesList;

public class MachinesListViewModel : ViewModelBase
{
    public event EventHandler<Guid>? OnEditMachineClicked;
    public event EventHandler<Guid>? OnDeleteMachineClicked;
    public ReactiveCommand<MachinesEntryViewModel, Unit> OnEditMachine { get; }
    public ReactiveCommand<MachinesEntryViewModel, Unit> OnDeleteMachine { get; }
    public ObservableCollection<MachinesEntryViewModel> Machines { get; set; } = [];

    public MachinesListViewModel()
    {
        OnEditMachine = ReactiveCommand.Create<MachinesEntryViewModel>(EditMachine);
        OnDeleteMachine = ReactiveCommand.Create<MachinesEntryViewModel>(DeleteMachine);
    }

    public void ReloadData()
    {
        Machines.Clear();
        foreach (var machine in DataAccess.GetEntities<Machine>())
        {
            Machines.Add(new MachinesEntryViewModel(machine.Id));
        }
    }

    private void EditMachine(MachinesEntryViewModel machine)
    {
        OnEditMachineClicked?.Invoke(this, machine.MachineId);
    }
    
    private void DeleteMachine(MachinesEntryViewModel machine)
    {
        OnDeleteMachineClicked?.Invoke(this, machine.MachineId);
    }
}