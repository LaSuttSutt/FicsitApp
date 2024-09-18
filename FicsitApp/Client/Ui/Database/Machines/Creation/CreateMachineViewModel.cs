using System.Reactive;
using Client.Shared.View;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database.Machines.Creation;

public class CreateMachineViewModel : ViewModelBase
{
    public Machine Machine { get; set; }
    
    public ReactiveCommand<Unit, ShowDialogResult> SaveMachineCommand { get; }
    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; }
    
    
    public CreateMachineViewModel(Machine machine)
    {
        Machine = machine;
        
        SaveMachineCommand = ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Ok));
        CancelCommand = ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));
    }
}