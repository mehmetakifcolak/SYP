import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";
import { PriceListItemsRow } from "./PriceListItemsRow";

export interface PriceListsRow {
    Id?: number;
    Code?: string;
    Name?: string;
    Description?: string;
    CurrencyId?: number;
    ValidFrom?: string;
    ValidTo?: string;
    IsActive?: number;
    IsDefault?: boolean;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    CurrencyCode?: string;
    CurrencyName?: string;
    ItemList?: PriceListItemsRow[];
}

export abstract class PriceListsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Catalog.PriceLists';
    static readonly lookupKey = 'Catalog.PriceLists';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<PriceListsRow>('Catalog.PriceLists') }
    static async getLookupAsync() { return getLookupAsync<PriceListsRow>('Catalog.PriceLists') }

    static readonly deletePermission = 'Catalog:PriceLists:Delete';
    static readonly insertPermission = 'Catalog:PriceLists:Insert';
    static readonly readPermission = 'Catalog:PriceLists:Read';
    static readonly updatePermission = 'Catalog:PriceLists:Update';

    static readonly Fields = fieldsProxy<PriceListsRow>();
}