using System;
using System.Reflection;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Client.Shared.View;
using Client.Ui.Database;
using ReactiveUI;

namespace Client.Ui.MainView;

public class MainWindowViewModel : ViewModelBase
{
    #region View Properties ---------------------

    public ICommand OpenDatabaseWindow { get; }

    #endregion

    #region C'tor -------------------------------

    public MainWindowViewModel()
    {
        OpenDatabaseWindow = ReactiveCommand.Create<Window>(OnOpenDatabaseWindow);
    }

    #endregion

    #region Private Methods ---------------------

    private void OnOpenDatabaseWindow(Window owner)
    {
        var dialog = new DatabaseWindow
        {
            DataContext = new DatabaseWindowViewModel()
        };
        dialog.ShowDialog(owner);
    }

    #endregion
}