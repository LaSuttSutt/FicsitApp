using Avalonia.Controls;

namespace Client.Ui.Projects.Planning;

public partial class PlanningView : UserControl
{
    private PlanningViewModel? Vm => DataContext as PlanningViewModel;
    
    public PlanningView()
    {
        InitializeComponent();
    }
}