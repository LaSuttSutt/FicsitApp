using System;
using System.Linq;
using System.Reactive;
using Client.Shared.View;
using Client.Ui.Database.Machines.MachinesList;
using ReactiveUI;
using Shared.DomainModel;
using Shared.TestData;

namespace Client.Ui.Database.Machines;

public class MachinesViewModel : NavigationViewModel
{
    public MachinesListViewModel ListViewModel { get; } = new();
    public ReactiveCommand<Unit, Unit> AddMachineCommand { get; }
    
    public MachinesViewModel()
    {
        AddMachineCommand = ReactiveCommand.Create(AddMachine);
        ListViewModel.OnEditMachineClicked += ListViewModelOnOnEditMachineClicked;
        ListViewModel.OnDeleteMachineClicked += ListViewModelOnOnDeleteMachineClicked;
        Title = "Machines";
    }

    private void AddMachine()
    {
        var newMachine = new Machine
        {
            Id = Guid.NewGuid(),
            Name = "Manufacturer",
            ItemInputs = 4,
            ByProducts = 0,
            ImageName = "avares://Client/Assets/ImageDb/G_Manufacturer_64.png"
        };
        
        ItemDatabase.Machines.Add(newMachine);
        ListViewModel.Machines.Add(new MachinesEntryViewModel(newMachine.Id));
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