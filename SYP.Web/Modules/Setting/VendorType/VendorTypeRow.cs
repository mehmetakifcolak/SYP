namespace SYP.Setting;

[ConnectionKey("Default"), Module("Setting"), TableName("VendorType")]
[DisplayName("Vendor Type"), InstanceName("Vendor Type")]
[LookupScript("Setting.VendorType")]
[NavigationPermission("Setting:VendorType:Navigation")]
[ReadPermission("Setting:VendorType:Read")]
[InsertPermission("Setting:VendorType:Insert")]
[UpdatePermission("Setting:VendorType:Update")]
[DeletePermission("Setting:VendorType:Delete")]
[ServiceLookupPermission("Setting:VendorType:Lookup")]
public sealed class VendorTypeRow : Row<VendorTypeRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Title"), Size(100), QuickSearch, NameProperty]
    public string Title { get => fields.Title[this]; set => fields.Title[this] = value; }
    public partial class RowFields { public StringField Title; }
    
    [DisplayName("Discount Type"), Size(10)]
    public string DiscountType { get => fields.DiscountType[this]; set => fields.DiscountType[this] = value; }
    public partial class RowFields { public StringField DiscountType; }
    
    [DisplayName("Discount Value"), Size(18), Scale(2)]
    public decimal? DiscountValue { get => fields.DiscountValue[this]; set => fields.DiscountValue[this] = value; }
    public partial class RowFields { public DecimalField DiscountValue; }
    
    [DisplayName("Description"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }
    
    [DisplayName("Is Active")]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}