using SYP.Catalog;

namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("WarehouseStock")]
[DisplayName("Depo Stokları"), InstanceName("Depo Stoku")]
[NavigationPermission("Warehouse:WarehouseStock:Navigation")]
[ReadPermission("Warehouse:WarehouseStock:Read")]
[InsertPermission("Warehouse:WarehouseStock:Insert")]
[UpdatePermission("Warehouse:WarehouseStock:Update")]
[DeletePermission("Warehouse:WarehouseStock:Delete")]
public sealed class WarehouseStockRow : Row<WarehouseStockRow.RowFields>, IIdRow
{
    const string jWarehouse = nameof(jWarehouse);
    const string jProduct = nameof(jProduct);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Depo"), NotNull, ForeignKey(typeof(WarehousesRow)), LeftJoin(jWarehouse)]
    [LookupEditor(typeof(WarehousesRow), FilterField = "IsActive", FilterValue = true)]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Ürün"), NotNull, ForeignKey(typeof(ProductsRow)), LeftJoin(jProduct)]
    [LookupEditor(typeof(ProductsRow))]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }
    public partial class RowFields { public Int32Field ProductId; }

    [DisplayName("Miktar"), NotNull, DefaultValue(0)]
    public decimal? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }
    public partial class RowFields { public DecimalField Quantity; }

    [DisplayName("Son Güncelleme")]
    public DateTime? LastUpdateDate { get => fields.LastUpdateDate[this]; set => fields.LastUpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField LastUpdateDate; }

    #region Foreign Fields

    [DisplayName("Depo Kodu"), Expression($"{jWarehouse}.[Code]")]
    public string WarehouseCode { get => fields.WarehouseCode[this]; set => fields.WarehouseCode[this] = value; }
    public partial class RowFields { public StringField WarehouseCode; }

    [DisplayName("Depo Adı"), Expression($"{jWarehouse}.[Name]")]
    public string WarehouseName { get => fields.WarehouseName[this]; set => fields.WarehouseName[this] = value; }
    public partial class RowFields { public StringField WarehouseName; }

    [DisplayName("Ürün Kodu"), Expression($"{jProduct}.[Code]")]
    public string ProductCode { get => fields.ProductCode[this]; set => fields.ProductCode[this] = value; }
    public partial class RowFields { public StringField ProductCode; }

    [DisplayName("Ürün Adı"), Expression($"{jProduct}.[Name]")]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }
    public partial class RowFields { public StringField ProductName; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
