using SYP.Administration;

namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("Warehouses")]
[DisplayName("Depolar"), InstanceName("Depo")]
[LookupScript("Warehouse.Warehouses", Permission = "Warehouse:Warehouses:Read")]
[NavigationPermission("Warehouse:Warehouses:Navigation")]
[ReadPermission("Warehouse:Warehouses:Read")]
[InsertPermission("Warehouse:Warehouses:Insert")]
[UpdatePermission("Warehouse:Warehouses:Update")]
[DeletePermission("Warehouse:Warehouses:Delete")]
[ServiceLookupPermission("Warehouse:Warehouses:Read")]
public sealed class WarehousesRow : Row<WarehousesRow.RowFields>, IIdRow, INameRow, IAuditedRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Depo Kodu"), Size(50), NotNull, QuickSearch]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }

    [DisplayName("Depo Adı"), Size(200), NotNull, NameProperty]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Adres"), Size(500)]
    public string Address { get => fields.Address[this]; set => fields.Address[this] = value; }
    public partial class RowFields { public StringField Address; }

    [DisplayName("Şehir"), Size(100)]
    public string City { get => fields.City[this]; set => fields.City[this] = value; }
    public partial class RowFields { public StringField City; }

    [DisplayName("Telefon"), Size(50)]
    public string Phone { get => fields.Phone[this]; set => fields.Phone[this] = value; }
    public partial class RowFields { public StringField Phone; }

    [DisplayName("Email"), Size(200)]
    public string Email { get => fields.Email[this]; set => fields.Email[this] = value; }
    public partial class RowFields { public StringField Email; }

    [DisplayName("Sorumlu Kişi"), Size(200)]
    public string ManagerName { get => fields.ManagerName[this]; set => fields.ManagerName[this] = value; }
    public partial class RowFields { public StringField ManagerName; }

    [DisplayName("Açıklama"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Aktif"), NotNull, DefaultValue(true), LookupInclude]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }

    [DisplayName("Varsayılan Depo"), NotNull, DefaultValue(false), LookupInclude]
    public bool? IsDefault { get => fields.IsDefault[this]; set => fields.IsDefault[this] = value; }
    public partial class RowFields { public BooleanField IsDefault; }

    [DisplayName("Oluşturma Tarihi"), Insertable(false), Updatable(false)]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Oluşturan"), Insertable(false), Updatable(false)]
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
