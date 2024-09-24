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
}