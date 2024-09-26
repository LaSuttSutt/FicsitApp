using System.Collections.Generic;
using Avalonia.Media.Imaging;
using Client.Helper;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public static class RecipeExtensions
{
    public static Recipe Clone(this Recipe recipe)
    {
        return new Recipe
        {
            Id = recipe.Id,
            Name = recipe.Name,
            ItemId = recipe.ItemId,
            Amount = recipe.Amount,
            MachineId = recipe.MachineId
        };
    }

    public static void Update(this Recipe recipe, Recipe clone)
    {
        recipe.Name = clone.Name;
        recipe.ItemId = clone.ItemId;
        recipe.Amount = clone.Amount;
        recipe.MachineId = clone.MachineId;
    }
    
    public static Bitmap Image(this Recipe recipe)
    {
        var machine = DataAccess.GetEntity<Machine>(recipe.MachineId);
        if (machine == null) return ImageHelper.DefaultImage;
        ImageHelper.Images.TryGetValue(machine.ImageName, out var image);
        return image ?? ImageHelper.DefaultImage;
    }
    
    public static List<RecipeListModel> ToRecipeList(this List<Recipe> recipes)
    {
        var list = new List<RecipeListModel>();
        recipes.ForEach(r => list.Add(new RecipeListModel(r, r.Image())));
        return list;
    }
}