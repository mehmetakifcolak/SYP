using Serenity.ComponentModel;
using System.ComponentModel;

namespace SYP.Catalog;

[EnumKey("Catalog.PriceListType")]
public enum PriceListType
{
    [Description("Sales Price List")]
    Sales = 1,

    [Description("Purchase Price List")]
    Purchase = 2
}
