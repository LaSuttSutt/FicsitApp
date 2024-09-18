using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Client.Ui.Database.Machines.Creation;
using Client.Ui.Shared;
using ReactiveUI;

namespace Client.Ui.Database.Machines;

public partial class MachinesView : ReactiveUserControl<MachinesViewModel>
{
    public MachinesView()
    {
        InitializeComponent();
        
        if (Design.IsDesignMode) return;
        this.WhenActivated(action => action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    private async Task DoShowDialogAsync(InteractionContext<CreateMachineViewModel, ShowDialogResult> interaction)
    {
        if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime lifetime) return;
        var owner = lifetime.Windows.FirstOrDefault(w => w is DatabaseWindow);
        if (owner == null) return;
        
        var dialog = new CreateMachineView()
        {
            DataContext = interaction.Input
        };
        
        var result = await dialog.ShowDialog<ShowDialogResult?>(owner);
        interaction.SetOutput(result ?? new ShowDialogResult(DialogResult.Cancel));
    }
}