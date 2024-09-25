using Avalonia;
using Client.Ui.Shared;
using Shared.DomainModel;

namespace Client.Ui.Projects.Creation;

public class CreateProjectViewModel(Project project) : DialogWindowModel
{
    public Project Project { get; set; } = project;

    public override Size Size { get; set; } = new Size(295, 85);

    public override void DoSaving()
    {
        // Nothing to to here...
    }
}