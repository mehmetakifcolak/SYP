using Serenity.ComponentModel;
using System.ComponentModel;

namespace SYP.Order;

[EnumKey("Order.OrderStatus"), ScriptInclude]
public enum OrderStatus
{
    [Description("Talep Gönderildi")]
    TALEP_GONDERILDI = 1,
    [Description("Revize Edildi")]
    REVIZE_EDILDI = 2,
    [Description("Bayi Onayladı")]
    BAYI_ONAYLADI = 3,
    [Description("Bayi Reddetti")]
    BAYI_REDDETTI = 4,
    [Description("Dekont Yüklendi")]
    DEKONT_YUKLENDI = 5,
    [Description("Dekont Reddedildi")]
    DEKONT_REDDEDILDI = 6,
    [Description("Hazırlanıyor")]
    HAZIRLANIYOR = 7,
    [Description("Sevk Aşamasında")]
    SEVK_ASAMASINDA = 8,
    [Description("Teslim Edildi")]
    TESLIM_EDILDI = 9,
    [Description("Talep İptal")]
    TALEP_IPTAL = 10
}

[EnumKey("Order.DocumentType"), ScriptInclude]
public enum DocumentType
{
    [Description("Dekont")]
    Dekont = 1,
    [Description("Fatura")]
    Fatura = 2,
    [Description("İrsaliye")]
    Irsaliye = 3,
    [Description("Diğer")]
    Diger = 4
}
