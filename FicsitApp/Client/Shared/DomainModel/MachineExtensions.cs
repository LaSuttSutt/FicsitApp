using Avalonia.Media.Imaging;
using Client.Helper;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public static class MachineExtensions
{
    public static Bitmap Image(this Machine machine)
    {
        ImageHelper.Images.TryGetValue(machine.ImageName, out var image);
        return image ?? ImageHelper.DefaultImage;
    }
}