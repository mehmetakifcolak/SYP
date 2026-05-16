namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.StockMovements")]
[BasedOnRow(typeof(StockMovementsRow), CheckNames = true)]
public class StockMovementsColumns
{
    [Width(80)]
    public string MovementType { get; set; }

    [Width(120)]
    public string DocumentNo { get; set; }

    [Width(150), DisplayFormat("dd.MM.yyyy HH:mm")]
    public DateTime MovementDate { get; set; }

    [Width(150)]
    public string WarehouseName { get; set; }

    [Width(100)]
    public string ProductCode { get; set; }

    [Width(200)]
    public string ProductName { get; set; }

    [Width(100), AlignRight]
    public decimal Quantity { get; set; }

    [Width(80)]
    public short Status { get; set; }

    [Width(200)]
    public string Description { get; set; }
}
