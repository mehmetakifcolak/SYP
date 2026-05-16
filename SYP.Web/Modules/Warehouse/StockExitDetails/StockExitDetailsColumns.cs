namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.StockExitDetails")]
[BasedOnRow(typeof(StockExitDetailsRow), CheckNames = true)]
public class StockExitDetailsColumns
{
    [EditLink, Width(120)]
    public string ProductCode { get; set; }

    [Width(250)]
    public string ProductName { get; set; }

    [Width(120), AlignRight]
    public decimal Quantity { get; set; }

    [Width(200)]
    public string Notes { get; set; }
}
