using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace Client.Ui.Database;

public class ItemsListControl : TemplatedControl
{
    public static readonly StyledProperty<ObservableCollection<DbItemListEntryViewModel>> ItemsProperty = 
        AvaloniaProperty.Register<ItemsListControl, ObservableCollection<DbItemListEntryViewModel>>(nameof(Items));

    public static readonly StyledProperty<DbItemListEntryViewModel> SelectedItemProperty = 
        AvaloniaProperty.Register<ItemsListControl, DbItemListEntryViewModel>(nameof(SelectedItem));
    
    public ObservableCollection<DbItemListEntryViewModel> Items
    {
        get => GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }
    
    public DbItemListEntryViewModel SelectedItem
    {
        get => GetValue(SelectedItemProperty);
        set => SetValue(SelectedItemProperty, value);
    }
}