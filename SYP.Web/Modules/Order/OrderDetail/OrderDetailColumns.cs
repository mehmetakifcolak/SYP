namespace SYP.Order.Columns;

[ColumnsScript("Order.OrderDetail")]
[BasedOnRow(typeof(OrderDetailRow), CheckNames = true)]
public class OrderDetailColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    public string OrderNumber { get; set; }
    public string ProductCodeName { get; set; }
    public decimal Quantity { get; set; }
    public string UnitCode { get; set; }
    public decimal UnitPrice { get; set; }
    public string VatRateName { get; set; }
    public decimal VatRate { get; set; }
    public decimal Discount { get; set; }
    public decimal LineTotal { get; set; }
    [EditLink]
    public string Notes { get; set; }
}