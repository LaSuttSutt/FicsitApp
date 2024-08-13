using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Item")]
public class Item
{
    [Key] public Guid Id { get; init; }
    public string Name { get; init; } = "New item";
    public string ImageName { get; init; } = "NoImage.png";
    public bool IsResource { get; init; }
}
