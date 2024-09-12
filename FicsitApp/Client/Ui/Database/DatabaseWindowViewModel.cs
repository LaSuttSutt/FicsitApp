using Client.Shared.View;
using Client.Ui.Database.Home;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    public static ViewModelBase TestViewModel => new HomeViewModel();
    public ViewModelBase CurrentView { get; set; }
    private HomeViewModel HomeViewModel { get; } = new();
    
    public DatabaseWindowViewModel()
    {
        CurrentView = HomeViewModel;
    }
}