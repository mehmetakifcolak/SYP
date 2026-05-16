import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { StockMovementsRow } from "./StockMovementsRow";

export interface StockMovementsColumns {
    MovementType: Column<StockMovementsRow>;
    DocumentNo: Column<StockMovementsRow>;
    MovementDate: Column<StockMovementsRow>;
    WarehouseName: Column<StockMovementsRow>;
    ProductCode: Column<StockMovementsRow>;
    ProductName: Column<StockMovementsRow>;
    Quantity: Column<StockMovementsRow>;
    Status: Column<StockMovementsRow>;
    Description: Column<StockMovementsRow>;
}

export class StockMovementsColumns extends ColumnsBase<StockMovementsRow> {
    static readonly columnsKey = 'Warehouse.StockMovements';
    static readonly Fields = fieldsProxy<StockMovementsColumns>();
}
