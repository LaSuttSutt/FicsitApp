using System.ComponentModel.DataAnnotations;

namespace Shared.DomainModel;

public class ResourceProduction
{
    [Key] public Guid Id { get; init; } = Guid.NewGuid();
    public Guid FactoryId { get; set; }
    public Guid ItemId { get; set; }
    public Purity Purity { get; set; }
    public Extractor Extractor { get; set; }
    public decimal ExtractorCount { get; set; }
    public decimal Workload { get; set; }
}

public enum Purity
{
    Impure,
    Normal,
    Pure
}

public enum Extractor
{
    MinerMk1,
    MinerMk2,
    MinerMk3,
    OilExtractor,
    WellExtractor
}