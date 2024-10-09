using System.ComponentModel.DataAnnotations;

namespace Shared.DomainModel;

public class ItemProduction
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public Guid FactoryId { get; set; }
    public Guid ItemId { get; set; }
    public Guid RecipeId { get; set; }
    public decimal MachineCount { get; set; }
    public decimal Workload { get; set; }
}