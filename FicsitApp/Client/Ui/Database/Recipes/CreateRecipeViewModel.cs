using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Client.Helper;
using Client.Shared.View;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Recipes;

public class CreateRecipeViewModel : ViewModelBase
{
    public Recipe Recipe { get; set; }
    public List<FicsitImage> MachineImages => ImageHelper.MachineImages;
    public FicsitImage? SelectedMachineImage { get; set; }
    
    public ReactiveCommand<Unit, ShowDialogResult> SaveRecipeCommand { get; }
    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } = 
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));

    public CreateRecipeViewModel(Recipe recipe)
    {
        Recipe = recipe;
        var machines = DataAccess.GetEntities<Machine>();
        var machine = machines.FirstOrDefault(m => m.Id == recipe.MachineId);

        if (machine != null)
            SelectedMachineImage = MachineImages.FirstOrDefault
                        (f => f.ImagePath == machine.ImageName);

        SaveRecipeCommand = ReactiveCommand.Create(() =>
        {
            if (SelectedMachineImage != null)
            {
                var selectedMachine = machines.FirstOrDefault
                    (m => m.ImageName == SelectedMachineImage.ImagePath);
                recipe.MachineId = selectedMachine?.Id ?? Guid.Empty;
            }
            return new ShowDialogResult(DialogResult.Ok);
        });
    }
}