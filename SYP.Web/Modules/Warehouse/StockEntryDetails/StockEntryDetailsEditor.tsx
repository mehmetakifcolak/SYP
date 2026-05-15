import { Decorators } from "@serenity-is/corelib";
import { GridEditorBase } from "@serenity-is/extensions";
import { StockEntryDetailsColumns, StockEntryDetailsRow } from "../../ServerTypes/Warehouse";
import { StockEntryDetailsEditDialog } from "./StockEntryDetailsEditDialog";

@Decorators.registerEditor("SYP.Warehouse.StockEntryDetailsEditor")
export class StockEntryDetailsEditor extends GridEditorBase<StockEntryDetailsRow> {
    protected getColumnsKey() { return StockEntryDetailsColumns.columnsKey; }
    protected getDialogType() { return StockEntryDetailsEditDialog; }
    protected getLocalTextPrefix() { return StockEntryDetailsRow.localTextPrefix; }

    constructor(props: any) {
        super(props);
    }

    protected getAddButtonCaption() {
        return "Ürün Ekle";
    }
}
