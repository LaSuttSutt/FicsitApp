using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Client.Ui.Database.Machines.MachinesList;

public partial class MachinesListView : UserControl
{
    public MachinesListView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        var viewModel = DataContext as MachinesListViewModel;
        viewModel?.ReloadData();
        base.OnLoaded(e);
    }
}