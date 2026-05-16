import { fieldsProxy } from "@serenity-is/corelib";

export interface BrandsRow {
    Id?: number;
    Name?: string;
    Description?: string;
    Logo?: string;
    IsActive?: boolean;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class BrandsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Products.Brands';
    static readonly deletePermission = 'Products:Brands:Delete';
    static readonly insertPermission = 'Products:Brands:Insert';
    static readonly readPermission = 'Products:Brands:Read';
    static readonly updatePermission = 'Products:Brands:Update';

    static readonly Fields = fieldsProxy<BrandsRow>();
}