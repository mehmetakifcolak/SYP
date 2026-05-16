using _Ext;

namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("StockExits")]
[DisplayName("Stok Çıkışları"), InstanceName("Stok Çıkışı")]
[NavigationPermission("Warehouse:StockExits:Navigation")]
[ReadPermission("Warehouse:StockExits:Read")]
[InsertPermission("Warehouse:StockExits:Insert")]
[UpdatePermission("Warehouse:StockExits:Update")]
[DeletePermission("Warehouse:StockExits:Delete")]
public sealed class StockExitsRow : Row<StockExitsRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    const string jWarehouse = nameof(jWarehouse);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Fiş No"), Size(50), NotNull, QuickSearch, NameProperty]
    public string ExitNo { get => fields.ExitNo[this]; set => fields.ExitNo[this] = value; }
    public partial class RowFields { public StringField ExitNo; }

    [DisplayName("Depo"), NotNull, ForeignKey(typeof(WarehousesRow)), LeftJoin(jWarehouse)]
    [LookupEditor(typeof(WarehousesRow))]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Çıkış Tarihi"), NotNull]
    public DateTime? ExitDate { get => fields.ExitDate[this]; set => fields.ExitDate[this] = value; }
    public partial class RowFields { public DateTimeField ExitDate; }

    [DisplayName("Açıklama"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Durum"), NotNull, DefaultValue(0)]
    public StockExitStatus? Status { get => (StockExitStatus?)fields.Status[this]; set => fields.Status[this] = (short?)value; }
    public partial class RowFields { public Int16Field Status; }

    [DisplayName("Ekli Dosyalar")]
    [MultipleImageUploadEditor(FilenameFormat = "StockExits/~", AllowNonImage = true, MaxSize = 10485760)]
    public string Attachments { get => fields.Attachments[this]; set => fields.Attachments[this] = value; }
    public partial class RowFields { public StringField Attachments; }

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

    // Master-Detail için detay listesi
    [DisplayName("Ürünler"), NotMapped]
    [MasterDetailRelation(foreignKey: nameof(StockExitDetailsRow.StockExitId))]
    public List<StockExitDetailsRow> DetailList { get => fields.DetailList[this]; set => fields.DetailList[this] = value; }
    public partial class RowFields { public RowListField<StockExitDetailsRow> DetailList; }

    #region Foreign Fields

    [DisplayName("Depo Adı"), Expression($"{jWarehouse}.[Name]")]
    public string WarehouseName { get => fields.WarehouseName[this]; set => fields.WarehouseName[this] = value; }
    public partial class RowFields { public StringField WarehouseName; }

    [DisplayName("Depo Kodu"), Expression($"{jWarehouse}.[Code]")]
    public string WarehouseCode { get => fields.WarehouseCode[this]; set => fields.WarehouseCode[this] = value; }
    public partial class RowFields { public StringField WarehouseCode; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
