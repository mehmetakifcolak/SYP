namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.WarehouseStock")]
[BasedOnRow(typeof(WarehouseStockRow), CheckNames = true)]
public class WarehouseStockColumns
{
    [Width(120)]
    public string WarehouseCode { get; set; }

    [Width(150)]
    public string WarehouseName { get; set; }

    [Width(120)]
    public string ProductCode { get; set; }

    [Width(250)]
    public string ProductName { get; set; }

    [Width(120), AlignRight]
    public decimal Quantity { get; set; }

    [Width(150)]
    public DateTime LastUpdateDate { get; set; }
}
