﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;
using Shared.DomainModel;
using Shared.TestData;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    private DbItemListEntryViewModel _selectedItem = null!;

    public DbItemListEntryViewModel SelectedItem 
    { 
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);   
    }
    
    public ObservableCollection<DbItemListEntryViewModel> ItemsViewModels { get; } = [];
    
    public DatabaseWindowViewModel()
    {
        var testData = new ItemDatabase();

        foreach (var item in testData.Items)
        {
            var viewModel = item.ViewModel();
            ItemsViewModels.Add(new() { Item = viewModel });
        }

        SelectedItem = ItemsViewModels[0];
    }
}