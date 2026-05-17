import { Decorators, EntityGrid} from "@serenity-is/corelib";
import { StockEntriesColumns, StockEntriesRow, StockEntriesService } from "../../ServerTypes/Warehouse";
import { StockEntriesDialog } from "./StockEntriesDialog";

@Decorators.registerClass("SYP.Warehouse.StockEntriesGrid")
export class StockEntriesGrid extends EntityGrid<StockEntriesRow, any> {
    protected getColumnsKey() { return StockEntriesColumns.columnsKey; }
    protected getDialogType() { return StockEntriesDialog; }
    protected getRowDefinition() { return StockEntriesRow; }
    protected getService() { return StockEntriesService.baseUrl; }
}
