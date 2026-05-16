namespace SYP.Setting;

using DocumentFormat.OpenXml.VariantTypes;

[ConnectionKey("Default"), Module("Setting"), TableName("BankAccountInformations")]
[DisplayName("Bank Account Informations"), InstanceName("Bank Account Informations")]
[NavigationPermission("Setting:BankAccountInformations:Navigation")]
[ReadPermission("Setting:BankAccountInformations:Read")]
[InsertPermission("Setting:BankAccountInformations:Insert")]
[UpdatePermission("Setting:BankAccountInformations:Update")]
[DeletePermission("Setting:BankAccountInformations:Delete")]
[ServiceLookupPermission("Setting:BankAccountInformations:Lookup")]
public sealed class BankAccountInformationsRow : Row<BankAccountInformationsRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    const string jCurrency = nameof(jCurrency);
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Firm"), Size(100), QuickSearch, NameProperty]
    public string Firm { get => fields.Firm[this]; set => fields.Firm[this] = value; }
    public partial class RowFields { public StringField Firm; }
    
    [DisplayName("Bank"), Size(50)]
    public string Bank { get => fields.Bank[this]; set => fields.Bank[this] = value; }
    public partial class RowFields { public StringField Bank; }
    
    [DisplayName("Branch"), Size(50)]
    public string Branch { get => fields.Branch[this]; set => fields.Branch[this] = value; }
    public partial class RowFields { public StringField Branch; }
    
    [DisplayName("Branch Code"), Size(50)]
    public string BranchCode { get => fields.BranchCode[this]; set => fields.BranchCode[this] = value; }
    public partial class RowFields { public StringField BranchCode; }
    
    [DisplayName("Account No"), Size(50)]
    public string AccountNo { get => fields.AccountNo[this]; set => fields.AccountNo[this] = value; }
    public partial class RowFields { public StringField AccountNo; }
    
    [DisplayName("Iban"), Size(50)]
    public string Iban { get => fields.Iban[this]; set => fields.Iban[this] = value; }
    public partial class RowFields { public StringField Iban; }
    
    [DisplayName("Swift"), Size(50)]
    public string Swift { get => fields.Swift[this]; set => fields.Swift[this] = value; }
    public partial class RowFields { public StringField Swift; }
    
    [DisplayName("Currency"), Size(50)]
    [ForeignKey(typeof(CurrencyListRow)), LeftJoin(jCurrency)]
    [ServiceLookupEditor(typeof(CurrencyListRow), FilterField = "IsActive", FilterValue = 1)]
   // [TextualField(nameof(CurrencyCode))]
    public string Currency { get => fields.Currency[this]; set => fields.Currency[this] = value; }
    public partial class RowFields { public StringField Currency; }
    
    [DisplayName("Origin"), Size(50)]
    public string Origin { get => fields.Origin[this]; set => fields.Origin[this] = value; }
    public partial class RowFields { public StringField Origin; }
    
    [DisplayName("Payment"), Size(50)]
    public string Payment { get => fields.Payment[this]; set => fields.Payment[this] = value; }
    public partial class RowFields { public StringField Payment; }
    
    [DisplayName("Shipment"), Size(100)]
    public string Shipment { get => fields.Shipment[this]; set => fields.Shipment[this] = value; }
    public partial class RowFields { public StringField Shipment; }
    
    [DisplayName("Tenant Id")]
    public int? TenantId { get => fields.TenantId[this]; set => fields.TenantId[this] = value; }
    public partial class RowFields { public Int32Field TenantId; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}