using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Windows.Input;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Client.Ui.Database.Items.Creation;
using Client.Ui.Shared;
using Client.Ui.Shared.Dialogs;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Database.Items.ItemList;

public class DbItemListViewModel : ViewModelBase
{
    #region Declarations

    public ObservableCollection<DbItemListEntryViewModel> Items { get; set; } = [];

    private DbItemListEntryViewModel? _selectedItem;
    public event EventHandler<Guid>? SelectedItemChanged;

    public ReactiveCommand<DbItemListEntryViewModel, Unit> OnEditItem { get; }
    public ReactiveCommand<DbItemListEntryViewModel, Unit> OnDeleteItem { get; }

    public ICommand AddItemCommand { get; }

    public DbItemListEntryViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            SelectedItemChanged?.Invoke(this, value?.ItemId ?? Guid.Empty);
        }
    }

    #endregion

    #region Constructors

    public DbItemListViewModel()
    {
        OnEditItem = ReactiveCommand.Create<DbItemListEntryViewModel>(EditItem);
        OnDeleteItem = ReactiveCommand.Create<DbItemListEntryViewModel>(DeleteItem);
        
        AddItemCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var item = new Item();

            var viewModel = new CreateItemViewModel(item);
            var result = await SaveCancelDialog.Show<DatabaseWindow>(viewModel);

            if (result == DialogResult.Ok)
            {
                DataAccess.AddEntity(item);
                Items.Add(new DbItemListEntryViewModel(item.Id));
            }
        });
    }

    #endregion

    #region Methods

    public void ReloadData()
    {
        Items.Clear();
        foreach (var item in DataAccess.GetEntities<Item>())
        {
            Items.Add(new DbItemListEntryViewModel(item.Id));
        }

        if (Items.Count > 0)
            SelectedItem = Items[0];
    }

    private async void EditItem(DbItemListEntryViewModel e)
    {
        var item = DataAccess.GetEntity<Item>(e.ItemId);
        if (item == null) return;

        var itemClone = item.Clone();
        var viewModel = new CreateItemViewModel(itemClone);
        var result = await SaveCancelDialog.Show<DatabaseWindow>(viewModel);

        if (result != DialogResult.Ok) return;

        item.Update(itemClone);
        DataAccess.UpdateEntity(item);
        var model = Items.FirstOrDefault(model => model.ItemId == item.Id);
        model?.Reload();
        SelectedItemChanged?.Invoke(this, e.ItemId);
    }

    private void DeleteItem(DbItemListEntryViewModel e)
    {
        var item = DataAccess.GetEntity<Item>(e.ItemId);
        if (item == null) return;

        var model = Items.FirstOrDefault(model => model.ItemId == item.Id);
        if (model == null) return;

        Items.Remove(model);

        // Delete recipes & ingredients before item
        var recipes = DataAccess.GetEntities<Recipe>(r => r.ItemId == item.Id);
        var ingredients = new List<Ingredient>();
        recipes.ForEach(r => ingredients.AddRange(
            DataAccess.GetEntities<Ingredient>(i => i.RecipeId == r.Id)));
        DataAccess.DeleteEntities(ingredients);
        DataAccess.DeleteEntities(recipes);
        DataAccess.DeleteEntity(item);
    }

    #endregion
}