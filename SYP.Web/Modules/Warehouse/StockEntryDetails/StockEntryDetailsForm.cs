namespace SYP.Warehouse.Forms;

[FormScript("Warehouse.StockEntryDetails")]
[BasedOnRow(typeof(StockEntryDetailsRow), CheckNames = true)]
public class StockEntryDetailsForm
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public string Notes { get; set; }
}
