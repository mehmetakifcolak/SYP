import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { StockEntryDetailsRow } from "./StockEntryDetailsRow";

export interface StockEntryDetailsColumns {
    ProductCode: Column<StockEntryDetailsRow>;
    ProductName: Column<StockEntryDetailsRow>;
    Quantity: Column<StockEntryDetailsRow>;
    Currency: Column<StockEntryDetailsRow>;
    UnitPrice: Column<StockEntryDetailsRow>;
    Notes: Column<StockEntryDetailsRow>;
}

export class StockEntryDetailsColumns extends ColumnsBase<StockEntryDetailsRow> {
    static readonly columnsKey = 'Warehouse.StockEntryDetails';
    static readonly Fields = fieldsProxy<StockEntryDetailsColumns>();
}