using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Client.Helper;
using Client.Shared.View;
using Client.Ui.Shared.Dialogs;
using Shared.DomainModel;

namespace Client.Ui.Database.Machines.Creation;

public class CreateMachineViewModel : ViewModelBase, ISaveCancelViewModel
{
    public string Title { get; } = "FicsitApp - Machine";
    public Size Size { get; } = new Size(387, 337);
    
    public Machine Machine { get; }
    public List<FicsitImage> MachineImages => ImageHelper.MachineImages;
    public FicsitImage? SelectedMachineImage { get; set; }
    
    public CreateMachineViewModel(Machine machine)
    {
        Machine = machine;
        SelectedMachineImage = MachineImages.FirstOrDefault(f => f.ImagePath == machine.ImageName);
    }

    public bool DoSaving()
    {
        if (SelectedMachineImage != null)
            Machine.ImageName = SelectedMachineImage.ImagePath;

        return true;
    }
}