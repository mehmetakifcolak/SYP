namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("CurrencyList")]
[DisplayName("Currency List"), InstanceName("Currency List")]
[NavigationPermission("Setting:CurrencyList:Navigation")]
[ReadPermission("Setting:CurrencyList:Read")]
[InsertPermission("Setting:CurrencyList:Insert")]
[UpdatePermission("Setting:CurrencyList:Update")]
[DeletePermission("Setting:CurrencyList:Delete")]
[ServiceLookupPermission("Setting:CurrencyList:Lookup")]
public sealed class CurrencyListRow : Row<CurrencyListRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Code"), Size(5), QuickSearch, NameProperty]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }
    
    [DisplayName("Name"), Size(50)]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }
    
    [DisplayName("Code Type")]
    public short? CodeType { get => fields.CodeType[this]; set => fields.CodeType[this] = value; }
    public partial class RowFields { public Int16Field CodeType; }
    
    [DisplayName("Symbol"), Size(5)]
    public string Symbol { get => fields.Symbol[this]; set => fields.Symbol[this] = value; }
    public partial class RowFields { public StringField Symbol; }
    
    [DisplayName("Is Active")]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Insert User Id")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }
    
    [DisplayName("Insert Date")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }
    
    [DisplayName("Update User Id")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }
    
    [DisplayName("Update Date")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }
    
    [DisplayName("Is Default Currency")]
    public bool? IsDefaultCurrency { get => fields.IsDefaultCurrency[this]; set => fields.IsDefaultCurrency[this] = value; }
    public partial class RowFields { public BooleanField IsDefaultCurrency; }
    
    [DisplayName("Default Exchange Type")]
    public short? DefaultExchangeType { get => fields.DefaultExchangeType[this]; set => fields.DefaultExchangeType[this] = value; }
    public partial class RowFields { public Int16Field DefaultExchangeType; }
    
    [DisplayName("Tenant Id")]
    public int? TenantId { get => fields.TenantId[this]; set => fields.TenantId[this] = value; }
    public partial class RowFields { public Int32Field TenantId; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}