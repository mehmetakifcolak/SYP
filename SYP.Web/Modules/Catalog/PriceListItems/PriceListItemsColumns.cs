using Serenity.ComponentModel;

namespace SYP.Catalog.Columns;

[ColumnsScript("Catalog.PriceListItems")]
[BasedOnRow(typeof(PriceListItemsRow), CheckNames = true)]
public class PriceListItemsColumns
{
    [EditLink, Width(100)]
    public string ProductCode { get; set; }

    [Width(250)]
    public string ProductName { get; set; }

    [Width(120), AlignRight]
    public decimal UnitPrice { get; set; }

    [Width(100), AlignRight]
    public decimal DiscountRate { get; set; }

    [Width(200)]
    public string Notes { get; set; }
}
