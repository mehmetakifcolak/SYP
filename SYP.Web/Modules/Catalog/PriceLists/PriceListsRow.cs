namespace SYP.Catalog;

[ConnectionKey("Default"), Module("Catalog"), TableName("PriceLists")]
[DisplayName("Price Lists"), InstanceName("Price Lists")]
[NavigationPermission("Catalog:PriceLists:Navigation")]
[ReadPermission("Catalog:PriceLists:Read")]
[InsertPermission("Catalog:PriceLists:Insert")]
[UpdatePermission("Catalog:PriceLists:Update")]
[DeletePermission("Catalog:PriceLists:Delete")]
[ServiceLookupPermission("Catalog:PriceLists:Lookup")]
public sealed class PriceListsRow : Row<PriceListsRow.RowFields>, IIdRow, INameRow
{
    const string jCurrency = nameof(jCurrency);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }
    
    [DisplayName("Code"), Size(50), QuickSearch, NameProperty]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }
    
    [DisplayName("Name"), Size(200), NotNull]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Type"), NotNull, DefaultValue(PriceListType.Sales)]
    public PriceListType? Type { get => (PriceListType?)fields.Type[this]; set => fields.Type[this] = (int?)value; }
    public partial class RowFields { public Int32Field Type; }

    [DisplayName("Description"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }
    
    [DisplayName("Currency"), NotNull, ForeignKey(typeof(Setting.CurrencyListRow)), LeftJoin(jCurrency), TextualField(nameof(CurrencyCode))]
    [LookupEditor(typeof(Setting.CurrencyListRow), Async = true)]
    public int? CurrencyId { get => fields.CurrencyId[this]; set => fields.CurrencyId[this] = value; }
    public partial class RowFields { public Int32Field CurrencyId; }
    
    [DisplayName("Valid From")]
    public DateTime? ValidFrom { get => fields.ValidFrom[this]; set => fields.ValidFrom[this] = value; }
    public partial class RowFields { public DateTimeField ValidFrom; }
    
    [DisplayName("Valid To")]
    public DateTime? ValidTo { get => fields.ValidTo[this]; set => fields.ValidTo[this] = value; }
    public partial class RowFields { public DateTimeField ValidTo; }
    
    [DisplayName("Is Active"), NotNull]
    public bool? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public BooleanField IsActive; }
    
    [DisplayName("Is Default"), NotNull]
    public bool? IsDefault { get => fields.IsDefault[this]; set => fields.IsDefault[this] = value; }
    public partial class RowFields { public BooleanField IsDefault; }
    
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
    
    #region Foreign Fields

    [DisplayName("Currency Code"), Origin(jCurrency, nameof(Setting.CurrencyListRow.Code)), ReadOnly(true)]
    public string CurrencyCode { get => fields.CurrencyCode[this]; set => fields.CurrencyCode[this] = value; }
    public partial class RowFields { public StringField CurrencyCode; }

    #endregion Foreign Fields

    [DisplayName("Products"), NotMapped]
    [MasterDetailRelation(foreignKey: "PriceListId", IncludeColumns = "ProductCode,ProductName,UnitPrice,DiscountRate,Notes")]
    public List<PriceListItemsRow> ItemList
    {
        get => fields.ItemList[this];
        set => fields.ItemList[this] = value;
    }
    public partial class RowFields { public RowListField<PriceListItemsRow> ItemList; }

    public partial class RowFields : RowFieldsBase { }
}