import { Decorators } from "@serenity-is/corelib";
import { GridBase } from "../../_Ext/Bases/GridBase";
import { PriceListsColumns, PriceListsRow, PriceListsService } from "../../ServerTypes/Catalog";
import { PriceListsDialog } from "./PriceListsDialog";

@Decorators.registerClass("SYP.Catalog.PriceListsGrid")
export class PriceListsGrid extends GridBase<PriceListsRow, any> {
    protected getColumnsKey() { return PriceListsColumns.columnsKey; }
    protected getDialogType() { return PriceListsDialog; }
    protected getRowDefinition() { return PriceListsRow; }
    protected getService() { return PriceListsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getQuickFilters() {
        const filters = super.getQuickFilters();

        filters.push({
            type: "Boolean",
            field: "IsActive",
            title: "Aktif",
            handler: h => {
                h.active = h.value != null;
                if (h.active) {
                    h.request.Criteria = [[["IsActive"], "=", h.value ? 1 : 0]];
                }
            },
            init: w => {
                w.value = true;
            }
        } as any);

        return filters;
    }
}
