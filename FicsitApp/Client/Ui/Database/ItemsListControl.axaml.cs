using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.Primitives;
using Client.Shared.DomainModel;

namespace Client.Ui.Database;

public class ItemsListControl : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<ItemViewModel>> ItemsProperty = 
        AvaloniaProperty.Register<ItemsListControl, ObservableCollection<ItemViewModel>>(nameof(Items));

    public static readonly StyledProperty<ItemViewModel> SelectedItemProperty = 
        AvaloniaProperty.Register<ItemsListControl, ItemViewModel>(nameof(SelectedItem));
    
    public ObservableCollection<ItemViewModel> Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }
    
    public ItemViewModel SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
}