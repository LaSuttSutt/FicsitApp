using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Client.Ui.Database.Recipes;
using Client.Ui.Shared;
using ReactiveUI;

namespace Client.Ui.Database.Items.ItemDetails;

public partial class ItemDetailsView : ReactiveUserControl<ItemDetailsViewModel>
{
    public ItemDetailsView()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    private async Task DoShowDialogAsync(InteractionContext<CreateRecipeViewModel, ShowDialogResult> interaction)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime lifetime) return;
        var owner = lifetime.Windows.FirstOrDefault(w => w is DatabaseWindow);
        if (owner == null) return;
        
        var dialog = new CreateRecipeView()
        {
            DataContext = interaction.Input
        };
        
        var result = await dialog.ShowDialog<ShowDialogResult?>(owner);
        interaction.SetOutput(result ?? new ShowDialogResult(DialogResult.Cancel));
    }
}