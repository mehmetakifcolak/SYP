namespace SYP.Order.Columns;

[ColumnsScript("Order.TieredDiscountSettings")]
[BasedOnRow(typeof(TieredDiscountSettingsRow), CheckNames = true)]
public class TieredDiscountSettingsColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    public decimal MinAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
}