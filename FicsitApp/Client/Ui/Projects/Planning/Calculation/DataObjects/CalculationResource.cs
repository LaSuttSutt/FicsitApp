using System.Collections.Generic;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning.Calculation.DataObjects;

public class CalculationResource
{
    public Item Item { get; set; }
    public List<ResourceProduction> ResourceProductions { get; set; }
    public decimal RequiredAmount { get; set; }
}