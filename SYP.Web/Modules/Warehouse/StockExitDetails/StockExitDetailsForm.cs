namespace SYP.Warehouse.Forms;

[FormScript("Warehouse.StockExitDetails")]
[BasedOnRow(typeof(StockExitDetailsRow), CheckNames = true)]
public class StockExitDetailsForm
{
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public string Currency { get; set; }
    public decimal UnitPrice { get; set; }
    public string Notes { get; set; }
}
