using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

public class Recipe
{
    [Key] public Guid Id { get; init; }
    public Guid ItemId { get; init; }
    public decimal Amount { get; init; }
    public string Name { get; init; } = "New recipe";

    [NotMapped] public List<Ingredient> Ingredients { get; init; } = [];
}