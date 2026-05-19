import { fieldsProxy } from "@serenity-is/corelib";
import { PriceListType } from "./PriceListType";

export interface PriceListItemsRow {
    Id?: number;
    PriceListId?: number;
    ProductId?: number;
    UnitPrice?: number;
    DiscountRate?: number;
    Notes?: string;
    ProductCode?: string;
    ProductName?: string;
    PriceListCode?: string;
    PriceListName?: string;
    PriceListValidFrom?: string;
    PriceListValidTo?: string;
    PriceListIsActive?: boolean;
    PriceListIsDefault?: boolean;
    PriceListType?: PriceListType;
}

export abstract class PriceListItemsRow {
    static readonly idProperty = 'Id';
    static readonly localTextPrefix = 'Catalog.PriceListItems';
    static readonly deletePermission = 'Catalog:PriceLists:Delete';
    static readonly insertPermission = 'Catalog:PriceLists:Insert';
    static readonly readPermission = 'Catalog:PriceLists:Read';
    static readonly updatePermission = 'Catalog:PriceLists:Update';

    static readonly Fields = fieldsProxy<PriceListItemsRow>();
}