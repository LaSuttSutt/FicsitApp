using Avalonia;

namespace Client.Ui.Shared;

public class ShowDialogResult(DialogResult result) : AvaloniaObject
{
    public DialogResult Result { get; set; } = result;
}

public enum DialogResult
{
    Ok,
    Cancel
}