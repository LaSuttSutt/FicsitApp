using Microsoft.EntityFrameworkCore;
using Shared.DomainModel;

namespace Shared.DataAccess;

public class FicsitDbContext : DbContext
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Recipe> Recipes { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./FicsitApp.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}