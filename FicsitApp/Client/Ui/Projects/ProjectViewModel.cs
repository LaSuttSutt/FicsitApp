using Avalonia;
using Client.Shared.View;
using Client.Ui.Projects.Planning;
using Client.Ui.Shared.Dialogs;
using Shared.DomainModel;

namespace Client.Ui.Projects;

public class ProjectViewModel(Project project) : ViewModelBase, IModalWindowModel
{
    public string Title => "Ficsit App - Project";
    public Size Size { get; } = new Size(984, 697);
    private Project _project = project;
    public PlanningViewModel PlanningViewModel { get; } = new PlanningViewModel();
}