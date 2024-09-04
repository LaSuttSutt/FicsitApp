using System;
using System.Linq;
using Avalonia.Media.Imaging;
using Client.Helper;
using Client.Shared.DomainModel;
using Client.Shared.View;
using Shared.TestData;

namespace Client.Ui.Database;

public class DbItemListEntryViewModel : ViewModelBase
{
    public Guid ItemId { get; set; }
    public string Name { get; init; }
    public Bitmap Image { get; init; }

    public DbItemListEntryViewModel(Guid itemId)
    {
        ItemId = itemId;
        
        var item = ItemDatabase.Items.FirstOrDefault(i => i.Id == itemId);
        Name = item?.Name ?? "<Item not found>";
        Image = item?.Image() ?? ImageHelper.DefaultImage;
    }
}