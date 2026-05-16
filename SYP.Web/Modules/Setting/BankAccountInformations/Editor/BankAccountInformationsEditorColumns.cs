namespace SYP.Setting.Columns;

[ColumnsScript("Setting.BankAccountInformationsEditor")]
[BasedOnRow(typeof(BankAccountInformationsRow), CheckNames = true)]
public class BankAccountInformationsEditorColumns
{
    [EditLink, DisplayName("Db.Shared.RecordId"), AlignRight]
    public int Id { get; set; }
    [EditLink]
    public string Firm { get; set; }
    public string Bank { get; set; }
    public string Branch { get; set; }
    public string BranchCode { get; set; }
    public string AccountNo { get; set; }
    public string Iban { get; set; }
    public string Swift { get; set; }
    [DisplayName("Para Birimi")]
    public string CurrencyCode { get; set; }
    public string Origin { get; set; }
    public string Payment { get; set; }
    public string Shipment { get; set; }
    public int TenantId { get; set; }
}