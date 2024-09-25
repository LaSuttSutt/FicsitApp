using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Client.Ui.Database.Recipes;

public partial class CreateRecipeView : UserControl
{
    public CreateRecipeView()
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