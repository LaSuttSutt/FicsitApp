using Avalonia.Media.Imaging;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public class RecipeListModel(Recipe recipe, Bitmap image)
{
    public Recipe Recipe { get; set; } = recipe;
    public Bitmap Image { get; set; } = image;
}