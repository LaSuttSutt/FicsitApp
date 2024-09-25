using System.Reactive;
using Client.Shared.View;
using ReactiveUI;

namespace Client.Ui.Shared.Dialogs;

public class SaveCancelDialogModel(ISaveCancelViewModel saveCancelViewModel)
{
    public ViewModelBase ViewModel { get; set; } = (ViewModelBase)saveCancelViewModel;
    
    public ReactiveCommand<Unit, ShowDialogResult> SaveCommand { get; } =
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Ok));

    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } =
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));
}