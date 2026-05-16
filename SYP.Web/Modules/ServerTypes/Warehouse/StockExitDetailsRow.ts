import { fieldsProxy } from "@serenity-is/corelib";

export interface StockExitDetailsRow {
    Id?: number;
    StockExitId?: number;
    ProductId?: number;
    Quantity?: number;
    Notes?: string;
    ProductCode?: string;
    ProductName?: string;
}

export abstract class StockExitDetailsRow {
    static readonly idProperty = 'Id';
    static readonly localTextPrefix = 'Warehouse.StockExitDetails';
    static readonly deletePermission = 'Warehouse:StockExits:Delete';
    static readonly insertPermission = 'Warehouse:StockExits:Insert';
    static readonly readPermission = 'Warehouse:StockExits:Read';
    static readonly updatePermission = 'Warehouse:StockExits:Update';

    static readonly Fields = fieldsProxy<StockExitDetailsRow>();
}
