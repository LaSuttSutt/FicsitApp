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

    public static Machine Clone(this Machine machine)
    {
        return new Machine
        {
            Id = machine.Id,
            Name = machine.Name,
            ImageName = machine.ImageName,
            ItemInputs = machine.ItemInputs,
            ByProducts = machine.ByProducts
        };
    }

    public static void Update(this Machine machine, Machine clone)
    {
        machine.Name = clone.Name;
        machine.ImageName = clone.ImageName;
        machine.ItemInputs = clone.ItemInputs;
        machine.ByProducts = clone.ByProducts;
    }
}