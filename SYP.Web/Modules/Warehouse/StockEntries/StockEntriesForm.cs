using _Ext;

namespace SYP.Warehouse.Forms;

[FormScript("Warehouse.StockEntries")]
[BasedOnRow(typeof(StockEntriesRow), CheckNames = true)]
public class StockEntriesForm
{
    [Tab("Genel Bilgiler")]
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

    [Tab("Ürünler")]
    [StockEntryDetailsEditor, IgnoreName]
    public List<StockEntryDetailsRow> DetailList { get; set; }

    [Tab("Dosyalar")]
    [FullWidth]
    public string Attachments { get; set; }
}
