using System;
using System.ComponentModel;
using Shared.DomainModel;

namespace Client.Shared.DomainModel;

public static class ResourceProductionExtensions
{
    public static decimal Amount(this ResourceProduction resourceProduction)
    {
        var items = resourceProduction.Purity.PurityAmount();
        var extractorFactor = resourceProduction.Extractor.ExtractorFactor();
        var workloadFactor = resourceProduction.Workload / 100m;

        return Math.Round(items * extractorFactor * workloadFactor * resourceProduction.ExtractorCount, 2,
            MidpointRounding.AwayFromZero);
    }

    private static decimal PurityAmount(this Purity purity)
    {
        return purity switch
        {
            Purity.Impure => 30m,
            Purity.Normal => 60m,
            Purity.Pure => 120m,
            _ => throw new InvalidEnumArgumentException()
        };
    }

    private static decimal ExtractorFactor(this Extractor extractor)
    {
        return extractor switch
        {
            Extractor.MinerMk1 => 1m,
            Extractor.MinerMk2 => 2m,
            Extractor.MinerMk3 => 4m,
            Extractor.OilExtractor => 2m,
            Extractor.WellExtractor => 1m,
            _ => throw new InvalidEnumArgumentException()
        };
    }
}