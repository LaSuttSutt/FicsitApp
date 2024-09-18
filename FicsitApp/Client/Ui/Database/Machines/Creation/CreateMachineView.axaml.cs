using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Client.Ui.Database.Machines.Creation;

public partial class CreateMachineView : ReactiveWindow<CreateMachineViewModel>
{
    public CreateMachineView()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.SaveMachineCommand.Subscribe(Close)));
        this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        TxtName.SelectAll();
        TxtName.Focus();
        base.OnLoaded(e);
    }
}