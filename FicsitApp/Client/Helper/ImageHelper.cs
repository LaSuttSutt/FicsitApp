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
    
    #endregion

    #region Static Methods ----------------------

    public static void Initialize()
    {
        var imageUris =
            AssetLoader.GetAssets(new Uri("avares://Client/Assets/ImageDb/"), new Uri("avares://Client/")).ToList();
        
        foreach (var uri in imageUris)
        {
            Images.Add(uri.OriginalString, new Bitmap(AssetLoader.Open(new Uri(uri.OriginalString))));
        }
    }

    #endregion
}