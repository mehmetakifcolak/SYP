import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface BrandsRow {
    Id?: number;
    Name?: string;
    Description?: string;
    Logo?: string;
    IsActive?: number;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class BrandsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Catalog.Brands';
    static readonly lookupKey = 'Catalog.Brands';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<BrandsRow>('Catalog.Brands') }
    static async getLookupAsync() { return getLookupAsync<BrandsRow>('Catalog.Brands') }

    static readonly deletePermission = 'Catalog:Brands:Delete';
    static readonly insertPermission = 'Catalog:Brands:Insert';
    static readonly readPermission = 'Catalog:Brands:Read';
    static readonly updatePermission = 'Catalog:Brands:Update';

    static readonly Fields = fieldsProxy<BrandsRow>();
}