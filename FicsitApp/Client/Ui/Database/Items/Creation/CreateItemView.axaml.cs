using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Client.Ui.Database.Items.Creation;

public partial class CreateItemView : ReactiveWindow<CreateItemViewModel>
{
    public CreateItemView()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.SaveItemCommand.Subscribe(Close)));
        this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
    }
    
    protected override void OnLoaded(RoutedEventArgs e)
    {
        TxtName.SelectAll();
        TxtName.Focus();
        base.OnLoaded(e);
    }
}