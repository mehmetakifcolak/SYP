using Serenity.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace _Ext //enums must have namespace otherwise it transforms to wrong typescript code
{

    [EnumKey("Months"), ScriptInclude]
    public enum Months
    {
        [Description("January")]
        January = 0,
        [Description("February")]
        February = 1,
        [Description("March")]
        March = 2,
        [Description("April")]
        April = 3,
        [Description("May")]
        May = 4,
        [Description("June")]
        June = 5,
        [Description("July")]
        July = 6,
        [Description("August")]
        August = 7,
        [Description("September")]
        September = 8,
        [Description("October")]
        October = 9,
        [Description("November")]
        November = 10,
        [Description("December")]
        December = 11
    }

    [EnumKey("TimeUoM"), ScriptInclude]
    public enum TimeUoM
    {

        Hour = 1,
        Day = 2,
        Week = 3,
        Month = 4,
        CalenderMonth = 5,
        Year = 6,
    }

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
        StokCikisi = 10
    }

    [EnumKey("Email.EmailQueueStatus"), ScriptInclude]
    public enum EmailQueueStatus
    {
        [Description("Beklemede")]
        Pending = 0,
        [Description("Zamanlanmış")]
        Scheduled = 1,
        [Description("İşleniyor")]
        Processing = 2,
        [Description("Gönderildi")]
        Sent = 3,
        [Description("Başarısız")]
        Failed = 4,
        [Description("İptal Edildi")]
        Cancelled = 5,
        [Description("Yeniden Deneniyor")]
        Retrying = 6
    }

    [EnumKey("Email.EmailPriority"), ScriptInclude]
    public enum EmailPriority
    {
        [Description("Yüksek")]
        High = 1,
        [Description("Normal")]
        Normal = 2,
        [Description("Düşük")]
        Low = 3
    }

    [EnumKey("Email.EmailLogStatus"), ScriptInclude]
    public enum EmailLogStatus
    {
        [Description("Kuyruğa Eklendi")]
        Queued = 0,
        [Description("İşleniyor")]
        Processing = 1,
        [Description("Gönderildi")]
        Sent = 2,
        [Description("Başarısız")]
        Failed = 3
    }

    [EnumKey("Warehouse.StockEntryStatus"), ScriptInclude]
    public enum StockEntryStatus
    {
        [Description("Taslak")]
        Draft = 0,
        [Description("Onaylandı")]
        Approved = 1,
        [Description("İptal")]
        Cancelled = 2
    }

    [EnumKey("Warehouse.StockExitStatus"), ScriptInclude]
    public enum StockExitStatus
    {
        [Description("Taslak")]
        Draft = 0,
        [Description("Onaylandı")]
        Approved = 1,
        [Description("İptal")]
        Cancelled = 2
    }
}