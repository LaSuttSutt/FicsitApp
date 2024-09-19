using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using Client.Shared.View;
using Client.Ui.Database.Items.Creation;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;
using Shared.TestData;

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
    public Interaction<CreateItemViewModel, ShowDialogResult> ShowDialog { get; }

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

    #endregion
    
    #region Constructors

    public DbItemListViewModel()
    {
        OnEditItem = ReactiveCommand.Create<DbItemListEntryViewModel>(EditItem);
        OnDeleteItem = ReactiveCommand.Create<DbItemListEntryViewModel>(DeleteItem);

        ShowDialog = new Interaction<CreateItemViewModel, ShowDialogResult>();
        AddItemCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var item = new Item();
            
            var viewModel = new CreateItemViewModel();
            var result = await ShowDialog.Handle(viewModel);

            if (result.Result == DialogResult.Ok)
            {
                
            }
            /*
             var machine = new Machine();
               
               var viewModel = new CreateMachineViewModel(machine);
               var result = await ShowDialog.Handle(viewModel);
               
               if (result.Result == DialogResult.Ok)
               {
                   DataAccess.AddEntity(machine);
                   ListViewModel.Machines.Add(new MachinesEntryViewModel(machine.Id));
               }
             */
        });
    }

    #endregion
    
    #region Methods
    
    public void ReloadData()
    {
        Items.Clear();
        foreach (var item in ItemDatabase.Items)
        {
            Items.Add(new DbItemListEntryViewModel(item.Id));
        }

        SelectedItem = Items[0];
    }

    private void EditItem(DbItemListEntryViewModel item)
    {
    }

    private void DeleteItem(DbItemListEntryViewModel item)
    {
    }
    
    #endregion
}