namespace SYP.Setting.Columns;

[ColumnsScript("Setting.CurrencyListEditor")]
[BasedOnRow(typeof(CurrencyListRow), CheckNames = true)]
public class CurrencyListEditorColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
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