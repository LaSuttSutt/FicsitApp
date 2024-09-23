using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Client.Ui.Database.Recipes;

public partial class CreateRecipeView : ReactiveWindow<CreateRecipeViewModel>
{
    public CreateRecipeView()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;

        this.WhenActivated(action => action(ViewModel!.SaveRecipeCommand.Subscribe(Close)));
        this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        TxtName.SelectAll();
        TxtName.Focus();
        base.OnLoaded(e);
    }
}