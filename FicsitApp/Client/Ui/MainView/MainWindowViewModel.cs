using System;
using System.Reactive.Linq;
using System.Windows.Input;
using Client.Helper;
using Client.Shared.View;
using Client.Ui.Database;
using ReactiveUI;
using Shared.DataAccess;
using Shared.TestData;

namespace Client.Ui.MainView;

public class MainWindowViewModel : ViewModelBase
{
    #region View Properties ---------------------

    public ICommand OpenDatabaseWindow { get; }
    public Interaction<DatabaseWindowViewModel, bool> ShowDialog { get; }

    #endregion

    #region C'tor -------------------------------

    public MainWindowViewModel()
    {
        ImageHelper.Initialize();
        ItemDatabase.Initialize();
        DataAccess.Initialize();
        
        ShowDialog = new Interaction<DatabaseWindowViewModel, bool>();
        OpenDatabaseWindow = ReactiveCommand.CreateFromTask(async () =>
        {
            var viewModel = new DatabaseWindowViewModel();
            return await ShowDialog.Handle(viewModel);
        });
    }

    #endregion
}