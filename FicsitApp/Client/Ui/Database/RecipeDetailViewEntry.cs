using Avalonia.Media.Imaging;
using Client.Helper;

namespace Client.Ui.Database;

public class RecipeDetailViewEntry
{
    public Bitmap Image { get; set; } = ImageHelper.DefaultImage;
    public string ItemName { get; set; } = string.Empty;
    public decimal AmountPerMinute { get; set; }
}