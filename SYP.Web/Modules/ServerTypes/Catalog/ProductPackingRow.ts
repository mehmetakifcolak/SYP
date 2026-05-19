import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface ProductPackingRow {
    Id?: number;
    Code?: string;
    Name?: string;
    Description?: string;
    Quantity?: number;
    IsActive?: boolean;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class ProductPackingRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Catalog.ProductPacking';
    static readonly lookupKey = 'Catalog.ProductPacking';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<ProductPackingRow>('Catalog.ProductPacking') }
    static async getLookupAsync() { return getLookupAsync<ProductPackingRow>('Catalog.ProductPacking') }

    static readonly deletePermission = 'Catalog:ProductPacking:Delete';
    static readonly insertPermission = 'Catalog:ProductPacking:Insert';
    static readonly readPermission = 'Catalog:ProductPacking:Read';
    static readonly updatePermission = 'Catalog:ProductPacking:Update';

    static readonly Fields = fieldsProxy<ProductPackingRow>();
}
