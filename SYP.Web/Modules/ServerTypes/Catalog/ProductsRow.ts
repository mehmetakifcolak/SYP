import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface ProductsRow {
    Id?: number;
    Code?: string;
    Name?: string;
    CodeName?: string;
    Name2?: string;
    Description?: string;
    Barcode?: string;
    ProductImage?: string;
    CategoryId?: number;
    BrandId?: number;
    UnitId?: number;
    CurrencyId?: number;
    VatRateId?: number;
    UnitPrice?: number;
    CurrentValidPrice?: number;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    IsActive?: number;
    CategoryName?: string;
    UnitName?: string;
    CurrencyCode?: string;
    VatRateName?: string;
    VatRate?: number;
    BrandName?: string;
}

export abstract class ProductsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'CodeName';
    static readonly localTextPrefix = 'Catalog.Products';
    static readonly lookupKey = 'Catalog.Products';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<ProductsRow>('Catalog.Products') }
    static async getLookupAsync() { return getLookupAsync<ProductsRow>('Catalog.Products') }

    static readonly deletePermission = 'Catalog:Products:Delete';
    static readonly insertPermission = 'Catalog:Products:Insert';
    static readonly readPermission = 'Catalog:Products:Read';
    static readonly updatePermission = 'Catalog:Products:Update';

    static readonly Fields = fieldsProxy<ProductsRow>();
}