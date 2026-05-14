namespace SYP.Setting.Columns;

[ColumnsScript("Setting.VatRates")]
[BasedOnRow(typeof(VatRatesRow), CheckNames = true)]
public class VatRatesColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}