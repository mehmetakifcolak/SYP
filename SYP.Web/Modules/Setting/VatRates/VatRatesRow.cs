namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("VatRates")]
[DisplayName("VAT Rates"), InstanceName("VAT Rate")]
[LookupScript("Setting.VatRates", Permission = "*")]
[NavigationPermission("Setting:VatRates:Navigation")]
[ReadPermission("Setting:VatRates:Read")]
[InsertPermission("Setting:VatRates:Insert")]
[UpdatePermission("Setting:VatRates:Update")]
[DeletePermission("Setting:VatRates:Delete")]
[ServiceLookupPermission("Setting:VatRates:Lookup")]
public sealed class VatRatesRow : Row<VatRatesRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    [DisplayName("Id"), PrimaryKey, NotNull, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Name"), Size(50), NotNull, QuickSearch, NameProperty, LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Code"), Size(10), LookupInclude]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }

    [DisplayName("Rate"), LookupInclude]
    public decimal? Rate { get => fields.Rate[this]; set => fields.Rate[this] = value; }
    public partial class RowFields { public DecimalField Rate; }
    
    [DisplayName("Is Default"), NotNull, LookupInclude]
    public bool? IsDefault { get => fields.IsDefault[this]; set => fields.IsDefault[this] = value; }
    public partial class RowFields { public BooleanField IsDefault; }

    [DisplayName("Is Active"), NotNull, LookupInclude]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Sort Order"), NotNull]
    public int? SortOrder { get => fields.SortOrder[this]; set => fields.SortOrder[this] = value; }
    public partial class RowFields { public Int32Field SortOrder; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}