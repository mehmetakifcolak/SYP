namespace SYP.Catalog;

[ConnectionKey("Default"), Module("Catalog"), TableName("ProductPacking")]
[DisplayName("Product Packing"), InstanceName("Product Packing")]
[LookupScript("Catalog.ProductPacking", Permission = "Catalog:ProductPacking:Read")]
[NavigationPermission("Catalog:ProductPacking:Navigation")]
[ReadPermission("Catalog:ProductPacking:Read")]
[InsertPermission("Catalog:ProductPacking:Insert")]
[UpdatePermission("Catalog:ProductPacking:Update")]
[DeletePermission("Catalog:ProductPacking:Delete")]
[ServiceLookupPermission("Catalog:ProductPacking:Lookup")]
public sealed class ProductPackingRow : Row<ProductPackingRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty, LookupInclude]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Kod"), Size(50), NotNull, QuickSearch, LookupInclude]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }

    [DisplayName("Ad"), Size(200), NotNull, QuickSearch, NameProperty, LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Açıklama"), Size(int.MaxValue)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Adet"), NotNull, DefaultValue(1), LookupInclude]
    public int? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }
    public partial class RowFields { public Int32Field Quantity; }

    [DisplayName("Aktif"), NotNull, DefaultValue(true), LookupInclude]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }

    [DisplayName("Kayıt Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Kaydeden"), Insertable(false), Updatable(false)]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Güncelleme Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Güncelleyen"), Insertable(false), Updatable(false)]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }

    public partial class RowFields : RowFieldsBase { }
}
