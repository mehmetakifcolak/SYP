import { fieldsProxy } from "@serenity-is/corelib";
import { StockExitStatus } from "../_Ext/StockExitStatus";
import { StockExitDetailsRow } from "./StockExitDetailsRow";

export interface StockExitsRow {
    Id?: number;
    ExitNo?: string;
    WarehouseId?: number;
    ExitDate?: string;
    Description?: string;
    Status?: StockExitStatus;
    Attachments?: string;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    DetailList?: StockExitDetailsRow[];
    WarehouseName?: string;
    WarehouseCode?: string;
}

export abstract class StockExitsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'ExitNo';
    static readonly localTextPrefix = 'Warehouse.StockExits';
    static readonly deletePermission = 'Warehouse:StockExits:Delete';
    static readonly insertPermission = 'Warehouse:StockExits:Insert';
    static readonly readPermission = 'Warehouse:StockExits:Read';
    static readonly updatePermission = 'Warehouse:StockExits:Update';

    static readonly Fields = fieldsProxy<StockExitsRow>();
}