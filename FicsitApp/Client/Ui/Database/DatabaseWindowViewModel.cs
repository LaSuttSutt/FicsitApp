using Client.Shared.View;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    public DbItemListUserControlViewModel ItemListViewModel { get; set; } = new();
    public ItemDetailUserControlViewModel ItemDetailViewModel { get; set; } = new();
    
    public DatabaseWindowViewModel()
    {
        ItemListViewModel.SelectedItemChanged += ItemListViewModelOnSelectedItemChanged;
    }

    private void ItemListViewModelOnSelectedItemChanged(object? sender, DbItemListEntryViewModel? e)
    {
        ItemDetailViewModel.Item = e?.Item ?? null!;
    }
}