namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("VatRates")]
[DisplayName("Vat Rates"), InstanceName("Vat Rates")]
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
    
    [DisplayName("Name"), Size(50), NotNull, QuickSearch, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }
    
    [DisplayName("Code"), Size(10)]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }
    
    [DisplayName("Is Default"), NotNull]
    public bool? IsDefault { get => fields.IsDefault[this]; set => fields.IsDefault[this] = value; }
    public partial class RowFields { public BooleanField IsDefault; }
    
    [DisplayName("Is Active"), NotNull]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Sort Order"), NotNull]
    public int? SortOrder { get => fields.SortOrder[this]; set => fields.SortOrder[this] = value; }
    public partial class RowFields { public Int32Field SortOrder; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}