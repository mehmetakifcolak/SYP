namespace SYP.Setting.Forms;

[FormScript("Setting.VatRates")]
[BasedOnRow(typeof(VatRatesRow), CheckNames = true)]
public class VatRatesForm
{
    public string Name { get; set; }
    public string Code { get; set; }
    public bool IsDefault { get; set; }
    public bool IsActive { get; set; }
    public int SortOrder { get; set; }
}