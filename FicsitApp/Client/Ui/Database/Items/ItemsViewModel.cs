using System;
using Client.Shared.View;
using Client.Ui.Database.Items.ItemList;
using Client.Ui.Database.Items.ItemDetails;

namespace Client.Ui.Database.Items;

public class ItemsViewModel : ViewModelBase
{
    public DbItemListViewModel ItemListViewModel { get; set; } = new();
    public ItemDetailsViewModel ItemDetailsViewModel { get; set; } = new();
    
    public ItemsViewModel()
    {
        ItemListViewModel.SelectedItemChanged += ItemListViewModelOnSelectedItemChanged;
    }

    private void ItemListViewModelOnSelectedItemChanged(object? sender, Guid e)
    {
        ItemDetailsViewModel.ItemId = e;
    }
}