import { fieldsProxy } from "@serenity-is/corelib";

export interface StockEntryDetailsRow {
    Id?: number;
    StockEntryId?: number;
    ProductId?: number;
    Quantity?: number;
    Unit?: string;
    Currency?: string;
    VatRate?: number;
    UnitPrice?: number;
    Notes?: string;
    ProductCode?: string;
    ProductName?: string;
}

export abstract class StockEntryDetailsRow {
    static readonly idProperty = 'Id';
    static readonly localTextPrefix = 'Warehouse.StockEntryDetails';
    static readonly deletePermission = 'Warehouse:StockEntries:Delete';
    static readonly insertPermission = 'Warehouse:StockEntries:Insert';
    static readonly readPermission = 'Warehouse:StockEntries:Read';
    static readonly updatePermission = 'Warehouse:StockEntries:Update';

    static readonly Fields = fieldsProxy<StockEntryDetailsRow>();
}