using System.Reactive;
using Avalonia.Controls;
using Client.Shared.Notification;
using Client.Shared.View;
using Client.Ui.Database.Items;
using Client.Ui.Database.Machines;
using ReactiveUI;

namespace Client.Ui.Database.Home;

public class HomeViewModel : NavigationViewModel
{
    public ReactiveCommand<NavigationViewModel, Unit> OnNavButtonClicked { get; set; }
    
    public ItemsViewModel ItemsViewModel { get; } = new();
    public MachinesViewModel MachinesViewModel { get; } = new();

    public HomeViewModel()
    {
        Title = "Ficsit Database";
        OnNavButtonClicked = ReactiveCommand.Create<NavigationViewModel>(ShowSubView);
    }

    public void SetWindow(Window window)
    {
        MachinesViewModel.SetWindow(window);
    }

    private void ShowSubView(NavigationViewModel viewModel)
    {
        NotificationService.RaiseOnShowNavigationView(this, viewModel);
    }
}