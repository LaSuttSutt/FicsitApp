using System;
using System.Linq;
using System.Windows.Input;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Database.Machines.Creation;
using Client.Ui.Database.Machines.MachinesList;
using Client.Ui.Shared;
using Client.Ui.Shared.Dialogs;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Machines;

public class MachinesViewModel : NavigationViewModel
{
    public MachinesListViewModel ListViewModel { get; } = new();
    public ICommand AddMachineCommand { get; }
    
    public MachinesViewModel()
    {
        ListViewModel.OnEditMachineClicked += ListViewModelOnOnEditMachineClicked;
        ListViewModel.OnDeleteMachineClicked += ListViewModelOnOnDeleteMachineClicked;
        Title = "Machines";
        
        AddMachineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var machine = new Machine();
            
            var viewModel = new CreateMachineViewModel(machine);
            var result = await SaveCancelDialog.Show<DatabaseWindow>(viewModel);
            
            if (result == DialogResult.Ok)
            {
                DataAccess.AddEntity(machine);
                ListViewModel.Machines.Add(new MachinesEntryViewModel(machine.Id));
            }
        });
    }
    
    private async void ListViewModelOnOnEditMachineClicked(object? sender, Guid e)
    {
        var machine = DataAccess.GetEntity<Machine>(e);
        if(machine == null) return;
        
        var machineClone = machine.Clone();
        var viewModel = new CreateMachineViewModel(machineClone);
        var result = await SaveCancelDialog.Show<DatabaseWindow>(viewModel);

        if (result != DialogResult.Ok) return;
        
        machine.Update(machineClone);
        DataAccess.UpdateEntity(machine);
        var model = ListViewModel.Machines.FirstOrDefault(model => model.MachineId == machine.Id);
        model?.Reload();
    }
    
    private void ListViewModelOnOnDeleteMachineClicked(object? sender, Guid e)
    {
        var machine = DataAccess.GetEntity<Machine>(e);
        if(machine == null) return;
        
        var model = ListViewModel.Machines.FirstOrDefault(model => model.MachineId == machine.Id);
        if (model == null) return;
        
        ListViewModel.Machines.Remove(model);
        DataAccess.DeleteEntity(machine);
    }
}