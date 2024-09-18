using Avalonia.Controls;

namespace Client.Ui.Database;

public partial class DatabaseWindow : Window
{
    public static Window? Instance { get; private set; }
    
    public DatabaseWindow()
    {
        InitializeComponent();
        Instance = this;
    }
}