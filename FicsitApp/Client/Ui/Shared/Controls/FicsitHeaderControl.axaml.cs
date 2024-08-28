using Avalonia.Controls;
using Client.Shared.DomainModel;

namespace Client.Ui.Shared.Controls;

public partial class FicsitHeaderControl : UserControl
{
    public ItemViewModel Item { get; set; } = null!;
    
    public FicsitHeaderControl()
    {
        InitializeComponent();
    }
}