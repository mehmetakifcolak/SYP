namespace SYP.Setting.Columns;

[ColumnsScript("Setting.VendorType")]
[BasedOnRow(typeof(VendorTypeRow), CheckNames = true)]
public class VendorTypeColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string Title { get; set; }
    public string DiscountType { get; set; }
    public decimal DiscountValue { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}