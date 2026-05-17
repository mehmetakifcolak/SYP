using Serenity.ComponentModel;
using Serenity.Web;

namespace SYP.Catalog;

[ConnectionKey("Default"), Module("Catalog"), TableName("Products")]
[DisplayName("Products"), InstanceName("Products")]
[LookupScript("Catalog.Products", Permission = "Catalog:Products:Read")]
[NavigationPermission("Catalog:Products:Navigation")]
[ReadPermission("Catalog:Products:Read")]
[InsertPermission("Catalog:Products:Insert")]
[UpdatePermission("Catalog:Products:Update")]
[DeletePermission("Catalog:Products:Delete")]
public sealed class ProductsRow : Row<ProductsRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Code"), Size(50), QuickSearch, LookupInclude]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }

    [DisplayName("Name"), Size(500), LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Code - Name"), NameProperty]
    [Expression("CONCAT(T0.Code, ' - ', T0.Name)"), LookupInclude]
    public string CodeName { get => fields.CodeName[this]; set => fields.CodeName[this] = value; }
    public partial class RowFields { public StringField CodeName; }
    
    [DisplayName("Name2"), Size(500)]
    public string Name2 { get => fields.Name2[this]; set => fields.Name2[this] = value; }
    public partial class RowFields { public StringField Name2; }
    
    [DisplayName("Description"), Size(4000)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }
    
    [DisplayName("Barcode"), Size(250)]
    public string Barcode { get => fields.Barcode[this]; set => fields.Barcode[this] = value; }
    public partial class RowFields { public StringField Barcode; }

    [DisplayName("Ürün Resmi"), Size(200)]
    [ImageUploadEditor(FilenameFormat = "Products/~", CopyToHistory = true  )]
    public string ProductImage { get => fields.ProductImage[this]; set => fields.ProductImage[this] = value; }
    public partial class RowFields { public StringField ProductImage; }

    const string jUnit = nameof(jUnit);
    const string jCurrency = nameof(jCurrency);
    const string jVatRate = nameof(jVatRate);
    const string jBrand = nameof(jBrand);
    const string jCategory = nameof(jCategory);

    [DisplayName("Kategori"), LookupInclude, ForeignKey(typeof(ProductCategoryRow)), LeftJoin(jCategory)]
    [LookupEditor("Catalog.ProductCategory", FilterField = "IsActive", FilterValue = true)]
    public int? CategoryId { get => fields.CategoryId[this]; set => fields.CategoryId[this] = value; }
    public partial class RowFields { public Int32Field CategoryId; }

    [DisplayName("Marka"), LookupInclude, ForeignKey(typeof(BrandsRow)), LeftJoin(jBrand)]
    [LookupEditor(typeof(BrandsRow), FilterField = "IsActive", FilterValue = 1)]
    public int? BrandId { get => fields.BrandId[this]; set => fields.BrandId[this] = value; }
    public partial class RowFields { public Int32Field BrandId; }

    [DisplayName("Birim"), LookupInclude, ForeignKey(typeof(Setting.UnitsRow)), LeftJoin(jUnit)]
    [LookupEditor(typeof(Setting.UnitsRow), FilterField = "IsActive", FilterValue = true)]
    public int? UnitId { get => fields.UnitId[this]; set => fields.UnitId[this] = value; }
    public partial class RowFields { public Int32Field UnitId; }

    [DisplayName("Para Birimi"), LookupInclude, ForeignKey(typeof(Setting.CurrencyListRow)), LeftJoin(jCurrency)]
    [LookupEditor(typeof(Setting.CurrencyListRow), FilterField = "IsActive", FilterValue = true)]
    public int? CurrencyId { get => fields.CurrencyId[this]; set => fields.CurrencyId[this] = value; }
    public partial class RowFields { public Int32Field CurrencyId; }

    [DisplayName("KDV Oranı"), LookupInclude, ForeignKey(typeof(Setting.VatRatesRow)), LeftJoin(jVatRate)]
    [LookupEditor(typeof(Setting.VatRatesRow), FilterField = "IsActive", FilterValue = true)]
    public int? VatRateId { get => fields.VatRateId[this]; set => fields.VatRateId[this] = value; }
    public partial class RowFields { public Int32Field VatRateId; }

    [DisplayName("Birim Fiyat (Baz)"), DecimalEditor(Decimals = 4, MinValue = "0")]
    public decimal? UnitPrice { get => fields.UnitPrice[this]; set => fields.UnitPrice[this] = value; }
    public partial class RowFields { public DecimalField UnitPrice; }

    [DisplayName("Güncel Birim Fiyat"), NotMapped, DecimalEditor(Decimals = 4, MinValue = "0")]
    public decimal? CurrentValidPrice { get => fields.CurrentValidPrice[this]; set => fields.CurrentValidPrice[this] = value; }
    public partial class RowFields { public DecimalField CurrentValidPrice; }

    [DisplayName("Insert Date")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }
    
    [DisplayName("Insert User Id")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }
    
    [DisplayName("Update Date")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }
    
    [DisplayName("Update User Id")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }
    
    [DisplayName("Is Active"), LookupInclude]
    public short? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public Int16Field IsActive; }
    
    #region Foreign Fields

    [DisplayName("Kategori"), Expression($"{jCategory}.[Name]"), LookupInclude]
    public string CategoryName { get => fields.CategoryName[this]; set => fields.CategoryName[this] = value; }
    public partial class RowFields { public StringField CategoryName; }

    [DisplayName("Birim"), Expression($"{jUnit}.[Name]"), LookupInclude]
    public string UnitName { get => fields.UnitName[this]; set => fields.UnitName[this] = value; }
    public partial class RowFields { public StringField UnitName; }

    [DisplayName("Para Birimi"), Expression($"{jCurrency}.[Code]"), LookupInclude]
    public string CurrencyCode { get => fields.CurrencyCode[this]; set => fields.CurrencyCode[this] = value; }
    public partial class RowFields { public StringField CurrencyCode; }

    [DisplayName("KDV Oranı"), Expression($"{jVatRate}.[Name]"), LookupInclude]
    public string VatRateName { get => fields.VatRateName[this]; set => fields.VatRateName[this] = value; }
    public partial class RowFields { public StringField VatRateName; }

    [DisplayName("Marka"), Expression($"{jBrand}.[Name]"), LookupInclude]
    public string BrandName { get => fields.BrandName[this]; set => fields.BrandName[this] = value; }
    public partial class RowFields { public StringField BrandName; }

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}