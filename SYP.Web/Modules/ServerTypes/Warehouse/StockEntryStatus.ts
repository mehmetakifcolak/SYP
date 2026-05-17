import { registerEnum } from "@serenity-is/corelib";

export enum StockEntryStatus {
    Draft = 0,
    Approved = 1,
    Cancelled = 2
}
registerEnum(StockEntryStatus, 'SYP.Warehouse.StockEntryStatus', 'Warehouse.StockEntryStatus');