using Avalonia;

namespace Client.Ui.Shared.Dialogs;

public interface IModalWindowModel
{
    public string Title { get; }
    public Size Size { get; }
}