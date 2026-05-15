import { fieldsProxy } from "@serenity-is/corelib";

export interface WarehouseStockRow {
    Id?: number;
    WarehouseId?: number;
    ProductId?: number;
    Quantity?: number;
    LastUpdateDate?: string;
    WarehouseCode?: string;
    WarehouseName?: string;
    ProductCode?: string;
    ProductName?: string;
}

export abstract class WarehouseStockRow {
    static readonly idProperty = 'Id';
    static readonly localTextPrefix = 'Warehouse.WarehouseStock';
    static readonly deletePermission = 'Warehouse:WarehouseStock:Delete';
    static readonly insertPermission = 'Warehouse:WarehouseStock:Insert';
    static readonly readPermission = 'Warehouse:WarehouseStock:Read';
    static readonly updatePermission = 'Warehouse:WarehouseStock:Update';

    static readonly Fields = fieldsProxy<WarehouseStockRow>();
}