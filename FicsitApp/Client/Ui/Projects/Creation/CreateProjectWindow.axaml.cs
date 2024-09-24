using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Client.Ui.Projects.Creation;

public partial class CreateProjectWindow : ReactiveWindow<CreateProjectWindowViewModel>
{
    public CreateProjectWindow()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.SaveProjectCommand.Subscribe(Close)));
        this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
    }
    
    protected override void OnLoaded(RoutedEventArgs e)
    {
        TxtName.SelectAll();
        TxtName.Focus();
        base.OnLoaded(e);
    }
}