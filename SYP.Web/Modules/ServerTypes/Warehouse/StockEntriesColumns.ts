import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { StockEntriesRow } from "./StockEntriesRow";
import { StockEntryStatus } from "./StockEntryStatus";

export interface StockEntriesColumns {
    EntryNo: Column<StockEntriesRow>;
    WarehouseName: Column<StockEntriesRow>;
    EntryDate: Column<StockEntriesRow>;
    Status: Column<StockEntriesRow>;
    Description: Column<StockEntriesRow>;
    InsertDate: Column<StockEntriesRow>;
}

export class StockEntriesColumns extends ColumnsBase<StockEntriesRow> {
    static readonly columnsKey = 'Warehouse.StockEntries';
    static readonly Fields = fieldsProxy<StockEntriesColumns>();
}

[StockEntryStatus]; // referenced types