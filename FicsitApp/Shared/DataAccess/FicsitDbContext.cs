using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Shared.DomainModel;

namespace Shared.DataAccess;

public class FicsitDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Factory> Factories { get; set; }
    public DbSet<ItemProduction> ItemProductions { get; set; }
    public DbSet<ResourceProduction> ResourceProductions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./FicsitApp.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
    
    public void ExportItemDatabase(string filePath)
    {
        var fileNameItems = Path.Combine(filePath, "FicsitDataItems.json");
        var fileNameMachines = Path.Combine(filePath, "FicsitDataMachines.json");
        var fileNameRecipes = Path.Combine(filePath, "FicsitDataRecipes.json");
        var fileNameIngredients = Path.Combine(filePath, "FicsitDataIngredients.json");
        
        var jsonItems = JsonSerializer.Serialize(Items.ToList());
        var jsonMachines = JsonSerializer.Serialize(Machines.ToList());
        var jsonRecipes = JsonSerializer.Serialize(Recipes.ToList());
        var jsonIngredients = JsonSerializer.Serialize(Ingredients.ToList());
        
        File.WriteAllText(fileNameItems, jsonItems);
        File.WriteAllText(fileNameMachines, jsonMachines);
        File.WriteAllText(fileNameRecipes, jsonRecipes);
        File.WriteAllText(fileNameIngredients, jsonIngredients);
    }

    public void ImportItemDatabase(string filePath)
    {
        var fileNameItems = Path.Combine(filePath, "FicsitDataItems.json");
        var fileNameMachines = Path.Combine(filePath, "FicsitDataMachines.json");
        var fileNameRecipes = Path.Combine(filePath, "FicsitDataRecipes.json");
        var fileNameIngredients = Path.Combine(filePath, "FicsitDataIngredients.json");

        if (!File.Exists(fileNameItems) || 
            !File.Exists(fileNameMachines) || 
            !File.Exists(fileNameRecipes) ||
            !File.Exists(fileNameIngredients))
            return;

        var items = JsonSerializer.Deserialize<List<Item>>(File.ReadAllText(fileNameItems));
        var machines = JsonSerializer.Deserialize<List<Machine>>(File.ReadAllText(fileNameMachines));
        var recipes = JsonSerializer.Deserialize<List<Recipe>>(File.ReadAllText(fileNameRecipes));
        var ingredients = JsonSerializer.Deserialize<List<Ingredient>>(File.ReadAllText(fileNameIngredients));

        Items.ExecuteDelete();
        Machines.ExecuteDelete();
        Recipes.ExecuteDelete();
        Ingredients.ExecuteDelete();

        Items.AddRange(items ?? []);
        Machines.AddRange(machines ?? []);
        Recipes.AddRange(recipes ?? []);
        Ingredients.AddRange(ingredients ?? []);
        SaveChanges();
    }
}