using Avalonia;
using Client.Shared.View;

namespace Client.Ui.Shared;

public abstract class DialogWindowModel : ViewModelBase
{
    public abstract void DoSaving();
    public abstract Size Size { get; set; }
}