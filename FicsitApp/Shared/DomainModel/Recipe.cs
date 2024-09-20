using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Recipes")]
public class Recipe
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ItemId { get; set; }
    public Guid MachineId { get; set; }
    public decimal Amount { get; set; }
    public string Name { get; set; } = "New recipe";
}