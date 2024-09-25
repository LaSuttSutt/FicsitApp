using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Client.Ui.Database.Machines.Creation;

public partial class CreateMachineView : UserControl
{
    public CreateMachineView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        TxtName.SelectAll();
        TxtName.Focus();
        base.OnLoaded(e);
    }
}