using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Client.Ui.Database.Items.Creation;
using Client.Ui.Shared;
using ReactiveUI;

namespace Client.Ui.Database.Items.ItemList;

public partial class DbItemListView : ReactiveUserControl<DbItemListViewModel>
{
    public DbItemListView()
    {
        InitializeComponent();

        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        var viewModel = DataContext as DbItemListViewModel;
        viewModel?.ReloadData();
        base.OnLoaded(e);
    }

    private async Task DoShowDialogAsync(InteractionContext<CreateItemViewModel, ShowDialogResult> interaction)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime lifetime) return;
        var owner = lifetime.Windows.FirstOrDefault(w => w is DatabaseWindow);
        if (owner == null) return;

        var dialog = new CreateItemView()
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<ShowDialogResult?>(owner);
        interaction.SetOutput(result ?? new ShowDialogResult(DialogResult.Cancel));
    }
}