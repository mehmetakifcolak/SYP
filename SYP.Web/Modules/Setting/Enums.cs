using Serenity.ComponentModel;
using System.ComponentModel;

namespace SYP.Setting;

[EnumKey("NumberTemplateType"), ScriptInclude]
public enum NumberTemplateType
{
    [Description("Müşteri")]
    Musteri = 1,
    [Description("Ürün")]
    Urun = 2,
    [Description("Satış Teklifi")]
    SatisTeklifi = 3,
    [Description("Satış Siparişi")]
    SatisSiparisi = 4,
    [Description("Satınalma Teklifi")]
    SatinalmaTeklifi = 5,
    [Description("Satınalma Siparişi")]
    SatinalmaSiparisi = 6,
    [Description("Fatura")]
    Fatura = 7,
    [Description("İrsaliye")]
    Irsaliye = 8,
    [Description("Stok Girişi")]
    StokGirisi = 9,
    [Description("Stok Çıkışı")]
    StokCikisi = 10,
    [Description("Fiyat Listesi")]
    FiyatListesi = 11,
    [Description("B2B Sipariş")]
    B2BSiparis = 12
}
