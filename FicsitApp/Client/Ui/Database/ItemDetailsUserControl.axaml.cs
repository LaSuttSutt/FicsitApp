using Avalonia;
using Avalonia.Controls;
using Client.Shared.DomainModel;

namespace Client.Ui.Database;

public partial class ItemDetailsUserControl : UserControl
{
    public static readonly StyledProperty<ItemViewModel> ItemProperty = 
        AvaloniaProperty.Register<ItemDetailsUserControl, ItemViewModel>(nameof(Item));
    
    public ItemViewModel Item
    {
        get => GetValue(ItemProperty);
        set => SetValue(ItemProperty, value);
    }

    public ItemDetailsUserControl()
    {
        InitializeComponent();
    }
}