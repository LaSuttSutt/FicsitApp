using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.TestData;

namespace Client.Ui.Database;

public class DbItemListUserControlViewModel : ViewModelBase
{
    public ObservableCollection<DbItemListEntryViewModel> Items { get; set; } = [];

    private DbItemListEntryViewModel? _selectedItem;

    public event EventHandler<DbItemListEntryViewModel>? SelectedItemChanged;

    public DbItemListEntryViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedItem, value);
            SelectedItemChanged?.Invoke(this, value!);
        }
    }

    public DbItemListUserControlViewModel()
    {
        var testData = new ItemDatabase();

        foreach (var item in testData.Items)
        {
            Items.Add(new() { Item = item.ViewModel() });
        }

        SelectedItem = Items[0];
    }

    public static readonly List<DbItemListEntryViewModel> TestItems =
    [
        new DbItemListEntryViewModel(), new DbItemListEntryViewModel()
    ];
}