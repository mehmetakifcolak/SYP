namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.StockExitDetails")]
[BasedOnRow(typeof(StockExitDetailsRow), CheckNames = true)]
public class StockExitDetailsColumns
{
    [EditLink, Width(120)]
    public string ProductCode { get; set; }

    [Width(250)]
    public string ProductName { get; set; }

    [Width(100), AlignRight]
    public decimal Quantity { get; set; }

    [Width(80), AlignCenter]
    public string Currency { get; set; }

    [Width(120), AlignRight, DisplayFormat("#,##0.00")]
    public decimal UnitPrice { get; set; }

    [Width(200)]
    public string Notes { get; set; }
}
