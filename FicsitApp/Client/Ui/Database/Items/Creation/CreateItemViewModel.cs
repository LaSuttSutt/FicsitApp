using System;
using System.Collections.Generic;
using System.Reactive;
using System.Windows.Input;
using Client.Shared.View;
using Client.Ui.Database.Items.Creation.Pages;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Database.Items.Creation;

public class CreateItemViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, List<Item>> SaveCommand { get; }
    public ICommand NextCommand { get; }
    public ICommand PreviousCommand { get; }
    
    private List<WizardViewModel> WizardPages { get; } = [];
    public WizardViewModel CurrentPage { get; set; }
    private List<Item> SavedItems { get; } = [];
    public bool CanNext { get; set; }
    public bool CanPrevious { get; set; }
    
    public CreateItemViewModel()
    {
        SaveCommand = ReactiveCommand.Create(() => SavedItems);
        
        WizardPages.Add(new ImagesViewModel());
        WizardPages.Add(new ItemDetailsViewModel());
        CurrentPage = WizardPages[0];
        WizardPages[0].StateChanged += OnCurrentPageStateChanged;
        WizardPages[1].StateChanged += OnCurrentPageStateChanged;
        
        NextCommand = ReactiveCommand.Create(OnNextCommand);
        PreviousCommand = ReactiveCommand.Create(OnPreviousCommand);
        CanNext = CurrentPage.CanGoForward;
    }

    private void OnNextCommand()
    {
        var index = WizardPages.IndexOf(CurrentPage);
        CurrentPage = WizardPages[index + 1];
        this.RaisePropertyChanged(nameof(CurrentPage));
        OnCurrentPageStateChanged(this);
    }

    private void OnPreviousCommand()
    {
        var index = WizardPages.IndexOf(CurrentPage);
        CurrentPage = WizardPages[index - 1];
        this.RaisePropertyChanged(nameof(CurrentPage));
        OnCurrentPageStateChanged(this);
    }
    
    private void OnCurrentPageStateChanged(object? sender, EventArgs? eventArgs = null)
    {
        var index = WizardPages.IndexOf(CurrentPage);
        CanNext = index < WizardPages.Count - 1 && CurrentPage.CanGoForward;
        CanPrevious = index > 0;
        
        this.RaisePropertyChanged(nameof(CanNext));
        this.RaisePropertyChanged(nameof(CanPrevious));
    }
}