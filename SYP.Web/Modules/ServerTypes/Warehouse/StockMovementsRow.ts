import { fieldsProxy } from "@serenity-is/corelib";

export interface StockMovementsRow {
    Id?: string;
    MovementType?: string;
    DocumentNo?: string;
    WarehouseId?: number;
    WarehouseCode?: string;
    WarehouseName?: string;
    ProductId?: number;
    ProductCode?: string;
    ProductName?: string;
    Quantity?: number;
    MovementDate?: string;
    Status?: number;
    Description?: string;
    InsertDate?: string;
    InsertUserId?: number;
}

export abstract class StockMovementsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'MovementType';
    static readonly localTextPrefix = 'Warehouse.StockMovements';
    static readonly deletePermission = 'Warehouse:StockMovements:Read';
    static readonly insertPermission = 'Warehouse:StockMovements:Read';
    static readonly readPermission = 'Warehouse:StockMovements:Read';
    static readonly updatePermission = 'Warehouse:StockMovements:Read';

    static readonly Fields = fieldsProxy<StockMovementsRow>();
}