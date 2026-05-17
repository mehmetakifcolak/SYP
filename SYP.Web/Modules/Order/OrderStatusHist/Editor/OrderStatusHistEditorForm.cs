namespace SYP.Order.Forms;

[FormScript("Order.OrderStatusHistEditor")]
[BasedOnRow(typeof(OrderStatusHistRow), CheckNames = true)]
public class OrderStatusHistEditorForm
{
    public int OrderId { get; set; }
    public int OldStatus { get; set; }
    public int NewStatus { get; set; }
    public int ChangedByUserId { get; set; }
    public string ChangedByUserRole { get; set; }
    public string ChangeReason { get; set; }
    public DateTime ChangeDate { get; set; }
}