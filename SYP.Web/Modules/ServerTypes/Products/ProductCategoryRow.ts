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
    static readonly localTextPrefix = 'Products.ProductCategory';
    static readonly deletePermission = 'Products:ProductCategory:Delete';
    static readonly insertPermission = 'Products:ProductCategory:Insert';
    static readonly readPermission = 'Products:ProductCategory:Read';
    static readonly updatePermission = 'Products:ProductCategory:Update';

    static readonly Fields = fieldsProxy<ProductCategoryRow>();
}