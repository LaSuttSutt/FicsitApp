using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Client.Shared.View;

namespace Client.Ui.Database;

public class DatabaseWindowViewModel : ViewModelBase
{
    public List<Tuple<Bitmap, string>> Items { get; } = [];
    public List<Uri> ItemImages { get; } = AssetLoader.GetAssets(new Uri("avares://Client/Assets/ImageDb/"), 
        new Uri("avares://Client/")).ToList();

    public DatabaseWindowViewModel()
    {
        foreach (var itemImage in ItemImages)
        {
            Items.Add(new Tuple<Bitmap, string>(new Bitmap(AssetLoader.Open(new Uri(itemImage.OriginalString))), itemImage.LocalPath));
        }
    }
}