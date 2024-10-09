using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Factories")]
public class Factory
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public Guid ProjectId { get; set; }

    [MaxLength(150)] public string Name { get; set; } = "New factory";
}