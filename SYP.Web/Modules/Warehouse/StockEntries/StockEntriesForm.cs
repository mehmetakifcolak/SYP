
namespace SYP.Warehouse.Forms;

[FormScript("Warehouse.StockEntries")]
[BasedOnRow(typeof(StockEntriesRow), CheckNames = true)]
public class StockEntriesForm
{
    [Tab("General Information")]
    [HalfWidth]
    public string EntryNo { get; set; }

    [HalfWidth]
    public int WarehouseId { get; set; }

    [HalfWidth, DateTimeEditor]
    public DateTime EntryDate { get; set; }

    [HalfWidth]
    public StockEntryStatus Status { get; set; }

    [FullWidth]
    public string Description { get; set; }

    [Tab("Products")]
    [StockEntryDetailsEditor, IgnoreName]
    public List<StockEntryDetailsRow> DetailList { get; set; }

    [Tab("Files")]
    [FullWidth]
    public string Attachments { get; set; }
}
