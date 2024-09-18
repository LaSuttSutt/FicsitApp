using System.Threading.Tasks;
using Avalonia.Controls;
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
        var dialog = new CreateMachineView()
        {
            DataContext = interaction.Input
        };

        var result = await dialog.ShowDialog<ShowDialogResult>(DatabaseWindow.Instance!);
        interaction.SetOutput(result);
    }
}