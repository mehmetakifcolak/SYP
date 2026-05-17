namespace SYP.Catalog;

[ConnectionKey("Default"), Module("Catalog"), TableName("Brands")]
[DisplayName("Brands"), InstanceName("Brands")]
[LookupScript("Catalog.Brands", Permission = "Catalog:Brands:Read")]
[NavigationPermission("Catalog:Brands:Navigation")]
[ReadPermission("Catalog:Brands:Read")]
[InsertPermission("Catalog:Brands:Insert")]
[UpdatePermission("Catalog:Brands:Update")]
[DeletePermission("Catalog:Brands:Delete")]
[ServiceLookupPermission("Catalog:Brands:Lookup")]
public sealed class BrandsRow : Row<BrandsRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty, LookupInclude]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Name"), Size(500), QuickSearch, NameProperty, LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Description"), Size(4000)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Logo"), Size(400)]
    [ImageUploadEditor(FilenameFormat = "Brands/~", CopyToHistory = true)]
    public string Logo { get => fields.Logo[this]; set => fields.Logo[this] = value; }
    public partial class RowFields { public StringField Logo; }

    [DisplayName("Is Active"), LookupInclude]
    public short? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public Int16Field IsActive; }
    
    [DisplayName("Insert Date")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }
    
    [DisplayName("Insert User Id")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }
    
    [DisplayName("Update Date")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }
    
    [DisplayName("Update User Id")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }
    
    #region Foreign Fields

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}