using Shared.DomainModel;

namespace Shared.TestData;

public class ItemDatabase
{
    public List<Item> Items { get; } = [];
    public List<Ingredient> Ingredients { get; } = [];
    public List<Recipe> Recipes { get; } = [];

    public ItemDatabase()
    {
        BuildTestData();
    }

    private void BuildTestData()
    {
        CreateIronItems();
    }

    private void CreateIronItems()
    {
        var ironOre = new Item
        {
            Id = Guid.NewGuid(),
            Name = "Iron Ore",
            ImageName = "avares://Client/Assets/ImageDb/B01_iron-ore_64.png",
            IsResource = true
        };
        Items.Add(ironOre);
        
        var ironIngot = new Item
        {
            Id = Guid.NewGuid(),
            Name = "Iron Ingot",
            ImageName = "avares://Client/Assets/ImageDb/C01_iron-ingot_64.png",
            IsResource = false
        };
        Items.Add(ironIngot);

        var ironIngotRecipe = new Recipe
        {
            Id = Guid.NewGuid(),
            Name = "Standard-Recipe",
            Amount = 15.0m,
            ItemId = ironIngot.Id
        };
        ironIngot.Recipes.Add(ironIngotRecipe);
        Recipes.Add(ironIngotRecipe);

        var ingredient1 = new Ingredient
        {
            Id = Guid.NewGuid(),
            RecipeId = ironIngotRecipe.Id,
            Amount = 30.0m,
            ItemId = ironOre.Id
        };
        ironIngotRecipe.Ingredients.Add(ingredient1);
        Ingredients.Add(ingredient1);

        var ironPlate = new Item
        {
            Id = Guid.NewGuid(),
            Name = "Iron Plate",
            ImageName = "avares://Client/Assets/ImageDb/C03_iron-plate_64.png"
        };
        Items.Add(ironPlate);

        var ironPlateRecipe = new Recipe
        {
            Id = Guid.NewGuid(),
            Name = "Standard-Recipe",
            Amount = 30.0m,
            ItemId = ironPlate.Id
        };
        ironPlate.Recipes.Add(ironPlateRecipe);
        Recipes.Add(ironPlateRecipe);

        ingredient1 = new Ingredient
        {
            Id = Guid.NewGuid(),
            RecipeId = ironPlateRecipe.Id,
            Amount = 30.0m,
            ItemId = ironIngot.Id
        };
        ironIngotRecipe.Ingredients.Add(ingredient1);
        Ingredients.Add(ingredient1);

        var ironRod = new Item()
        {
            Id = Guid.NewGuid(),
            Name = "Iron Rod",
            ImageName = "avares://Client/Assets/ImageDb/C02_iron-rod_64.png"
        };
        Items.Add(ironRod);

        var ironRodRecipe = new Recipe()
        {
            Id = Guid.NewGuid(),
            Name = "Standard-Recipe",
            Amount = 45.0m,
            ItemId = ironRod.Id
        };
        ironRod.Recipes.Add(ironRodRecipe);
        Recipes.Add(ironRodRecipe);

        ingredient1 = new Ingredient()
        {
            Id = Guid.NewGuid(),
            RecipeId = ironRodRecipe.Id,
            Amount = 15.0m,
            ItemId = ironIngot.Id
        };
        ironIngotRecipe.Ingredients.Add(ingredient1);
        Ingredients.Add(ingredient1);
    }
}