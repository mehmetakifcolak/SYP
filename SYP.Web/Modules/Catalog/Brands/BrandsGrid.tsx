import { Decorators, EntityGrid } from "@serenity-is/corelib";
import { BrandsColumns, BrandsRow, BrandsService } from "../../ServerTypes/Catalog";
import { BrandsDialog } from "./BrandsDialog";

@Decorators.registerClass("SYP.Catalog.BrandsGrid")
export class BrandsGrid extends EntityGrid<BrandsRow> {
    protected getColumnsKey() { return BrandsColumns.columnsKey; }
    protected getDialogType() { return BrandsDialog; }
    protected getRowDefinition() { return BrandsRow; }
    protected getService() { return BrandsService.baseUrl; }
}
