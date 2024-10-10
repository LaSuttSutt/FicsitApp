using System.Collections.Generic;
using Shared.DomainModel;

namespace Client.Ui.Projects.Planning.Calculation.DataObjects;

public class CalculationItem
{
    public Item Item { get; set; }
    public List<ItemProduction> ItemProductions { get; set; }
    public decimal RequiredAmount { get; set; }
}