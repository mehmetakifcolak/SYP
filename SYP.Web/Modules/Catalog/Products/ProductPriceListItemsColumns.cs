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

    [Width(120), AlignRight, DisplayFormat("#,##0.0000")]
    public decimal UnitPrice { get; set; }

    [Width(100), AlignRight, DisplayFormat("#,##0.00")]
    public decimal DiscountRate { get; set; }

    [Width(110), DisplayFormat("dd.MM.yyyy")]
    public DateTime PriceListValidFrom { get; set; }

    [Width(110), DisplayFormat("dd.MM.yyyy")]
    public DateTime PriceListValidTo { get; set; }

    [Width(60)]
    public short PriceListIsActive { get; set; }

    [Width(80)]
    public bool PriceListIsDefault { get; set; }

    [Width(200)]
    public string Notes { get; set; }
}
