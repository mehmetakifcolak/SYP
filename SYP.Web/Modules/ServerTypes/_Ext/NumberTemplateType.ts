import { registerEnum } from "@serenity-is/corelib";

export enum NumberTemplateType {
    Musteri = 1,
    Urun = 2,
    SatisTeklifi = 3,
    SatisSiparisi = 4,
    SatinalmaTeklifi = 5,
    SatinalmaSiparisi = 6,
    Fatura = 7,
    Irsaliye = 8,
    StokGirisi = 9,
    StokCikisi = 10,
    FiyatListesi = 11,
    B2BSiparis = 12
}
registerEnum(NumberTemplateType, '_Ext.NumberTemplateType', 'NumberTemplateType');