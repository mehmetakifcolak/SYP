using Serenity.ComponentModel;

namespace SYP.Catalog.Columns;

[ColumnsScript("Catalog.ProductPriceListItems")]
[BasedOnRow(typeof(PriceListItemsRow), CheckNames = true)]
public class ProductPriceListItemsColumns
{
    [EditLink, Width(100)]
    public string PriceListCode { get; set; }

    [Width(200)]
    public string PriceListName { get; set; }

    [Width(120), AlignRight]
    public decimal UnitPrice { get; set; }

    [Width(100), AlignRight]
    public decimal DiscountRate { get; set; }

    [Width(200)]
    public string Notes { get; set; }
}
