using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.DomainModel;

[Table("Machines")]
public class Machine
{
    [Key] public Guid Id { get; init; }
    public string Name { get; set; } = "New machine";
    public string ImageName { get; set; } = "avares://Client/Assets/ImageDb/A01_default_64.png";
    public int ItemInputs { get; set; } = 1;
    public int ByProducts { get; set; }
}