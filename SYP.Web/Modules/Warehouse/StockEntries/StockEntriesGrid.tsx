import { Decorators } from "@serenity-is/corelib";
import { StockEntriesColumns, StockEntriesRow, StockEntriesService } from "../../ServerTypes/Warehouse";
import { StockEntriesDialog } from "./StockEntriesDialog";
import { GridBase } from "../../_Ext/Bases/GridBase";

@Decorators.registerClass("SYP.Warehouse.StockEntriesGrid")
export class StockEntriesGrid extends GridBase<StockEntriesRow, any> {
    protected getColumnsKey() { return StockEntriesColumns.columnsKey; }
    protected getDialogType() { return StockEntriesDialog; }
    protected getRowDefinition() { return StockEntriesRow; }
    protected getService() { return StockEntriesService.baseUrl; }
}
