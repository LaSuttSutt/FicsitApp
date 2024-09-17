using System;
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
    private Window _mainWindow = null!;
    public MachinesListViewModel ListViewModel { get; } = new();
    public ICommand AddMachineCommand { get; }
    private Interaction<CreateMachineViewModel, ShowDialogResult?> ShowDialog { get; }
    
    public MachinesViewModel()
    {
        ListViewModel.OnEditMachineClicked += ListViewModelOnOnEditMachineClicked;
        ListViewModel.OnDeleteMachineClicked += ListViewModelOnOnDeleteMachineClicked;
        Title = "Machines";
        
        ShowDialog = new Interaction<CreateMachineViewModel, ShowDialogResult?>();
        ShowDialog.RegisterHandler(DoShowDialogAsync);
        AddMachineCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var newMachine = new Machine
            {
                Id = Guid.NewGuid(),
                Name = "Manufacturer",
                ItemInputs = 4,
                ByProducts = 0,
                ImageName = "avares://Client/Assets/ImageDb/G_Manufacturer_64.png"
            };
            
            var viewModel = new CreateMachineViewModel(newMachine);
            var result = await ShowDialog.Handle(viewModel);

            if (result?.Result == DialogResult.Ok)
            {
                ItemDatabase.Machines.Add(newMachine);
                ListViewModel.Machines.Add(new MachinesEntryViewModel(newMachine.Id));
            }
        });
    }

    public void SetWindow(Window window)
    {
        _mainWindow = window;
    }
    
    private async Task DoShowDialogAsync(InteractionContext<CreateMachineViewModel, ShowDialogResult?> interaction)
    {
        var dialog = new CreateMachineView()
        {
            DataContext = interaction.Input
        };
        
        var result = await dialog.ShowDialog<ShowDialogResult>(_mainWindow);
        interaction.SetOutput(result);
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