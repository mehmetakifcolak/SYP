import { fieldsProxy } from "@serenity-is/corelib";

export interface ProductsRow {
    Id?: number;
    Code?: string;
    Name?: string;
    Name2?: string;
    Description?: string;
    Barcode?: string;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    IsActive?: number;
}

export abstract class ProductsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Code';
    static readonly localTextPrefix = 'Catalog.Products';
    static readonly deletePermission = 'Catalog:Products:Delete';
    static readonly insertPermission = 'Catalog:Products:Insert';
    static readonly readPermission = 'Catalog:Products:Read';
    static readonly updatePermission = 'Catalog:Products:Update';

    static readonly Fields = fieldsProxy<ProductsRow>();
}