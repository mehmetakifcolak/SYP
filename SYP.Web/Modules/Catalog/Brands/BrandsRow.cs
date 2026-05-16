using System;

namespace SYP.Catalog;

[ConnectionKey("Default"), Module("Catalog"), TableName("Brands")]
[DisplayName("Markalar"), InstanceName("Marka")]
[LookupScript("Catalog.Brands", Permission = "*")]
[NavigationPermission("Catalog:Brands:Navigation")]
[ReadPermission("Catalog:Brands:Read")]
[InsertPermission("Catalog:Brands:Insert")]
[UpdatePermission("Catalog:Brands:Update")]
[DeletePermission("Catalog:Brands:Delete")]
public sealed class BrandsRow : Row<BrandsRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Ad"), Size(500), QuickSearch, NameProperty, LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Açıklama"), Size(4000)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Logo"), Size(400)]
    [ImageUploadEditor(FilenameFormat = "Brands/~", CopyToHistory = true)]
    public string Logo { get => fields.Logo[this]; set => fields.Logo[this] = value; }
    public partial class RowFields { public StringField Logo; }

    [DisplayName("Aktif"), LookupInclude]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }

    [DisplayName("Kayıt Tarihi")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Kaydeden")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Güncelleme Tarihi")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Güncelleyen")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }

    public partial class RowFields : RowFieldsBase { }
}
