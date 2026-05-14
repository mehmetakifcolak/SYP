namespace SYP.Setting.Forms;

[FormScript("Setting.CurrencyList")]
[BasedOnRow(typeof(CurrencyListRow), CheckNames = true)]
public class CurrencyListForm
{
    public string Code { get; set; }
    public string Name { get; set; }
    public short CodeType { get; set; }
    public string Symbol { get; set; }
    public bool IsActive { get; set; }
    public int InsertUserId { get; set; }
    public DateTime InsertDate { get; set; }
    public int UpdateUserId { get; set; }
    public DateTime UpdateDate { get; set; }
    public bool IsDefaultCurrency { get; set; }
    public short DefaultExchangeType { get; set; }
    public int TenantId { get; set; }
}