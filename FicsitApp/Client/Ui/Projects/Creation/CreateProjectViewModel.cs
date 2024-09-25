using Avalonia;
using Client.Shared.View;
using Client.Ui.Shared.Dialogs;
using Shared.DomainModel;

namespace Client.Ui.Projects.Creation;

public class CreateProjectViewModel(Project project) : ViewModelBase, ISaveCancelViewModel
{
    public Project Project { get; } = project;

    public Size Size { get; set; } = new Size(295, 85);

    public string Title { get; set; } = "Ficsit App - Project";

    public bool DoSaving()
    {
        // Nothing to do here
        return true;
    }
}