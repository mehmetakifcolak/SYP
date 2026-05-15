namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.StockEntryDetails")]
[BasedOnRow(typeof(StockEntryDetailsRow), CheckNames = true)]
public class StockEntryDetailsColumns
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
