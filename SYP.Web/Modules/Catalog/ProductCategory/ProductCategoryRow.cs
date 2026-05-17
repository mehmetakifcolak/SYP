namespace SYP.Catalog;
[ConnectionKey("Default"), Module("Catalog"), TableName("ProductCategory")]
[DisplayName("Kategoriler"), InstanceName("Kategori")]
[NavigationPermission("Catalog:ProductCategory:Navigation")]
[ReadPermission("Catalog:ProductCategory:Read")]
[InsertPermission("Catalog:ProductCategory:Insert")]
[UpdatePermission("Catalog:ProductCategory:Update")]
[DeletePermission("Catalog:ProductCategory:Delete")]
[ServiceLookupPermission("Catalog:ProductCategory:Lookup")]
public sealed class ProductCategoryRow : Row<ProductCategoryRow.RowFields>, IIdRow, INameRow
{
    const string jParent = nameof(jParent);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Üst Kategori"), ForeignKey(typeof(ProductCategoryRow)), LeftJoin(jParent), TextualField(nameof(ParentName)), LookupInclude]
    [LookupEditor("Catalog.ProductCategory")]
    public int? ParentId { get => fields.ParentId[this]; set => fields.ParentId[this] = value; }
    public partial class RowFields { public Int32Field ParentId; }

    [DisplayName("Ad"), Size(200), NotNull, QuickSearch, NameProperty, LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Tam Yol"), NotMapped, LookupInclude]
    public string FullPath { get => fields.FullPath[this]; set => fields.FullPath[this] = value; }
    public partial class RowFields { public StringField FullPath; }
    
    [DisplayName("Sort Order"), NotNull]
    public int? SortOrder { get => fields.SortOrder[this]; set => fields.SortOrder[this] = value; }
    public partial class RowFields { public Int32Field SortOrder; }
    
    [DisplayName("Is Active"), NotNull, DefaultValue(true)]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Created At")]
    public DateTime? CreatedAt { get => fields.CreatedAt[this]; set => fields.CreatedAt[this] = value; }
    public partial class RowFields { public DateTimeField CreatedAt; }
    
    #region Foreign Fields

    [DisplayName("Parent Name"), Origin(jParent, nameof(Name)), ReadOnly(true)]
    public string ParentName { get => fields.ParentName[this]; set => fields.ParentName[this] = value; }
    public partial class RowFields { public StringField ParentName; }

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}