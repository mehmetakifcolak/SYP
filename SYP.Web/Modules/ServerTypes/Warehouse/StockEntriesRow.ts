import { fieldsProxy } from "@serenity-is/corelib";
import { StockEntryStatus } from "../_Ext/StockEntryStatus";
import { StockEntryDetailsRow } from "./StockEntryDetailsRow";

export interface StockEntriesRow {
    Id?: number;
    EntryNo?: string;
    WarehouseId?: number;
    EntryDate?: string;
    Description?: string;
    Status?: StockEntryStatus;
    Attachments?: string;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    DetailList?: StockEntryDetailsRow[];
    WarehouseName?: string;
    WarehouseCode?: string;
}

export abstract class StockEntriesRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'EntryNo';
    static readonly localTextPrefix = 'Warehouse.StockEntries';
    static readonly deletePermission = 'Warehouse:StockEntries:Delete';
    static readonly insertPermission = 'Warehouse:StockEntries:Insert';
    static readonly readPermission = 'Warehouse:StockEntries:Read';
    static readonly updatePermission = 'Warehouse:StockEntries:Update';

    static readonly Fields = fieldsProxy<StockEntriesRow>();
}