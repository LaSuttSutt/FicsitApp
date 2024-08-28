using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Media.Imaging;
using Client.Helper;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public class ItemViewModel : AvaloniaObject
{
    public Guid Id { get; init; }
    public string Name { get; init; }
    public Bitmap Image { get; init; }
    public bool IsResource { get; init; }
    public List<Recipe> Recipes { get; init; } = [];

    public ItemViewModel()
    {
        Id = Guid.NewGuid();
        Name = "Name";
        Image = ImageHelper.Images["avares://Client/Assets/ImageDb/A01_default_64.png"];
        IsResource = false;
    }

    public ItemViewModel(Item item)
    {
        Id = item.Id;
        Name = item.Name;
        Image = ImageHelper.Images[item.ImageName];
        IsResource = item.IsResource;
        Recipes = item.Recipes;
    }
}

public static class ItemExtensions
{
    public static ItemViewModel ViewModel(this Item item)
    {
        return new ItemViewModel(item);
    }
}