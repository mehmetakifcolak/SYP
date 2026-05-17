import { Decorators, EntityGrid} from "@serenity-is/corelib";
import { StockExitsColumns, StockExitsRow, StockExitsService } from "../../ServerTypes/Warehouse";
import { StockExitsDialog } from "./StockExitsDialog";

@Decorators.registerClass("SYP.Warehouse.StockExitsGrid")
export class StockExitsGrid extends EntityGrid<StockExitsRow, any> {
    protected getColumnsKey() { return StockExitsColumns.columnsKey; }
    protected getDialogType() { return StockExitsDialog; }
    protected getRowDefinition() { return StockExitsRow; }
    protected getService() { return StockExitsService.baseUrl; }
}
