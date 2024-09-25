using Client.Shared.View;
using Client.Ui.Projects.Planning;
using Shared.DomainModel;

namespace Client.Ui.Projects;

public class ProjectWindowViewModel(Project project) : ViewModelBase
{
    private Project _project = project;
    public PlanningViewModel PlanningViewModel { get; } = new PlanningViewModel();
}