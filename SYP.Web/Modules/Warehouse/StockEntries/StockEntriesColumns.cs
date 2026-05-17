
namespace SYP.Warehouse.Columns;

[ColumnsScript("Warehouse.StockEntries")]
[BasedOnRow(typeof(StockEntriesRow), CheckNames = true)]
public class StockEntriesColumns
{
    [EditLink, Width(120)]
    public string EntryNo { get; set; }

    [Width(150)]
    public string WarehouseName { get; set; }

    [Width(150), DisplayFormat("dd.MM.yyyy HH:mm")]
    public DateTime EntryDate { get; set; }

    [Width(100)]
    public StockEntryStatus Status { get; set; }

    [Width(250)]
    public string Description { get; set; }

    [Width(150), DisplayFormat("dd.MM.yyyy HH:mm")]
    public DateTime InsertDate { get; set; }
}
