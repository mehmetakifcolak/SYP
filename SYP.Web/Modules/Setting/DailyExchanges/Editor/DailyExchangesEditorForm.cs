namespace SYP.Setting.Forms;

[FormScript("Setting.DailyExchangesEditor")]
[BasedOnRow(typeof(DailyExchangesRow), CheckNames = true)]
public class DailyExchangesEditorForm
{
    public int CurrencyId { get; set; }
    public string CurrencyCode { get; set; }
    public decimal ForexBuying { get; set; }
    public decimal ForexSelling { get; set; }
    public decimal BanknoteBuying { get; set; }
    public decimal BanknoteSelling { get; set; }
    public DateTime InsertDate { get; set; }
    public int InsertUserId { get; set; }
    public int UpdateUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsActive { get; set; }
    public short DefaultExchangeTypeId { get; set; }
}