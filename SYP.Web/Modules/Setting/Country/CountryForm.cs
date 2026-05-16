namespace SYP.Setting.Forms;

[FormScript("Setting.Country")]
[BasedOnRow(typeof(CountryRow), CheckNames = true)]
public class CountryForm
{
    public string Name { get; set; }
    public string Code { get; set; }
    public string IsoCode3 { get; set; }
    public string PhoneCode { get; set; }
    public string CountryNr { get; set; }
    public int InsertUserId { get; set; }
    public DateTime InsertDate { get; set; }
    public int UpdateUserId { get; set; }
    public DateTime UpdateDate { get; set; }
}