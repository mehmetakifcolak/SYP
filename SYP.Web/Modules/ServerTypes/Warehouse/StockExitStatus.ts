import { registerEnum } from "@serenity-is/corelib";

export enum StockExitStatus {
    Draft = 0,
    Approved = 1,
    Cancelled = 2
}
registerEnum(StockExitStatus, 'SYP.Warehouse.StockExitStatus', 'Warehouse.StockExitStatus');