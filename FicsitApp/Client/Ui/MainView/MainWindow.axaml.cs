using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Client.Ui.Database;
using ReactiveUI;

namespace Client.Ui.MainView;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    private async Task DoShowDialogAsync(InteractionContext<DatabaseWindowViewModel, bool> interaction)
    {
        var dialog = new DatabaseWindow()
        {
            DataContext = interaction.Input
        };

        await dialog.ShowDialog(this);
        interaction.SetOutput(true);
    }
}