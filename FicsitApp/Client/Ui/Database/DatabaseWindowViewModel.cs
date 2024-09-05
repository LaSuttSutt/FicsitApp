using System;
using Client.Shared.View;
using Client.Ui.Database.ItemDetails;
using Client.Ui.Database.ItemList;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    public DbItemListViewModel ItemListViewModel { get; set; } = new();
    public ItemDetailsViewModel ItemDetailsViewModel { get; set; } = new();
    
    public DatabaseWindowViewModel()
    {
        ItemListViewModel.SelectedItemChanged += ItemListViewModelOnSelectedItemChanged;
    }

    private void ItemListViewModelOnSelectedItemChanged(object? sender, Guid e)
    {
        ItemDetailsViewModel.ItemId = e;
    }
}