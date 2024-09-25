using System.Windows.Input;
using Client.Shared.View;
using ReactiveUI;

namespace Client.Ui.Projects.Planning;

public class PlanningViewModel : ViewModelBase
{
    public ICommand DialogCheck { get; set; }

    public PlanningViewModel()
    {
        DialogCheck = ReactiveCommand.Create(DoDialogCheck);
    }

    private void DoDialogCheck()
    {
    }
}