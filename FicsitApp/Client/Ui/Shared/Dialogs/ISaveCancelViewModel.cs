using Avalonia;
using Client.Shared.View;

namespace Client.Ui.Shared.Dialogs;

public interface ISaveCancelViewModel
{
    public string Title { get; }
    public Size Size { get; }
    public bool DoSaving();
}