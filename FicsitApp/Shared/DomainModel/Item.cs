using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Items")]
public class Item
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public string Name { get; set; } = "New item";
    public string ShortName { get; set; } = "";
    public string ImageName { get; set; } = "avares://Assets/ImageDb/_default_65.png";
    public bool IsResource { get; set; }
    public bool HasShortName => !string.IsNullOrWhiteSpace(ShortName);
}