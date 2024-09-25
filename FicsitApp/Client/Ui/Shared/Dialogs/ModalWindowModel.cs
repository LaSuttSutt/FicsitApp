using Client.Shared.View;

namespace Client.Ui.Shared.Dialogs;

public class ModalWindowModel(IModalWindowModel modalWindowModel) : ViewModelBase
{
    public ViewModelBase ViewModel { get; set; } = (ViewModelBase)modalWindowModel;
}