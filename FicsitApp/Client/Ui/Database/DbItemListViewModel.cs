using System;
using System.Collections.ObjectModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.TestData;

namespace Client.Ui.Database;

public class DbItemListViewModel : ViewModelBase
{
    public ObservableCollection<DbItemListEntryViewModel> Items { get; set; } = [];

    private DbItemListEntryViewModel? _selectedItem;

    public event EventHandler<Guid>? SelectedItemChanged;

    public DbItemListEntryViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            if (value is not null)
                SelectedItemChanged?.Invoke(this, value.ItemId);
        }
    }

    public DbItemListViewModel()
    {
        foreach (var item in ItemDatabase.Items)
        {
            Items.Add(new DbItemListEntryViewModel(item.Id));
        }

        SelectedItem = Items[0];
    }
}