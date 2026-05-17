using Serenity.ComponentModel;
using System.ComponentModel;

namespace SYP.Warehouse;

[EnumKey("Warehouse.StockEntryStatus"), ScriptInclude]
public enum StockEntryStatus
{
    [Description("Taslak")]
    Draft = 0,
    [Description("Onaylandı")]
    Approved = 1,
    [Description("İptal")]
    Cancelled = 2
}

[EnumKey("Warehouse.StockExitStatus"), ScriptInclude]
public enum StockExitStatus
{
    [Description("Taslak")]
    Draft = 0,
    [Description("Onaylandı")]
    Approved = 1,
    [Description("İptal")]
    Cancelled = 2
}
