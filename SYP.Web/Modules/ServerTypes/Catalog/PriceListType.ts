import { registerEnum } from "@serenity-is/corelib";

export enum PriceListType {
    Sales = 1,
    Purchase = 2
}
registerEnum(PriceListType, 'SYP.Catalog.PriceListType', 'Catalog.PriceListType');