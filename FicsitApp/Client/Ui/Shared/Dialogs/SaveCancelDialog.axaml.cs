using System;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace Client.Ui.Shared.Dialogs;

public partial class SaveCancelDialog : ReactiveWindow<SaveCancelDialogModel>
{
    public static async Task<DialogResult> Show<T>(ISaveCancelViewModel viewModel) where T : Window
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime lifetime) 
            return DialogResult.Cancel;
        
        var owner = lifetime.Windows.LastOrDefault(w => w is T);
        if (owner == null) return DialogResult.Cancel;
        
        var dialog = new SaveCancelDialog()
        {
            Title = viewModel.Title,
            Width = viewModel.Size.Width + 40,
            Height = viewModel.Size.Height + 40 + 31 + 50,
            DataContext = new SaveCancelDialogModel(viewModel)
        };
        
        var result = await dialog.ShowDialog<ShowDialogResult?>(owner);
        return result?.Result ?? DialogResult.Cancel;
    }
    
    private SaveCancelDialog()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.SaveCommand.Subscribe(BeforeClose)));
        this.WhenActivated(action => action(ViewModel!.CancelCommand.Subscribe(Close)));
    }
    
    private void BeforeClose(object? dialogResult)
    {
        var view = ViewModel!.ViewModel as ISaveCancelViewModel;
        var saved = view?.DoSaving() ?? false;
        if (saved) Close(dialogResult);
    }
}