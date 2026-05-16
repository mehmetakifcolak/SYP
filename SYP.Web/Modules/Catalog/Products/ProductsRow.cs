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
[ServiceLookupPermission("Catalog:Products:Lookup")]
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

    //[DisplayName("Unit"), Size(10)]
    //public string Unit { get => fields.Unit[this]; set => fields.Unit[this] = value; }
    //public partial class RowFields { public StringField Unit; }
    
    //[DisplayName("Currency"), Size(10)]
    //public string Currency { get => fields.Currency[this]; set => fields.Currency[this] = value; }
    //public partial class RowFields { public StringField Currency; }
    
    //[DisplayName("Vat Rate"), Size(10)]
    //public string VatRate { get => fields.VatRate[this]; set => fields.VatRate[this] = value; }
    //public partial class RowFields { public StringField VatRate; }
    
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

    #endregion Foreign Fields

    public partial class RowFields : RowFieldsBase { }
}