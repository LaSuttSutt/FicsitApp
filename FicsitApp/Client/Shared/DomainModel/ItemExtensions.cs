using System.Collections.Generic;
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
    
    public static Item Clone(this Item item)
    {
        return new Item
        {
            Id = item.Id,
            Name = item.Name,
            ShortName = item.ShortName,
            ImageName = item.ImageName,
            IsResource = item.IsResource
        };
    }

    public static void Update(this Item item, Item clone)
    {
        item.Name = clone.Name;
        item.ImageName = clone.ImageName;
        item.ShortName = clone.ShortName;
        item.IsResource = clone.IsResource;
    }

    public static List<ItemListModel> ToItemList(this List<Item> items)
    {
        var list = new List<ItemListModel>();
        items.ForEach(i => list.Add(new ItemListModel(i, i.Image())));
        return list;
    }
}