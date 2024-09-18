using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Client.Helper;
using Client.Shared.View;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database.Machines.Creation;

public class CreateMachineViewModel : ViewModelBase
{
    public Machine Machine { get; }
    public List<FicsitImage> MachineImages => ImageHelper.MachineImages;
    public FicsitImage? SelectedMachineImage { get; set; }

    public ReactiveCommand<Unit, ShowDialogResult> SaveMachineCommand { get; }
    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } = 
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));

    public CreateMachineViewModel(Machine machine)
    {
        Machine = machine;
        SelectedMachineImage = MachineImages.FirstOrDefault(f => f.ImagePath == machine.ImageName);
        
        SaveMachineCommand = ReactiveCommand.Create(() =>
        {
            if (SelectedMachineImage != null)
                machine.ImageName = SelectedMachineImage.ImagePath;
            
            return new ShowDialogResult(DialogResult.Ok);
        });
    }
}