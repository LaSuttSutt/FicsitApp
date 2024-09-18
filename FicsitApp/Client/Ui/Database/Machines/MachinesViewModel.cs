using System;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;
using Client.Shared.View;
using Client.Ui.Database.Machines.Creation;
using Client.Ui.Database.Machines.MachinesList;
using Client.Ui.Shared;
using DynamicData;
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
            if (result?.Result == DialogResult.Ok)
            {
                ItemDatabase.Machines.Add(machine);
                ListViewModel.Machines.Add(new MachinesEntryViewModel(machine.Id));
            }
        });
    }
    
    private void ListViewModelOnOnEditMachineClicked(object? sender, Guid e)
    {
        var machine = ItemDatabase.Machines.FirstOrDefault(m => m.Id == e);
        Console.WriteLine($"Edit machine: {machine?.Name ?? "<not found>"}");
    }
    
    private void ListViewModelOnOnDeleteMachineClicked(object? sender, Guid e)
    {
        var machine = ItemDatabase.Machines.FirstOrDefault(m => m.Id == e);
        Console.WriteLine($"Delete machine: {machine?.Name ?? "<not found>"}");
    }
}