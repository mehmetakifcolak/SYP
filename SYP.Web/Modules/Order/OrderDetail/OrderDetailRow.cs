namespace SYP.Order;

[ConnectionKey("Default"), Module("Order"), TableName("OrderDetails")]
[DisplayName("Order Detail"), InstanceName("Order Detail")]
[NavigationPermission("Order:OrderDetail:Navigation")]
[ReadPermission("Order:OrderDetail:Read")]
[InsertPermission("Order:OrderDetail:Insert")]
[UpdatePermission("Order:OrderDetail:Update")]
[DeletePermission("Order:OrderDetail:Delete")]
[ServiceLookupPermission("Order:OrderDetail:Lookup")]
public sealed class OrderDetailRow : Row<OrderDetailRow.RowFields>, IIdRow, INameRow
{
    const string jOrder = nameof(jOrder);
    const string jProduct = nameof(jProduct);
    const string jUnit = nameof(jUnit);
    const string jVatRate = nameof(jVatRate);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [NotMapped, Ignore]
    public int? __id { get; set; }
    
    [DisplayName("Order"), NotNull, ForeignKey(typeof(OrderRow)), LeftJoin(jOrder), TextualField(nameof(OrderNumber))]
    [ServiceLookupEditor(typeof(OrderRow))]
    public int? OrderId { get => fields.OrderId[this]; set => fields.OrderId[this] = value; }
    public partial class RowFields { public Int32Field OrderId; }
    
    [DisplayName("Product"), NotNull, ForeignKey(typeof(Catalog.ProductsRow)), LeftJoin(jProduct), TextualField(nameof(ProductCodeName))]
    [LookupEditor(typeof(Catalog.ProductsRow), Async = true)]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }
    public partial class RowFields { public Int32Field ProductId; }
    
    [DisplayName("Quantity"), Size(18), Scale(4), NotNull]
    public decimal? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }
    public partial class RowFields { public DecimalField Quantity; }
    
    [DisplayName("Unit"), ForeignKey(typeof(Setting.UnitsRow)), LeftJoin(jUnit), TextualField(nameof(UnitCode))]
    [LookupEditor(typeof(Setting.UnitsRow), Async = true)]
    public int? UnitId { get => fields.UnitId[this]; set => fields.UnitId[this] = value; }
    public partial class RowFields { public Int32Field UnitId; }
    
    [DisplayName("Unit Price"), Size(18), Scale(4), NotNull]
    public decimal? UnitPrice { get => fields.UnitPrice[this]; set => fields.UnitPrice[this] = value; }
    public partial class RowFields { public DecimalField UnitPrice; }
    
    [DisplayName("Vat Rate"), ForeignKey(typeof(Setting.VatRatesRow)), LeftJoin(jVatRate), TextualField(nameof(VatRateName))]
    [LookupEditor(typeof(Setting.VatRatesRow), Async = true)]
    public int? VatRateId { get => fields.VatRateId[this]; set => fields.VatRateId[this] = value; }
    public partial class RowFields { public Int32Field VatRateId; }
    
    [DisplayName("Vat Rate"), Size(5), Scale(2), NotNull]
    public decimal? VatRate { get => fields.VatRate[this]; set => fields.VatRate[this] = value; }
    public partial class RowFields { public DecimalField VatRate; }
    
    [DisplayName("Discount"), Size(18), Scale(4), NotNull]
    public decimal? Discount { get => fields.Discount[this]; set => fields.Discount[this] = value; }
    public partial class RowFields { public DecimalField Discount; }
    
    [DisplayName("Line Total"), Size(18), Scale(4), NotNull]
    public decimal? LineTotal { get => fields.LineTotal[this]; set => fields.LineTotal[this] = value; }
    public partial class RowFields { public DecimalField LineTotal; }
    
    [DisplayName("Notes"), Size(500), QuickSearch, NameProperty]
    public string Notes { get => fields.Notes[this]; set => fields.Notes[this] = value; }
    public partial class RowFields { public StringField Notes; }
    
    #region Foreign Fields

    [DisplayName("Order Order Number"), Origin(jOrder, nameof(OrderRow.OrderNumber)), ReadOnly(true)]
    public string OrderNumber { get => fields.OrderNumber[this]; set => fields.OrderNumber[this] = value; }
    public partial class RowFields { public StringField OrderNumber; }

    [DisplayName("Product"), Origin(jProduct, nameof(Catalog.ProductsRow.CodeName)), ReadOnly(true)]
    public string ProductCodeName { get => fields.ProductCodeName[this]; set => fields.ProductCodeName[this] = value; }
    public partial class RowFields { public StringField ProductCodeName; }

    [DisplayName("Unit Code"), Origin(jUnit, nameof(Setting.UnitsRow.Code)), ReadOnly(true)]
    public string UnitCode { get => fields.UnitCode[this]; set => fields.UnitCode[this] = value; }
    public partial class RowFields { public StringField UnitCode; }

    [DisplayName("Vat Rate Name"), Origin(jVatRate, nameof(Setting.VatRatesRow.Name)), ReadOnly(true)]
    public string VatRateName { get => fields.VatRateName[this]; set => fields.VatRateName[this] = value; }
    public partial class RowFields { public StringField VatRateName; }

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}