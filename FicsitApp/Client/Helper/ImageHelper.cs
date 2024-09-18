using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Client.Helper;

public static class ImageHelper
{
    #region Declaration -------------------------

    public static Bitmap DefaultImage { get; } = 
        new(AssetLoader.Open(new Uri("avares://Client/Assets/ImageDb/A01_default_64.png")));
    
    public static Bitmap DefaultMachine { get; } =
        new(AssetLoader.Open(new Uri("avares://Client/Assets/ImageDb/G_Constructor_64.png")));
    
    public static Dictionary<string, Bitmap> Images { get; } = [];
    public static List<FicsitImage> MachineImages { get; } = [];
    
    #endregion

    #region Static Methods ----------------------

    public static void Initialize()
    {
        var imageUris =
            AssetLoader.GetAssets(new Uri("avares://Client/Assets/ImageDb/"), new Uri("avares://Client/")).ToList();
        
        foreach (var uri in imageUris)
        {
            var path = uri.OriginalString;
            var image = new Bitmap(AssetLoader.Open(new Uri(uri.OriginalString)));
            
            Images.Add(path, image);
            
            if (path.Contains("G_") || uri.OriginalString.Contains("A01_"))
                MachineImages.Add(new FicsitImage(path, image));
        }
    }

    #endregion
}