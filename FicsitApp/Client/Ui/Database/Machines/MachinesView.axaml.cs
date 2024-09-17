using Avalonia.ReactiveUI;

namespace Client.Ui.Database.Machines;

public partial class MachinesView : ReactiveUserControl<MachinesViewModel>
{
    public MachinesView()
    {
        InitializeComponent();
    }
}