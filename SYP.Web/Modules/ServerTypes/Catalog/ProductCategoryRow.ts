import { fieldsProxy } from "@serenity-is/corelib";

export interface ProductCategoryRow {
    Id?: number;
    ParentId?: number;
    Name?: string;
    FullPath?: string;
    SortOrder?: number;
    IsActive?: boolean;
    CreatedAt?: string;
    ParentName?: string;
}

export abstract class ProductCategoryRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Catalog.ProductCategory';
    static readonly deletePermission = 'Catalog:ProductCategory:Delete';
    static readonly insertPermission = 'Catalog:ProductCategory:Insert';
    static readonly readPermission = 'Catalog:ProductCategory:Read';
    static readonly updatePermission = 'Catalog:ProductCategory:Update';

    static readonly Fields = fieldsProxy<ProductCategoryRow>();
}