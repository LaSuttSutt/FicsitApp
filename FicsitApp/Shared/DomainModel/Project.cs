using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Projects")]
public class Project
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    
    [MaxLength(150)] 
    public string Name { get; set; } = "New project";
}