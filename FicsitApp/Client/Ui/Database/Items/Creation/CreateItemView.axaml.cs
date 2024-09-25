using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Client.Ui.Database.Items.Creation;

public partial class CreateItemView : UserControl
{
    public CreateItemView()
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