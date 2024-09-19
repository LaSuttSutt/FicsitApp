using Microsoft.EntityFrameworkCore;
using Shared.DomainModel;

namespace Shared.DataAccess;

public class FicsitDbContext : DbContext
{
    public DbSet<Machine> Machines { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=./FicsitApp.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}