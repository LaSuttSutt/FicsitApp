using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Machines")]
public class Machine
{
    [Key] public Guid Id { get; init; }
    public string Name { get; init; } = "New machine";
    public string ImageName { get; init; } = "avares://Assets/ImageDb/_default_65.png";
    public int ItemInputs { get; init; } = 1;
    public int ByProducts { get; init; } = 0;
}