using Avalonia.Media.Imaging;

namespace Client.Helper;

public class FicsitImage(string path, Bitmap image)
{
    public string ImagePath { get; set; } = path;
    public Bitmap Image { get; set; } = image;
}