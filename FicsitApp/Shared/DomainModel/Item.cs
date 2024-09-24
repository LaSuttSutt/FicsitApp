using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Items")]
public class Item
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    
    [MaxLength(150)]
    public string Name { get; set; } = "New item";
    
    [MaxLength(40)]
    public string ShortName { get; set; } = "";
    
    [MaxLength(100)]
    public string ImageName { get; set; } = "avares://Assets/ImageDb/_default_65.png";
    
    public bool IsResource { get; set; }
    
    public bool HasShortName => !string.IsNullOrWhiteSpace(ShortName);
    public string ShortNameOrName => HasShortName ? ShortName : Name;
}