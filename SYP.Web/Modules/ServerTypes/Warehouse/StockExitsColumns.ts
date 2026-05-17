import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { StockExitsRow } from "./StockExitsRow";
import { StockExitStatus } from "./StockExitStatus";

export interface StockExitsColumns {
    ExitNo: Column<StockExitsRow>;
    WarehouseName: Column<StockExitsRow>;
    ExitDate: Column<StockExitsRow>;
    Status: Column<StockExitsRow>;
    Description: Column<StockExitsRow>;
    InsertDate: Column<StockExitsRow>;
}

export class StockExitsColumns extends ColumnsBase<StockExitsRow> {
    static readonly columnsKey = 'Warehouse.StockExits';
    static readonly Fields = fieldsProxy<StockExitsColumns>();
}

[StockExitStatus]; // referenced types