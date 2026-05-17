namespace SYP.Order.Forms;

[FormScript("Order.OrderDetailEditor")]
[BasedOnRow(typeof(OrderDetailRow), CheckNames = true)]
public class OrderDetailEditorForm
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public decimal Quantity { get; set; }
    public int UnitId { get; set; }
    public decimal UnitPrice { get; set; }
    public int VatRateId { get; set; }
    public decimal VatRate { get; set; }
    public decimal Discount { get; set; }
    public decimal LineTotal { get; set; }
    public string Notes { get; set; }
}