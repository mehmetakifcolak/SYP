import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { StockExitDetailsRow } from "./StockExitDetailsRow";

export interface StockExitDetailsColumns {
    ProductCode: Column<StockExitDetailsRow>;
    ProductName: Column<StockExitDetailsRow>;
    Quantity: Column<StockExitDetailsRow>;
    Notes: Column<StockExitDetailsRow>;
}

export class StockExitDetailsColumns extends ColumnsBase<StockExitDetailsRow> {
    static readonly columnsKey = 'Warehouse.StockExitDetails';
    static readonly Fields = fieldsProxy<StockExitDetailsColumns>();
}
