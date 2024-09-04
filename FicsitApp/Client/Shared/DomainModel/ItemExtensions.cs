using Avalonia.Media.Imaging;
using Client.Helper;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public static class ItemExtensions
{
    public static Bitmap Image(this Item item)
    {
        ImageHelper.Images.TryGetValue(item.ImageName, out var image);
        return image ?? ImageHelper.DefaultImage;
    }
}