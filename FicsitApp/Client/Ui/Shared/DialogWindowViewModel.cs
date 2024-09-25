using System.Reactive;
using Client.Shared.View;
using ReactiveUI;

namespace Client.Ui.Shared;

public class DialogWindowViewModel(ViewModelBase viewModel) : ViewModelBase
{
    public ViewModelBase ViewModel { get; set; } = viewModel;
    
    public ReactiveCommand<Unit, ShowDialogResult> SaveCommand { get; } =
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Ok));

    public ReactiveCommand<Unit, ShowDialogResult> CancelCommand { get; } =
        ReactiveCommand.Create(() => new ShowDialogResult(DialogResult.Cancel));
}