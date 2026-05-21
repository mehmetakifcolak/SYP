import { registerEnum } from "@serenity-is/corelib";

export enum OrderStatus {
    TALEP_GONDERILDI = 1,
    REVIZE_EDILDI = 2,
    BAYI_ONAYLADI = 3,
    BAYI_REDDETTI = 4,
    DEKONT_YUKLENDI = 5,
    DEKONT_REDDEDILDI = 6,
    HAZIRLANIYOR = 7,
    SEVK_ASAMASINDA = 8,
    TESLIM_ALINDI = 9,
    TALEP_IPTAL = 10,
    TEMSILCI_ONAYLADI = 11,
    DEKONT_ONAYLANDI = 12,
    TESLIM_ALINMADI = 13,
    TALEP_BEKLETTE = 14
}
registerEnum(OrderStatus, 'SYP.Order.OrderStatus', 'Order.OrderStatus');