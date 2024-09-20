using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace Client.Helper;

public static class ImageHelper
{
    #region Declaration -------------------------

    public static Bitmap DefaultImage { get; } = 
        new(AssetLoader.Open(new Uri("avares://Client/Assets/ImageDb/Default.png")));
    public static Bitmap DefaultItem { get; } =
        new(AssetLoader.Open(new Uri("avares://Client/Assets/ImageDb/Screws.png")));
    public static Bitmap DefaultMachine { get; } = 
        new(AssetLoader.Open(new Uri("avares://Client/Assets/ImageDb/Constructor.png")));
    
    public static Dictionary<string, Bitmap> Images { get; } = [];
    public static List<FicsitImage> ItemImages { get; } = [];
    public static List<FicsitImage> MachineImages { get; } = [];
    
    #endregion

    #region Static Methods ----------------------

    public static void Initialize()
    {
        InitImagePart(ItemImages, "./_ImgItems");
        InitImagePart(MachineImages, "./_ImgMachines");
    }

    private static void InitImagePart(List<FicsitImage> imgList, string relativeFolderPath)
    {
        var directory = Directory.CreateDirectory(relativeFolderPath);
        foreach (var imgFile in directory.GetFiles("*.png").OrderBy(fi => fi.Name))
        {
            var image = new Bitmap(imgFile.FullName);
            Images.Add(imgFile.Name, image);
            imgList.Add(new FicsitImage(imgFile.Name, image));
        }
    }
    
    #endregion
}