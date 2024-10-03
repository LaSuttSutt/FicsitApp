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
        var itemImages =
            AssetLoader.GetAssets(new Uri("avares://Client/Assets/Items/"), new Uri("avares://Client/")).ToList();
        var machineImages =
            AssetLoader.GetAssets(new Uri("avares://Client/Assets/Machines/"), new Uri("avares://Client/")).ToList();

        
        InitImagePart(ItemImages, "./_ImgItems", itemImages);
        InitImagePart(MachineImages, "./_ImgMachines", machineImages);
    }

    private static void InitImagePart(List<FicsitImage> imgList, string relativeFolderPath, List<Uri> assetImages)
    {
        var directory = Directory.CreateDirectory(relativeFolderPath);
        
        // Sync Assets/Folder
        foreach (var uri in assetImages)
        {
            var fileName = uri.Segments[^1];
            if (directory.GetFiles().Any(fi => fi.Name == fileName))
                continue;
            
            Bitmap bitmap = new(AssetLoader.Open(uri));
            bitmap.Save(directory.FullName + "\\" + fileName);
        }
        
        foreach (var imgFile in directory.GetFiles("*.png").OrderBy(fi => fi.Name))
        {
            var image = new Bitmap(imgFile.FullName);
            Images.Add(imgFile.Name, image);
            imgList.Add(new FicsitImage(imgFile.Name, image));
        }
    }
    
    #endregion
}