using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using Client.Shared.View;
using Client.Ui.Database.Creation;
using ReactiveUI;
using Shared.DomainModel;
using Shared.TestData;

namespace Client.Ui.Database.ItemList;

public class DbItemListViewModel : ViewModelBase
{
    public ObservableCollection<DbItemListEntryViewModel> Items { get; set; } = [];

    private DbItemListEntryViewModel? _selectedItem;

    public event EventHandler<Guid>? SelectedItemChanged;
    
    public ICommand AddItemCommand { get; }
    public Interaction<CreateItemViewModel, List<Item>?> ShowDialog { get; }

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
        
        ShowDialog = new Interaction<CreateItemViewModel, List<Item>?>();
        AddItemCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var viewModel = new CreateItemViewModel();
            var result = await ShowDialog.Handle(viewModel);

            Console.WriteLine(result is not null ? $"Liste mit {result.Count} Items" : "Cancelled");
        });
    }
}