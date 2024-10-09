using System;
using Shared.DataAccess;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public static class ItemProductionExtensions
{
    public static decimal Amount(this ItemProduction itemProduction)
    {
        var recipe = DataAccess.GetEntity<Recipe>(itemProduction.RecipeId);
        if (recipe == null)
            throw new NullReferenceException($"Recipe with id {itemProduction.RecipeId} not found");
        
        var workloadFactor = itemProduction.Workload / 100m;
        
        return Math.Round(recipe.Amount * workloadFactor * itemProduction.MachineCount, 2,
            MidpointRounding.AwayFromZero);
    }
}