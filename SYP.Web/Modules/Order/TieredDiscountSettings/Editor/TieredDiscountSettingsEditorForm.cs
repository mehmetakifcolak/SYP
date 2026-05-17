namespace SYP.Order.Forms;

[FormScript("Order.TieredDiscountSettingsEditor")]
[BasedOnRow(typeof(TieredDiscountSettingsRow), CheckNames = true)]
public class TieredDiscountSettingsEditorForm
{
    public decimal MinAmount { get; set; }
    public decimal DiscountPercentage { get; set; }
    public bool IsActive { get; set; }
    public int DisplayOrder { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public int UpdateUserId { get; set; }
}