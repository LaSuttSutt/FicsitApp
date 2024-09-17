using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
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
    
    public MachinesViewModel()
    {
        AddMachineCommand = ReactiveCommand.Create(() => { });
        ListViewModel.OnEditMachineClicked += ListViewModelOnOnEditMachineClicked;
        ListViewModel.OnDeleteMachineClicked += ListViewModelOnOnDeleteMachineClicked;
        Title = "Machines";
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