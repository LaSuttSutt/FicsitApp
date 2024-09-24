using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using Client.Ui.Database;
using Client.Ui.Projects;
using Client.Ui.Projects.Creation;
using Client.Ui.Shared;
using ReactiveUI;

namespace Client.Ui.MainView;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();

        if (Design.IsDesignMode) return;
        this.WhenActivated(action =>
            action(ViewModel!.ShowProjectDialog.RegisterHandler(DoShowProjectDialogAsync)));
        this.WhenActivated(action =>
            action(ViewModel!.ShowDatabaseDialog.RegisterHandler(DoShowDatabaseDialogAsync)));
        this.WhenActivated(action =>
            action(ViewModel!.ShowCreateProjectDialog.RegisterHandler(DoShowCreateProjectDialogAsync)));
    }
    
    private async Task DoShowProjectDialogAsync(InteractionContext<ProjectWindowViewModel, bool> interaction)
    {
        var dialog = new ProjectWindow()
        {
            DataContext = interaction.Input
        };

        await dialog.ShowDialog(this);
        interaction.SetOutput(true);
    }

    private async Task DoShowDatabaseDialogAsync(InteractionContext<DatabaseWindowViewModel, bool> interaction)
    {
        var dialog = new DatabaseWindow()
        {
            DataContext = interaction.Input
        };

        await dialog.ShowDialog(this);
        interaction.SetOutput(true);
    }

    private async Task DoShowCreateProjectDialogAsync(
        InteractionContext<CreateProjectWindowViewModel, ShowDialogResult> interaction)
    {
        var dialog = new CreateProjectWindow()
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<ShowDialogResult?>(this);
        interaction.SetOutput(result ?? new ShowDialogResult(DialogResult.Cancel));
    }
}