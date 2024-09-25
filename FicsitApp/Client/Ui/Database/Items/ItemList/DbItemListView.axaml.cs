using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Client.Ui.Database.Items.ItemList;

public partial class DbItemListView : UserControl
{
    public DbItemListView()
    {
        InitializeComponent();
    }

    protected override void OnLoaded(RoutedEventArgs e)
    {
        var viewModel = DataContext as DbItemListViewModel;
        viewModel?.ReloadData();
        base.OnLoaded(e);
    }
}