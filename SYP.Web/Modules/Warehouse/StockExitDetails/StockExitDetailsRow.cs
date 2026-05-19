using SYP.Catalog;

namespace SYP.Warehouse;

[ConnectionKey("Default"), Module("Warehouse"), TableName("StockExitDetails")]
[DisplayName("Stok Çıkış Detayları"), InstanceName("Stok Çıkış Detayı")]
[ReadPermission("Warehouse:StockExits:Read")]
[InsertPermission("Warehouse:StockExits:Insert")]
[UpdatePermission("Warehouse:StockExits:Update")]
[DeletePermission("Warehouse:StockExits:Delete")]
public sealed class StockExitDetailsRow : Row<StockExitDetailsRow.RowFields>, IIdRow
{
    const string jStockExit = nameof(jStockExit);
    const string jProduct = nameof(jProduct);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Stok Çıkışı"), NotNull, ForeignKey(typeof(StockExitsRow)), LeftJoin(jStockExit)]
    public int? StockExitId { get => fields.StockExitId[this]; set => fields.StockExitId[this] = value; }
    public partial class RowFields { public Int32Field StockExitId; }

    [DisplayName("Ürün"), NotNull, ForeignKey(typeof(ProductsRow)), LeftJoin(jProduct)]
    [LookupEditor(typeof(ProductsRow))]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }
    public partial class RowFields { public Int32Field ProductId; }

    [DisplayName("Miktar"), NotNull, DefaultValue(1), DecimalEditor(Decimals = 4, MinValue = "0.0001")]
    public decimal? Quantity { get => fields.Quantity[this]; set => fields.Quantity[this] = value; }
    public partial class RowFields { public DecimalField Quantity; }

    [DisplayName("Birim"), Size(10)]
    public string Unit { get => fields.Unit[this]; set => fields.Unit[this] = value; }
    public partial class RowFields { public StringField Unit; }

    [DisplayName("Para Birimi"), Size(10), QuickSearch]
    public string Currency { get => fields.Currency[this]; set => fields.Currency[this] = value; }
    public partial class RowFields { public StringField Currency; }

    [DisplayName("KDV Oranı")]
    public decimal? VatRate { get => fields.VatRate[this]; set => fields.VatRate[this] = value; }
    public partial class RowFields { public DecimalField VatRate; }

    [DisplayName("Birim Fiyat"), DecimalEditor(Decimals = 2, MinValue = "0")]
    public decimal? UnitPrice { get => fields.UnitPrice[this]; set => fields.UnitPrice[this] = value; }
    public partial class RowFields { public DecimalField UnitPrice; }

    [DisplayName("Notlar"), Size(500)]
    public string Notes { get => fields.Notes[this]; set => fields.Notes[this] = value; }
    public partial class RowFields { public StringField Notes; }

    #region Foreign Fields

    [DisplayName("Ürün Kodu"), Expression($"{jProduct}.[Code]"), MinSelectLevel(SelectLevel.List)]
    public string ProductCode { get => fields.ProductCode[this]; set => fields.ProductCode[this] = value; }
    public partial class RowFields { public StringField ProductCode; }

    [DisplayName("Ürün Adı"), Expression($"{jProduct}.[Name]"), MinSelectLevel(SelectLevel.List)]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }
    public partial class RowFields { public StringField ProductName; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
