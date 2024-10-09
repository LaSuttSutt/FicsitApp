using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Client.Shared.DomainModel;
using Client.Shared.View;
using ReactiveUI;

namespace Client.Ui.Projects.Planning;

public class ResourceEntryViewModel : ViewModelBase
{
    #region Declarations
    
    public ItemListModel ResourceItem { get; }
    public ObservableCollection<ItemEntryViewModel> Miner { get; set; } = [];
    public decimal Amount { get; set; }
    public decimal Required { get; set; }
    public decimal Difference { get; set; }
    public bool Overproduction => Difference > 0;
    public bool Underproduction => Difference < 0;
    public ICommand AddMinerCommand { get; set; }
    
    #endregion
    
    #region Constructors

    public ResourceEntryViewModel(ItemListModel resourceItem)
    {
        ResourceItem = resourceItem;
        AddMinerCommand = ReactiveCommand.Create(AddMiner);
    }
    
    #endregion
    
    #region Methods

    public void SetRequired(decimal required)
    {
        Required = required;
        Difference = Math.Round(Amount - Required, 2, MidpointRounding.AwayFromZero);
        this.RaisePropertyChanged(nameof(Required));
        this.RaisePropertyChanged(nameof(Difference));
    }

    private void AddMiner()
    {
        var newItem = new ItemEntryViewModel(true);
        Miner.Add(newItem);
    }
    
    #endregion
}