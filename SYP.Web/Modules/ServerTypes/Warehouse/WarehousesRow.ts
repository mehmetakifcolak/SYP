import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface WarehousesRow {
    Id?: number;
    Code?: string;
    Name?: string;
    Address?: string;
    City?: string;
    Phone?: string;
    Email?: string;
    ManagerName?: string;
    Description?: string;
    IsActive?: boolean;
    IsDefault?: boolean;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class WarehousesRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Warehouse.Warehouses';
    static readonly lookupKey = 'Warehouse.Warehouses';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<WarehousesRow>('Warehouse.Warehouses') }
    static async getLookupAsync() { return getLookupAsync<WarehousesRow>('Warehouse.Warehouses') }

    static readonly deletePermission = 'Warehouse:Warehouses:Delete';
    static readonly insertPermission = 'Warehouse:Warehouses:Insert';
    static readonly readPermission = 'Warehouse:Warehouses:Read';
    static readonly updatePermission = 'Warehouse:Warehouses:Update';

    static readonly Fields = fieldsProxy<WarehousesRow>();
}