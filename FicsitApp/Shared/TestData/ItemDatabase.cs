using Shared.DomainModel;

namespace Shared.TestData;

public static class ItemDatabase
{
    public static List<Item> Items { get; } = [];
    public static List<Machine> Machines { get; } = [];
    public static List<Ingredient> Ingredients { get; } = [];
    public static List<Recipe> Recipes { get; } = [];

    public static void Initialize()
    {
        BuildTestData();
    }
    
    private static void BuildTestData()
    {
        CreateIronItems();
        CreateMachines();
    }

    private static void CreateIronItems()
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
        Recipes.Add(ironIngotRecipe);

        var ingredient1 = new Ingredient
        {
            Id = Guid.NewGuid(),
            RecipeId = ironIngotRecipe.Id,
            Amount = 30.0m,
            ItemId = ironOre.Id
        };
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
        Recipes.Add(ironPlateRecipe);

        ingredient1 = new Ingredient
        {
            Id = Guid.NewGuid(),
            RecipeId = ironPlateRecipe.Id,
            Amount = 30.0m,
            ItemId = ironIngot.Id
        };
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
        Recipes.Add(ironRodRecipe);

        ingredient1 = new Ingredient()
        {
            Id = Guid.NewGuid(),
            RecipeId = ironRodRecipe.Id,
            Amount = 15.0m,
            ItemId = ironIngot.Id
        };
        Ingredients.Add(ingredient1);

        ingredient1 = new Ingredient()
        {
            Id = Guid.NewGuid(),
            RecipeId = ironRodRecipe.Id,
            Amount = 5.0m,
            ItemId = ironOre.Id,
            IsByProduct = true
        };
        Ingredients.Add(ingredient1);
    }

    private static void CreateMachines()
    {
        Machines.Add(new Machine()
        {
            Id = Guid.NewGuid(),
            Name = "Constructor",
            ImageName = "avares://Client/Assets/ImageDb/G_Constructor_64.png",
            ItemInputs = 1
        });
        
        Machines.Add(new Machine
        {
            Id = Guid.NewGuid(),
            Name = "Assembler",
            ImageName = "avares://Client/Assets/ImageDb/G_Assembler_64.png",
            ItemInputs = 2
        });
    }
}