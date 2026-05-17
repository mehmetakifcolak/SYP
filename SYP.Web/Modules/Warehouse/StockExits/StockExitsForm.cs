
namespace SYP.Warehouse.Forms;

[FormScript("Warehouse.StockExits")]
[BasedOnRow(typeof(StockExitsRow), CheckNames = true)]
public class StockExitsForm
{
    [Tab("General Information")]
    [HalfWidth]
    public string ExitNo { get; set; }

    [HalfWidth]
    public int WarehouseId { get; set; }

    [HalfWidth, DateTimeEditor]
    public DateTime ExitDate { get; set; }

    [HalfWidth]
    public StockExitStatus Status { get; set; }

    [FullWidth]
    public string Description { get; set; }

    [Tab("Products")]
    [StockExitDetailsEditor, IgnoreName]
    public List<StockExitDetailsRow> DetailList { get; set; }

    [Tab("Files")]
    [FullWidth]
    public string Attachments { get; set; }
}
