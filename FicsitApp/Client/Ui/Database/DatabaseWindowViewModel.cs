using System.Reactive;
using Client.Shared.Notification;
using Client.Shared.View;
using Client.Ui.Database.Home;
using ReactiveUI;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    public ViewModelBase CurrentView { get; set; }
    public string CurrentTitle { get; set; }

    public ReactiveCommand<Unit, Unit> OnHomeClicked { get; }
    private HomeViewModel HomeViewModel { get; } = new();
    
    
    public DatabaseWindowViewModel()
    {
        CurrentView = HomeViewModel;
        CurrentTitle = HomeViewModel.Title;
        OnHomeClicked = ReactiveCommand.Create(ShowHomeView);
        NotificationService.OnShowNavigationView += NotificationServiceOnOnShowNavigationView;
    }

    private void ShowHomeView()
    {
        if (CurrentView is HomeViewModel)
            return;
        
        NotificationServiceOnOnShowNavigationView(this, HomeViewModel);
    }

    private void NotificationServiceOnOnShowNavigationView(object? sender, NavigationViewModel e)
    {
        CurrentTitle = e.Title;
        CurrentView = e;
        
        this.RaisePropertyChanged(nameof(CurrentTitle));
        this.RaisePropertyChanged(nameof(CurrentView));
    }
}