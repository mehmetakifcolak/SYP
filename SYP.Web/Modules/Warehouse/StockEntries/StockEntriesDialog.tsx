import { Decorators, EntityDialog } from "@serenity-is/corelib";
import { StockEntriesForm, StockEntriesRow, StockEntriesService } from "../../ServerTypes/Warehouse";

@Decorators.registerClass("SYP.Warehouse.StockEntriesDialog")
export class StockEntriesDialog extends EntityDialog<StockEntriesRow, any> {
    protected getFormKey() { return StockEntriesForm.formKey; }
    protected getRowDefinition() { return StockEntriesRow; }
    protected getService() { return StockEntriesService.baseUrl; }

    protected form = new StockEntriesForm(this.idPrefix);
}
