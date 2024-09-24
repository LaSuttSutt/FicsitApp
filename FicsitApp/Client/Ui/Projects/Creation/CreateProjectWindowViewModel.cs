using System.Reactive;
using Client.Ui.Shared;
using ReactiveUI;
using Shared.DomainModel;

namespace Client.Ui.Projects.Creation;

public class CreateProjectWindowViewModel(Project project)
{
    public Project Project { get; } = project;
    public ReactiveCommand<Unit, ShowDialogResult> SaveProjectCommand { get; } = ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Ok));

    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } = 
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));
}