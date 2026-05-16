using Serenity.ComponentModel;

namespace SYP.Catalog.Columns;

[ColumnsScript("Catalog.PriceLists")]
[BasedOnRow(typeof(PriceListsRow), CheckNames = true)]
public class PriceListsColumns
{
    [EditLink, Width(100)]
    public string Code { get; set; }

    [EditLink, Width(200)]
    public string Name { get; set; }

    [Width(100)]
    public string CurrencyCode { get; set; }

    [Width(120)]
    public DateTime ValidFrom { get; set; }

    [Width(120)]
    public DateTime ValidTo { get; set; }

    [Width(80)]
    public short IsActive { get; set; }

    [Width(80)]
    public bool IsDefault { get; set; }
}
