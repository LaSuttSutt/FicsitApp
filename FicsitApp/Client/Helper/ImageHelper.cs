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
        }
        
        InitMachines();
    }

    private static void InitMachines()
    {
        AddImageToList(MachineImages, "G_Constructor_64");
        AddImageToList(MachineImages, "G_Assembler_64");
        AddImageToList(MachineImages, "G_Blender_64");
        AddImageToList(MachineImages, "G_Manufacturer_64");
        AddImageToList(MachineImages, "A01_default_64");
    }

    private static void AddImageToList(List<FicsitImage> images, string imageName)
    {
        var path = "avares://Client/Assets/ImageDb/" + imageName + ".png";
        images.Add(new FicsitImage(path, Images[path]));
    }

    #endregion
}