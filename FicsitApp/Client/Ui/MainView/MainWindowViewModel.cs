using System.Windows.Input;
using Avalonia.Controls;
using Client.Helper;
using Client.Shared.View;
using Client.Ui.Database;
using ReactiveUI;
using Shared.TestData;

namespace Client.Ui.MainView;

public class MainWindowViewModel : ViewModelBase
{
    #region View Properties ---------------------

    public ICommand OpenDatabaseWindow { get; }

    #endregion

    #region C'tor -------------------------------

    public MainWindowViewModel()
    {
        ImageHelper.Initialize();
        ItemDatabase.Initialize();
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