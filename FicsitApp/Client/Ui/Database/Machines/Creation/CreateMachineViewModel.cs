using System.Reactive;
using Client.Shared.View;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database.Machines.Creation;

public class CreateMachineViewModel : ViewModelBase
{
    public Machine Machine { get; set; }
    
    public ReactiveCommand<Unit, CreateMachineViewModel> SaveMachineCommand { get; set; }
    public ReactiveCommand<Unit, CreateMachineViewModel> CancelCommand { get; set; }
    
    
    public CreateMachineViewModel(Machine machine)
    {
        Machine = machine;
        SaveMachineCommand = ReactiveCommand.Create(() => this);
        CancelCommand = ReactiveCommand.Create(() => this);
    }
}