using Serenity.ComponentModel;
using Serenity.Data;
using Serenity.Data.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SYP.Catalog;

[ConnectionKey("Default"), Module("Catalog"), TableName("PriceLists")]
[DisplayName("Fiyat Listeleri"), InstanceName("Fiyat Listesi")]
[LookupScript("Catalog.PriceLists", Permission = "Catalog:PriceLists:Read")]
[ReadPermission("Catalog:PriceLists:Read")]
[InsertPermission("Catalog:PriceLists:Insert")]
[UpdatePermission("Catalog:PriceLists:Update")]
[DeletePermission("Catalog:PriceLists:Delete")]
public sealed class PriceListsRow : Row<PriceListsRow.RowFields>, IIdRow, INameRow, SYP.Administration.IAuditedRow
{
    const string jCurrency = nameof(jCurrency);

    [DisplayName("Id"), Identity, IdProperty]
    public int? Id { get => fields.Id[this]; set => fields.Id[this] = value; }
    public partial class RowFields { public Int32Field Id; }

    [DisplayName("Kod"), Size(50), NotNull, QuickSearch, LookupInclude]
    public string Code { get => fields.Code[this]; set => fields.Code[this] = value; }
    public partial class RowFields { public StringField Code; }

    [DisplayName("Ad"), Size(200), NotNull, NameProperty, LookupInclude]
    public string Name { get => fields.Name[this]; set => fields.Name[this] = value; }
    public partial class RowFields { public StringField Name; }

    [DisplayName("Açıklama"), Size(500)]
    public string Description { get => fields.Description[this]; set => fields.Description[this] = value; }
    public partial class RowFields { public StringField Description; }

    [DisplayName("Para Birimi"), NotNull, ForeignKey(typeof(Setting.CurrencyListRow)), LeftJoin(jCurrency), LookupInclude]
    [LookupEditor(typeof(Setting.CurrencyListRow), FilterField = "IsActive", FilterValue = true)]
    public int? CurrencyId { get => fields.CurrencyId[this]; set => fields.CurrencyId[this] = value; }
    public partial class RowFields { public Int32Field CurrencyId; }

    [DisplayName("Geçerlilik Başlangıç"), LookupInclude]
    public DateTime? ValidFrom { get => fields.ValidFrom[this]; set => fields.ValidFrom[this] = value; }
    public partial class RowFields { public DateTimeField ValidFrom; }

    [DisplayName("Son Kullanma Tarihi"), LookupInclude]
    public DateTime? ValidTo { get => fields.ValidTo[this]; set => fields.ValidTo[this] = value; }
    public partial class RowFields { public DateTimeField ValidTo; }

    [DisplayName("Aktif"), NotNull, DefaultValue(1), LookupInclude]
    public short? IsActive { get => fields.IsActive[this]; set => fields.IsActive[this] = value; }
    public partial class RowFields { public Int16Field IsActive; }

    [DisplayName("Varsayılan"), NotNull, DefaultValue(false)]
    public bool? IsDefault { get => fields.IsDefault[this]; set => fields.IsDefault[this] = value; }
    public partial class RowFields { public BooleanField IsDefault; }

    [DisplayName("Kayıt Tarihi")]
    public DateTime? InsertDate { get => fields.InsertDate[this]; set => fields.InsertDate[this] = value; }
    public partial class RowFields { public DateTimeField InsertDate; }

    [DisplayName("Kaydeden")]
    public int? InsertUserId { get => fields.InsertUserId[this]; set => fields.InsertUserId[this] = value; }
    public partial class RowFields { public Int32Field InsertUserId; }

    [DisplayName("Güncelleme Tarihi")]
    public DateTime? UpdateDate { get => fields.UpdateDate[this]; set => fields.UpdateDate[this] = value; }
    public partial class RowFields { public DateTimeField UpdateDate; }

    [DisplayName("Güncelleyen")]
    public int? UpdateUserId { get => fields.UpdateUserId[this]; set => fields.UpdateUserId[this] = value; }
    public partial class RowFields { public Int32Field UpdateUserId; }

    #region Foreign Fields

    [DisplayName("Para Birimi"), Expression($"{jCurrency}.[Code]"), LookupInclude]
    public string CurrencyCode { get => fields.CurrencyCode[this]; set => fields.CurrencyCode[this] = value; }
    public partial class RowFields { public StringField CurrencyCode; }

    [DisplayName("Para Birimi Adı"), Expression($"{jCurrency}.[Name]")]
    public string CurrencyName { get => fields.CurrencyName[this]; set => fields.CurrencyName[this] = value; }
    public partial class RowFields { public StringField CurrencyName; }

    #endregion

    #region Detail List

    [DisplayName("Kalemler"), NotMapped]
    public List<PriceListItemsRow> ItemList { get => fields.ItemList[this]; set => fields.ItemList[this] = value; }
    public partial class RowFields { public RowListField<PriceListItemsRow> ItemList; }

    #endregion

    public partial class RowFields : RowFieldsBase { }
}
