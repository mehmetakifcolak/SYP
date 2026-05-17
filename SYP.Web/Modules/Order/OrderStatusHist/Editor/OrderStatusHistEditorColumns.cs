namespace SYP.Order.Columns;

[ColumnsScript("Order.OrderStatusHistEditor")]
[BasedOnRow(typeof(OrderStatusHistRow), CheckNames = true)]
public class OrderStatusHistEditorColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public long Id { get; set; }
    public string OrderNumber { get; set; }
    public int OldStatus { get; set; }
    public int NewStatus { get; set; }
    public string ChangedByUserUsername { get; set; }
    [EditLink]
    public string ChangedByUserRole { get; set; }
    public string ChangeReason { get; set; }
    public DateTime ChangeDate { get; set; }
}