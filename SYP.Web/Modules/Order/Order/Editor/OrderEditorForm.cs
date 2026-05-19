namespace SYP.Order.Forms;

[FormScript("Order.OrderEditor")]
[BasedOnRow(typeof(OrderRow), CheckNames = true)]
public class OrderEditorForm
{
    public string OrderNumber { get; set; }
    public int? CustomerId { get; set; }
    public int? ManagerUserId { get; set; }
    public int? Status { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal NetAmount { get; set; }
    public int? CurrencyId { get; set; }
    public string Notes { get; set; }
    public string RejectReason { get; set; }
    public DateTime InsertDate { get; set; }
    public int? InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int? UpdateUserId { get; set; }
}