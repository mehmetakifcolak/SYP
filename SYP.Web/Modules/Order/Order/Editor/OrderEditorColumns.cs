namespace SYP.Order.Columns;

[ColumnsScript("Order.OrderEditor")]
[BasedOnRow(typeof(OrderRow), CheckNames = true)]
public class OrderEditorColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string OrderNumber { get; set; }
    public string CustomerCode { get; set; }
    public string ManagerUserUsername { get; set; }
    public int Status { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal NetAmount { get; set; }
    public string CurrencyCode { get; set; }
    public string Notes { get; set; }
    public string RejectReason { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
}