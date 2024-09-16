using System;
using Client.Shared.View;

namespace Client.Shared.Notification;

public static class NotificationService
{
    public static event EventHandler<NavigationViewModel> OnShowNavigationView = (_, _) => { };

    public static void RaiseOnShowNavigationView(object sender, NavigationViewModel viewModel) =>
        OnShowNavigationView.Invoke(sender, viewModel);
}