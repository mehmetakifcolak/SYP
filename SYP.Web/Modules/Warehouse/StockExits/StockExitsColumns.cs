
namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.StockExits")]
[BasedOnRow(typeof(StockExitsRow), CheckNames = true)]
public class StockExitsColumns
{
    [EditLink, Width(120)]
    public string ExitNo { get; set; }

    [Width(150)]
    public string WarehouseName { get; set; }

    [Width(150), DisplayFormat("dd.MM.yyyy HH:mm")]
    public DateTime ExitDate { get; set; }

    [Width(100)]
    public StockExitStatus Status { get; set; }

    [Width(250)]
    public string Description { get; set; }

    [Width(150), DisplayFormat("dd.MM.yyyy HH:mm")]
    public DateTime InsertDate { get; set; }
}
