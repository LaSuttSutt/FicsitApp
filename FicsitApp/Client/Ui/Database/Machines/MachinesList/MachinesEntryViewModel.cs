﻿using System;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Machines.MachinesList;

public class MachinesEntryViewModel : ViewModelBase
{
    public Guid MachineId { get; }
    public string Name { get; set; }
    public Bitmap Image { get; set; }
    public int Inputs { get; set; }
    public int ByProducts { get; set; }

    public MachinesEntryViewModel(Guid machineId)
    {
        MachineId = machineId;
        
        var machine = DataAccess.GetEntity<Machine>(machineId);
        Name = machine?.Name ?? "<Machine not found>";
        Image = machine?.Image() ?? ImageHelper.DefaultImage;
        Inputs = machine?.ItemInputs ?? 0;
        ByProducts = machine?.ByProducts ?? 0;
    }

    public void Reload()
    {
        var machine = DataAccess.GetEntity<Machine>(MachineId);
        if(machine == null) return;

        Name = machine.Name;
        Image = machine.Image();
        Inputs = machine.ItemInputs;
        ByProducts = machine.ByProducts;
        this.RaisePropertyChanged(nameof(Name));
        this.RaisePropertyChanged(nameof(Image));
        this.RaisePropertyChanged(nameof(Inputs));
        this.RaisePropertyChanged(nameof(ByProducts));
    }
}