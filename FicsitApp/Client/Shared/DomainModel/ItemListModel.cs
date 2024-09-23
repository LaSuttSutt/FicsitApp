using Avalonia.Media.Imaging;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public class ItemListModel(Item item, Bitmap image)
{
    public Item Item { get; set; } = item;
    public Bitmap Image { get; set; } = image;
}