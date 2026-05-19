using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System.ComponentModel;

namespace SYP.Catalog;

[ConnectionKey("Default"), Module("Catalog"), TableName("PriceListItems")]
[DisplayName("Fiyat Listesi Kalemleri"), InstanceName("Fiyat Listesi Kalemi")]
[ReadPermission("Catalog:PriceLists:Read")]
[InsertPermission("Catalog:PriceLists:Insert")]
[UpdatePermission("Catalog:PriceLists:Update")]
[DeletePermission("Catalog:PriceLists:Delete")]
public sealed class PriceListItemsRow : Row<PriceListItemsRow.RowFields>, IIdRow
{
    const string jPriceList = nameof(jPriceList);
    const string jProduct = nameof(jProduct);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Fiyat Listesi"), NotNull, ForeignKey(typeof(PriceListsRow)), LeftJoin(jPriceList)]
    public int? PriceListId { get => fields.PriceListId[this]; set => fields.PriceListId[this] = value; }
    public partial class RowFields { public Int32Field PriceListId; }

    [DisplayName("Ürün"), NotNull, ForeignKey(typeof(ProductsRow)), LeftJoin(jProduct)]
    [LookupEditor(typeof(ProductsRow), FilterField = "IsActive", FilterValue = 1)]
    public int? ProductId { get => fields.ProductId[this]; set => fields.ProductId[this] = value; }
    public partial class RowFields { public Int32Field ProductId; }

    [DisplayName("Birim Fiyat"), NotNull, DecimalEditor(Decimals = 4, MinValue = "0")]
    public decimal? UnitPrice { get => fields.UnitPrice[this]; set => fields.UnitPrice[this] = value; }
    public partial class RowFields { public DecimalField UnitPrice; }

    [DisplayName("İndirim Oranı (%)"), DecimalEditor(Decimals = 2, MinValue = "0", MaxValue = "100")]
    public decimal? DiscountRate { get => fields.DiscountRate[this]; set => fields.DiscountRate[this] = value; }
    public partial class RowFields { public DecimalField DiscountRate; }

    [DisplayName("Notlar"), Size(500)]
    public string Notes { get => fields.Notes[this]; set => fields.Notes[this] = value; }
    public partial class RowFields { public StringField Notes; }

    #region Foreign Fields

    [DisplayName("Ürün Kodu"), Expression($"{jProduct}.[Code]")]
    public string ProductCode { get => fields.ProductCode[this]; set => fields.ProductCode[this] = value; }
    public partial class RowFields { public StringField ProductCode; }

    [DisplayName("Ürün Adı"), Expression($"{jProduct}.[Name]")]
    public string ProductName { get => fields.ProductName[this]; set => fields.ProductName[this] = value; }
    public partial class RowFields { public StringField ProductName; }

    [DisplayName("Fiyat Listesi Kodu"), Expression($"{jPriceList}.[Code]")]
    public string PriceListCode { get => fields.PriceListCode[this]; set => fields.PriceListCode[this] = value; }
    public partial class RowFields { public StringField PriceListCode; }

    [DisplayName("Fiyat Listesi Adı"), Expression($"{jPriceList}.[Name]")]
    public string PriceListName { get => fields.PriceListName[this]; set => fields.PriceListName[this] = value; }
    public partial class RowFields { public StringField PriceListName; }

    [DisplayName("Geçerlilik Başlangıç"), Expression($"{jPriceList}.[ValidFrom]")]
    public DateTime? PriceListValidFrom { get => fields.PriceListValidFrom[this]; set => fields.PriceListValidFrom[this] = value; }
    public partial class RowFields { public DateTimeField PriceListValidFrom; }

    [DisplayName("Geçerlilik Bitiş"), Expression($"{jPriceList}.[ValidTo]")]
    public DateTime? PriceListValidTo { get => fields.PriceListValidTo[this]; set => fields.PriceListValidTo[this] = value; }
    public partial class RowFields { public DateTimeField PriceListValidTo; }

    [DisplayName("Aktif"), Expression($"{jPriceList}.[IsActive]")]
    public bool? PriceListIsActive { get => fields.PriceListIsActive[this]; set => fields.PriceListIsActive[this] = value; }
    public partial class RowFields { public BooleanField PriceListIsActive; }

    [DisplayName("Varsayılan"), Expression($"{jPriceList}.[IsDefault]")]
    public bool? PriceListIsDefault { get => fields.PriceListIsDefault[this]; set => fields.PriceListIsDefault[this] = value; }
    public partial class RowFields { public BooleanField PriceListIsDefault; }

    [DisplayName("Fiyat Listesi Tipi"), Expression($"{jPriceList}.[Type]")]
    public PriceListType? PriceListType { get => (PriceListType?)fields.PriceListType[this]; set => fields.PriceListType[this] = (int?)value; }
    public partial class RowFields { public Int32Field PriceListType; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
