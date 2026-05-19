import { fieldsProxy } from "@serenity-is/corelib";
import { PriceListItemsRow } from "./PriceListItemsRow";
import { PriceListType } from "./PriceListType";

export interface PriceListsRow {
    Id?: number;
    Code?: string;
    Name?: string;
    Type?: PriceListType;
    Description?: string;
    CurrencyId?: number;
    ValidFrom?: string;
    ValidTo?: string;
    IsActive?: boolean;
    IsDefault?: boolean;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    CurrencyCode?: string;
    ItemList?: PriceListItemsRow[];
}

export abstract class PriceListsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Code';
    static readonly localTextPrefix = 'Catalog.PriceLists';
    static readonly deletePermission = 'Catalog:PriceLists:Delete';
    static readonly insertPermission = 'Catalog:PriceLists:Insert';
    static readonly readPermission = 'Catalog:PriceLists:Read';
    static readonly updatePermission = 'Catalog:PriceLists:Update';

    static readonly Fields = fieldsProxy<PriceListsRow>();
}