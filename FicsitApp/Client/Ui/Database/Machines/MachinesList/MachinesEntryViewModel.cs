using System;
using System.Linq;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Shared.TestData;

namespace Client.Ui.Database.Machines.MachinesList;

public class MachinesEntryViewModel : ViewModelBase
{
    public Guid MachineId { get; }
    public string Name { get; }
    public Bitmap Image { get; init; }
    public int Inputs { get; }
    public int ByProducts { get; }

    public MachinesEntryViewModel(Guid machineId)
    {
        MachineId = machineId;
        
        var machine = ItemDatabase.Machines.FirstOrDefault(m => m.Id == machineId);
        Name = machine?.Name ?? "<Machine not found>";
        Image = machine?.Image() ?? ImageHelper.DefaultImage;
        Inputs = machine?.ItemInputs ?? 0;
        ByProducts = machine?.ByProducts ?? 0;
    }
}