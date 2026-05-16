using _Ext;

namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("StockMovements")]
[DisplayName("Stok Hareketleri"), InstanceName("Stok Hareketi")]
[NavigationPermission("Warehouse:StockMovements:Navigation")]
[ReadPermission("Warehouse:StockMovements:Read")]
public sealed class StockMovementsRow : Row<StockMovementsRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), IdProperty]
    public string Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public StringField Id; }

    [DisplayName("Hareket Tipi"), Size(10), QuickSearch, NameProperty]
    public string MovementType { get => fields.MovementType[this]; set => fields.MovementType[this] = value; }
    public partial class RowFields { public StringField MovementType; }

    [DisplayName("Fiş No"), Size(50)]
    public string DocumentNo { get => fields.DocumentNo[this]; set => fields.DocumentNo[this] = value; }
    public partial class RowFields { public StringField DocumentNo; }

    [DisplayName("Depo Id")]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Depo Kodu"), Size(50)]
    public string WarehouseCode { get => fields.WarehouseCode[this]; set => fields.WarehouseCode[this] = value; }
    public partial class RowFields { public StringField WarehouseCode; }

    [DisplayName("Depo Adı"), Size(200)]
    public string WarehouseName { get => fields.WarehouseName[this]; set => fields.WarehouseName[this] = value; }
    public partial class RowFields { public StringField WarehouseName; }

    [DisplayName("Ürün Id")]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }
    public partial class RowFields { public Int32Field ProductId; }

    [DisplayName("Ürün Kodu"), Size(50)]
    public string ProductCode { get => fields.ProductCode[this]; set => fields.ProductCode[this] = value; }
    public partial class RowFields { public StringField ProductCode; }

    [DisplayName("Ürün Adı"), Size(200)]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }
    public partial class RowFields { public StringField ProductName; }

    [DisplayName("Miktar"), Size(18), Scale(4)]
    public decimal? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }
    public partial class RowFields { public DecimalField Quantity; }

    [DisplayName("Hareket Tarihi")]
    public DateTime? MovementDate { get => fields.MovementDate[this]; set => fields.MovementDate[this] = value; }
    public partial class RowFields { public DateTimeField MovementDate; }

    [DisplayName("Durum")]
    public short? Status { get => fields.Status[this]; set => fields.Status[this] = value; }
    public partial class RowFields { public Int16Field Status; }

    [DisplayName("Açıklama"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Oluşturma Tarihi")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Oluşturan")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    public partial class RowFields : RowFieldsBase { }
}
