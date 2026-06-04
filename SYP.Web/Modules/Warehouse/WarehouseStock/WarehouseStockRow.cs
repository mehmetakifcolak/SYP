using SYP.Catalog;

namespace SYP.Warehouse;

// NOT: WarehouseStock artık fiziksel bir tablo değil, [WarehouseStockView] adlı bir
// SQL view'idir. Stok durumu onaylı stok giriş/çıkış hareketlerinden dinamik olarak
// hesaplanır (bkz. DefaultDB_20260604_1000_WarehouseStockView). Bu yüzden satır
// salt-okunurdur; insert/update/delete yoktur.
[ConnectionKey("Default"), Module("Warehouse"), TableName("WarehouseStockView")]
[DisplayName("Warehouse Stock"), InstanceName("Warehouse Stock")]
[NavigationPermission("Warehouse:WarehouseStock:Navigation")]
[ReadPermission("Warehouse:WarehouseStock:Read")]
public sealed class WarehouseStockRow : Row<WarehouseStockRow.RowFields>, IIdRow
{
    const string jWarehouse = nameof(jWarehouse);
    const string jProduct = nameof(jProduct);

    [DisplayName("Id"), IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Warehouse"), ForeignKey(typeof(WarehousesRow)), LeftJoin(jWarehouse)]
    [LookupEditor(typeof(WarehousesRow), FilterField = "IsActive", FilterValue = true)]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Product"), ForeignKey(typeof(ProductsRow)), LeftJoin(jProduct)]
    [LookupEditor(typeof(ProductsRow))]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }
    public partial class RowFields { public Int32Field ProductId; }

    [DisplayName("Quantity")]
    public decimal? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }
    public partial class RowFields { public DecimalField Quantity; }

    [DisplayName("Last Update Date")]
    public DateTime? LastUpdateDate { get => fields.LastUpdateDate[this]; set => fields.LastUpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField LastUpdateDate; }

    #region Foreign Fields

    [DisplayName("Warehouse Code"), Expression($"{jWarehouse}.[Code]")]
    public string WarehouseCode { get => fields.WarehouseCode[this]; set => fields.WarehouseCode[this] = value; }
    public partial class RowFields { public StringField WarehouseCode; }

    [DisplayName("Warehouse Name"), Expression($"{jWarehouse}.[Name]")]
    public string WarehouseName { get => fields.WarehouseName[this]; set => fields.WarehouseName[this] = value; }
    public partial class RowFields { public StringField WarehouseName; }

    [DisplayName("Product Code"), Expression($"{jProduct}.[Code]")]
    public string ProductCode { get => fields.ProductCode[this]; set => fields.ProductCode[this] = value; }
    public partial class RowFields { public StringField ProductCode; }

    [DisplayName("Product Name"), Expression($"{jProduct}.[Name]")]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }
    public partial class RowFields { public StringField ProductName; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
