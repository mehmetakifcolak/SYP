namespace SYP.Setting.Forms;

[FormScript("Setting.VatRatesEditor")]
[BasedOnRow(typeof(VatRatesRow), CheckNames = true)]
public class VatRatesEditorForm
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}