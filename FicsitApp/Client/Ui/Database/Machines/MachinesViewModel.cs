using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Database.Machines.Creation;
using Client.Ui.Database.Machines.MachinesList;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;
using Shared.TestData;

namespace Client.Ui.Database.Machines;

public class MachinesViewModel : NavigationViewModel
{
    public MachinesListViewModel ListViewModel { get; } = new();
    public ICommand AddMachineCommand { get; }
    
    public Interaction<CreateMachineViewModel, ShowDialogResult> ShowDialog { get; }
    
    public MachinesViewModel()
    {
        ListViewModel.OnEditMachineClicked += ListViewModelOnOnEditMachineClicked;
        ListViewModel.OnDeleteMachineClicked += ListViewModelOnOnDeleteMachineClicked;
        Title = "Machines";
        
        ShowDialog = new Interaction<CreateMachineViewModel, ShowDialogResult>();
        AddMachineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var machine = new Machine();
            
            var viewModel = new CreateMachineViewModel(machine);
            var result = await ShowDialog.Handle(viewModel);
            
            if (result.Result == DialogResult.Ok)
            {
                ItemDatabase.Machines.Add(machine);
                ListViewModel.Machines.Add(new MachinesEntryViewModel(machine.Id));
            }
        });
    }
    
    private async void ListViewModelOnOnEditMachineClicked(object? sender, Guid e)
    {
        var machine = ItemDatabase.Machines.FirstOrDefault(m => m.Id == e);
        if(machine == null) return;
        
        var machineClone = machine.Clone();
        var viewModel = new CreateMachineViewModel(machineClone);
        var result = await ShowDialog.Handle(viewModel);

        if (result.Result != DialogResult.Ok) return;
        machine.Update(machineClone);
        var model = ListViewModel.Machines.FirstOrDefault(model => model.MachineId == machine.Id);
        model?.Reload();
    }
    
    private void ListViewModelOnOnDeleteMachineClicked(object? sender, Guid e)
    {
        var machine = ItemDatabase.Machines.FirstOrDefault(m => m.Id == e);
        if(machine == null) return;
        
        var model = ListViewModel.Machines.FirstOrDefault(model => model.MachineId == machine.Id);
        if (model == null) return;
        
        ListViewModel.Machines.Remove(model);
        ItemDatabase.Machines.Remove(machine);
    }
}