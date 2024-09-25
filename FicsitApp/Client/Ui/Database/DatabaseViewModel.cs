using System.Reactive;
using Avalonia;
using Client.Shared.Notification;
using Client.Shared.View;
using Client.Ui.Database.Home;
using Client.Ui.Shared.Dialogs;
using ReactiveUI;

namespace Client.Ui.Database;

public class DatabaseViewModel : ViewModelBase, IModalWindowModel
{
    public string Title => "Ficsit App - Database";
    public Size Size { get; } = new Size(984, 697);
    public ViewModelBase CurrentView { get; set; }
    public string CurrentTitle { get; set; }

    public ReactiveCommand<Unit, Unit> OnHomeClicked { get; }
    private HomeViewModel HomeViewModel { get; } = new();
    
    public DatabaseViewModel()
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