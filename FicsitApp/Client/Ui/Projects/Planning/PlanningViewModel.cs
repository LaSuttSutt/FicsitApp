using System;
using System.Linq;
using System.Windows.Input;
using Client.Shared.View;
using Client.Ui.Projects.Creation;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning;

public class PlanningViewModel : ViewModelBase
{
    public ICommand DialogCheck { get; set; }

    public PlanningViewModel()
    {
        DialogCheck = ReactiveCommand.Create(DoDialogCheck);
    }

    private async void DoDialogCheck()
    {
        var project = DataAccess.GetEntities<Project>().First();
        var viewModel = new CreateProjectViewModel(project);
        
        var result = await DialogWindow.Show<ProjectWindow>(viewModel, "Test");
        Console.WriteLine(result);
    }
}