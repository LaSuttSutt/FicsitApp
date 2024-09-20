using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Ingredients")]
public class Ingredient
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public Guid RecipeId { get; set; }
    public Guid ItemId { get; set; }
    public decimal Amount { get; set; }
    public bool IsByProduct { get; set; }
}