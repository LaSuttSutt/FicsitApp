using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using Avalonia.Media.Imaging;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Recipes;

public class CreateRecipeViewModel : ViewModelBase
{
    #region Declarations

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
    public bool HasByProduct1 { get; set; }
    public bool HasByProduct2 { get; set; }
    public ItemListModel SelectedIngredient1 { get; set; }
    public decimal Ingredient1Amount { get; set; }
    public ItemListModel SelectedIngredient2 { get; set; }
    public decimal Ingredient2Amount { get; set; }
    public ItemListModel SelectedIngredient3 { get; set; }
    public decimal Ingredient3Amount { get; set; }
    public ItemListModel SelectedIngredient4 { get; set; }
    public decimal Ingredient4Amount { get; set; }
    public ItemListModel SelectedByProduct1 { get; set; }
    public decimal ByProduct1Amount { get; set; }
    public ItemListModel SelectedByProduct2 { get; set; }
    public decimal ByProduct2Amount { get; set; }

    public ReactiveCommand<Unit, ShowDialogResult> SaveRecipeCommand { get; }

    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } =
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));

    #endregion

    #region Constructor

    public CreateRecipeViewModel(Recipe recipe)
    {
        Recipe = recipe;

        LoadPageData();
        DoPreSelection();

        SelectedIngredient1 = Items.First();
        SelectedIngredient2 = Items.First();
        SelectedIngredient3 = Items.First();
        SelectedIngredient4 = Items.First();
        SelectedByProduct1 = Items.First();
        SelectedByProduct2 = Items.First();

        SaveRecipeCommand = ReactiveCommand.Create(() =>
        {
            var saveDataSuccessful = SaveData();
            return new ShowDialogResult(saveDataSuccessful ? DialogResult.Ok : DialogResult.Cancel);
        });
    }

    #endregion

    #region Methods

    private void LoadPageData()
    {
        var items = DataAccess.GetEntities<Item>(i => i.Id != Recipe.ItemId).ToList();
        items.ForEach(i => Items.Add(new ItemListModel(i, i.Image())));

        var machines = DataAccess.GetEntities<Machine>();
        machines.ForEach(m => Machines.Add(new Tuple<Machine, Bitmap>(m, m.Image())));
    }

    private void DoPreSelection()
    {
        SelectedMachine = Machines.FirstOrDefault(m => m.Item1.Id == Recipe.MachineId);
        if (SelectedMachine == null && Machines.Count > 0)
            SelectedMachine = Machines.First();

        var ingredients = DataAccess.GetEntities<Ingredient>
            (i => i.RecipeId == Recipe.Id).OrderBy(i => i.IsByProduct);
    }

    private bool SaveData()
    {
        if (SelectedMachine == null)
            return false;

        Recipe.MachineId = SelectedMachine!.Item1.Id;

        // Delete exisiting ingredients
        var ingredients = DataAccess.GetEntities<Ingredient>(i => i.RecipeId == Recipe.Id);
        DataAccess.DeleteEntities(ingredients);

        SaveIngredient(1);
        if (SelectedMachine.Item1.ItemInputs > 1)
            SaveIngredient(2);
        if (SelectedMachine.Item1.ItemInputs > 2)
            SaveIngredient(3);
        if (SelectedMachine.Item1.ItemInputs > 3)
            SaveIngredient(4);

        if (SelectedMachine.Item1.ByProducts > 0)
            SaveIngredient(1, true);
        if (SelectedMachine.Item1.ByProducts > 1)
            SaveIngredient(2, true);

        return true;
    }

    private void OnMachineSelectionChanged(Machine? machine)
    {
        HasIngredient2 = machine?.ItemInputs >= 2;
        HasIngredient3 = machine?.ItemInputs >= 3;
        HasIngredient4 = machine?.ItemInputs >= 4;
        HasByProduct1 = machine?.ByProducts >= 1;
        HasByProduct2 = machine?.ByProducts >= 2;

        this.RaisePropertyChanged(nameof(HasMachineSelection));
        this.RaisePropertyChanged(nameof(HasIngredient2));
        this.RaisePropertyChanged(nameof(HasIngredient3));
        this.RaisePropertyChanged(nameof(HasIngredient4));
        this.RaisePropertyChanged(nameof(HasByProduct1));
        this.RaisePropertyChanged(nameof(HasByProduct2));
    }

    private void SaveIngredient(int number, bool isByProduct = false)
    {
        Item item;
        decimal amount;

        switch (number)
        {
            case 1:
                item = isByProduct ? SelectedByProduct1.Item : SelectedIngredient1.Item;
                amount = isByProduct ? ByProduct1Amount : Ingredient1Amount;
                break;

            case 2:
                item = isByProduct ? SelectedByProduct2.Item : SelectedIngredient2.Item;
                amount = isByProduct ? ByProduct2Amount : Ingredient2Amount;
                break;

            case 3:
                item = SelectedIngredient3.Item;
                amount = Ingredient3Amount;
                break;

            case 4:
                item = SelectedIngredient4.Item;
                amount = Ingredient4Amount;
                break;

            default:
                return;
        }

        var ingredient = new Ingredient()
            {RecipeId = Recipe.Id, ItemId = item.Id, Amount = amount, IsByProduct = isByProduct};
        DataAccess.AddEntity(ingredient);
    }

    #endregion
}