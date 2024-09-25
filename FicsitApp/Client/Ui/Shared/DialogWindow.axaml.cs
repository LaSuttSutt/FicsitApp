using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Client.Shared.View;
using ReactiveUI;

namespace Client.Ui.Shared;

public partial class DialogWindow : ReactiveWindow<DialogWindowViewModel>
{
    public static async Task<DialogResult> Show<T>(DialogWindowModel viewModel, string title) where T : Window
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime lifetime) 
            return DialogResult.Cancel;
        
        var owner = lifetime.Windows.LastOrDefault(w => w is T);
        if (owner == null) return DialogResult.Cancel;
        
        var dialog = new DialogWindow()
        {
            Title = title,
            Width = viewModel.Size.Width + 30,
            Height = viewModel.Size.Height + 30 + 31 + 50,
            DataContext = new DialogWindowViewModel(viewModel)
        };
        
        var result = await dialog.ShowDialog<ShowDialogResult?>(owner);
        return result?.Result ?? DialogResult.Cancel;
    }
    
    private DialogWindow()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        
        this.WhenActivated(action => action(ViewModel!.SaveCommand.Subscribe(BeforeClose)));
        this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
    }
    
    private void BeforeClose(object? dialogResult)
    {
        var view = ViewModel!.ViewModel as DialogWindowModel;
        view?.DoSaving();
        Close(dialogResult);
    }
}