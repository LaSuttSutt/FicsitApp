using System.ComponentModel.DataAnnotations;

namespace Shared.DomainModel;

public class Ingredient
{
    [Key]
    public Guid Id { get; init; }
    public Guid RecipeId { get; init; }
    public Guid ItemId { get; init; }
    public decimal Amount { get; init; }
    public bool IsByProduct { get; init; }
}