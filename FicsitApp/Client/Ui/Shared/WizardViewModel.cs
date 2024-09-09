using System;
using Client.Shared.View;

namespace Client.Ui.Shared;

public class WizardViewModel : ViewModelBase
{
    public event EventHandler StateChanged;
    public bool CanGoForward { get; set; }
    
    protected void RaiseOnStateChanged() => StateChanged?.Invoke(this, EventArgs.Empty);
}