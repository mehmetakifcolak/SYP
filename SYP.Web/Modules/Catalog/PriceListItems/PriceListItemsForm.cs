using Serenity.ComponentModel;

namespace SYP.Catalog.Forms;

[FormScript("Catalog.PriceListItems")]
[BasedOnRow(typeof(PriceListItemsRow), CheckNames = true)]
public class PriceListItemsForm
{
    public int ProductId { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal DiscountRate { get; set; }
    public string Notes { get; set; }
}
