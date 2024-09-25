using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;

namespace Client.Ui.Shared.Dialogs;

public partial class ModalWindow : ReactiveWindow<ModalWindowModel>
{
    public static async Task<bool> Show<T>(IModalWindowModel viewModel) where T : Window
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime lifetime) 
            return false;
        
        var owner = lifetime.Windows.LastOrDefault(w => w is T);
        if (owner == null) return false;
        
        var dialog = new ModalWindow()
        {
            Title = viewModel.Title,
            Width = viewModel.Size.Width + 40,
            Height = viewModel.Size.Height + 40 + 31,
            DataContext = new ModalWindowModel(viewModel)
        };
        
        await dialog.ShowDialog(owner);
        return true;
    }
    
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public ModalWindow()
    {
        InitializeComponent();
    }
}