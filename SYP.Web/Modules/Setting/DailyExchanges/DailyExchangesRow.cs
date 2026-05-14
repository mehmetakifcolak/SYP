namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("DailyExchanges")]
[DisplayName("Daily Exchanges"), InstanceName("Daily Exchanges")]
[NavigationPermission("Setting:DailyExchanges:Navigation")]
[ReadPermission("Setting:DailyExchanges:Read")]
[InsertPermission("Setting:DailyExchanges:Insert")]
[UpdatePermission("Setting:DailyExchanges:Update")]
[DeletePermission("Setting:DailyExchanges:Delete")]
[ServiceLookupPermission("Setting:DailyExchanges:Lookup")]
public sealed class DailyExchangesRow : Row<DailyExchangesRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Currency Id"), NotNull]
    public int? CurrencyId { get => fields.CurrencyId[this]; set => fields.CurrencyId[this] = value; }
    public partial class RowFields { public Int32Field CurrencyId; }
    
    [DisplayName("Currency Code"), Size(3), NotNull, QuickSearch, NameProperty]
    public string CurrencyCode { get => fields.CurrencyCode[this]; set => fields.CurrencyCode[this] = value; }
    public partial class RowFields { public StringField CurrencyCode; }
    
    [DisplayName("Forex Buying"), Size(18), Scale(4)]
    public decimal? ForexBuying { get => fields.ForexBuying[this]; set => fields.ForexBuying[this] = value; }
    public partial class RowFields { public DecimalField ForexBuying; }
    
    [DisplayName("Forex Selling"), Size(18), Scale(4)]
    public decimal? ForexSelling { get => fields.ForexSelling[this]; set => fields.ForexSelling[this] = value; }
    public partial class RowFields { public DecimalField ForexSelling; }
    
    [DisplayName("Banknote Buying"), Size(18), Scale(4)]
    public decimal? BanknoteBuying { get => fields.BanknoteBuying[this]; set => fields.BanknoteBuying[this] = value; }
    public partial class RowFields { public DecimalField BanknoteBuying; }
    
    [DisplayName("Banknote Selling"), Size(18), Scale(4)]
    public decimal? BanknoteSelling { get => fields.BanknoteSelling[this]; set => fields.BanknoteSelling[this] = value; }
    public partial class RowFields { public DecimalField BanknoteSelling; }
    
    [DisplayName("Insert Date")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }
    
    [DisplayName("Insert User Id")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }
    
    [DisplayName("Update User Id")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }
    
    [DisplayName("Update Date")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }
    
    [DisplayName("Is Active"), NotNull]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Default Exchange Type Id")]
    public short? DefaultExchangeTypeId { get => fields.DefaultExchangeTypeId[this]; set => fields.DefaultExchangeTypeId[this] = value; }
    public partial class RowFields { public Int16Field DefaultExchangeTypeId; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}