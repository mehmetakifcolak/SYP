namespace SYP.Setting.Forms;

[FormScript("Setting.VendorTypeEditor")]
[BasedOnRow(typeof(VendorTypeRow), CheckNames = true)]
public class VendorTypeEditorForm
{
    public string Title { get; set; }
    public string DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}