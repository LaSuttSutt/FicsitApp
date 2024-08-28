using Client.Shared.DomainModel;
using Client.Shared.View;

namespace Client.Ui.Database;

public class DbItemListEntryViewModel : ViewModelBase
{
    public ItemViewModel Item { get; set; } = new();
}