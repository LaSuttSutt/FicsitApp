using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Recipes;

public class CreateRecipeViewModel : ViewModelBase
{
    public Recipe Recipe { get; }

    public List<ItemListModel> Items { get; } = [];
    public List<Tuple<Machine, Bitmap>> Machines { get; } = [];
    private Tuple<Machine, Bitmap>? _selectedMachine;

    public Tuple<Machine, Bitmap>? SelectedMachine
    {
        get => _selectedMachine;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedMachine, value);
            OnMachineSelectionChanged(value?.Item1 ?? null);
        }
    }
    
    public bool HasMachineSelection => SelectedMachine != null;
    public bool HasIngredient2 { get; set; }
    public bool HasIngredient3 { get; set; }
    public bool HasIngredient4 { get; set; }
    
    public ReactiveCommand<Unit, ShowDialogResult> SaveRecipeCommand { get; }
    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } = 
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));

    public CreateRecipeViewModel(Recipe recipe)
    {
        Recipe = recipe;

        var items = DataAccess.GetEntities<Item>(i => i.Id != recipe.ItemId).ToList();
        items.ForEach(i => Items.Add(new ItemListModel(i, i.Image())));
        
        var machines = DataAccess.GetEntities<Machine>();
        machines.ForEach(m => Machines.Add(new Tuple<Machine, Bitmap>(m, m.Image())));

        SelectedMachine = Machines.FirstOrDefault(m => m.Item1.Id == recipe.MachineId);

        SaveRecipeCommand = ReactiveCommand.Create(() =>
        {
            if (SelectedMachine != null)
            {
                recipe.MachineId = SelectedMachine.Item1.Id;
            }
            return new ShowDialogResult(DialogResult.Ok);
        });
    }

    private void OnMachineSelectionChanged(Machine? machine)
    {
        HasIngredient2 = machine?.ItemInputs >= 2;
        HasIngredient3 = machine?.ItemInputs >= 3;
        HasIngredient4 = machine?.ItemInputs >= 4;
        
        this.RaisePropertyChanged(nameof(HasMachineSelection));
        this.RaisePropertyChanged(nameof(HasIngredient2));
        this.RaisePropertyChanged(nameof(HasIngredient3));
        this.RaisePropertyChanged(nameof(HasIngredient4));
    }
}