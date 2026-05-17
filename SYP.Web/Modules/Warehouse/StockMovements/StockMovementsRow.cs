
namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("StockMovements")]
[DisplayName("Stock Movements"), InstanceName("Stock Movement")]
[NavigationPermission("Warehouse:StockMovements:Navigation")]
[ReadPermission("Warehouse:StockMovements:Read")]
public sealed class StockMovementsRow : Row<StockMovementsRow.RowFields>, IIdRow, INameRow
{
    [DisplayName("Id"), IdProperty]
    public string Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public StringField Id; }

    [DisplayName("Movement Type"), Size(10), QuickSearch, NameProperty]
    public string MovementType { get => fields.MovementType[this]; set => fields.MovementType[this] = value; }
    public partial class RowFields { public StringField MovementType; }

    [DisplayName("Document No"), Size(50)]
    public string DocumentNo { get => fields.DocumentNo[this]; set => fields.DocumentNo[this] = value; }
    public partial class RowFields { public StringField DocumentNo; }

    [DisplayName("Warehouse Id")]
    public int? WarehouseId { get => fields.WarehouseId[this]; set => fields.WarehouseId[this] = value; }
    public partial class RowFields { public Int32Field WarehouseId; }

    [DisplayName("Warehouse Code"), Size(50)]
    public string WarehouseCode { get => fields.WarehouseCode[this]; set => fields.WarehouseCode[this] = value; }
    public partial class RowFields { public StringField WarehouseCode; }

    [DisplayName("Warehouse Name"), Size(200)]
    public string WarehouseName { get => fields.WarehouseName[this]; set => fields.WarehouseName[this] = value; }
    public partial class RowFields { public StringField WarehouseName; }

    [DisplayName("Product Id")]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }
    public partial class RowFields { public Int32Field ProductId; }

    [DisplayName("Product Code"), Size(50)]
    public string ProductCode { get => fields.ProductCode[this]; set => fields.ProductCode[this] = value; }
    public partial class RowFields { public StringField ProductCode; }

    [DisplayName("Product Name"), Size(200)]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }
    public partial class RowFields { public StringField ProductName; }

    [DisplayName("Quantity"), Size(18), Scale(4)]
    public decimal? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }
    public partial class RowFields { public DecimalField Quantity; }

    [DisplayName("Movement Date")]
    public DateTime? MovementDate { get => fields.MovementDate[this]; set => fields.MovementDate[this] = value; }
    public partial class RowFields { public DateTimeField MovementDate; }

    [DisplayName("Status")]
    public short? Status { get => fields.Status[this]; set => fields.Status[this] = value; }
    public partial class RowFields { public Int16Field Status; }

    [DisplayName("Description"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Insert Date")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Insert User")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    public partial class RowFields : RowFieldsBase { }
}
