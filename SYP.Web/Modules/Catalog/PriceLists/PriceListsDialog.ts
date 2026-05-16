import { Decorators } from "@serenity-is/corelib";
import { DialogBase } from "../../_Ext/Bases/DialogBase";
import { PriceListsForm, PriceListsRow, PriceListsService } from "../../ServerTypes/Catalog";

@Decorators.registerClass("SYP.Catalog.PriceListsDialog")
export class PriceListsDialog extends DialogBase<PriceListsRow, any> {
    protected getFormKey() { return PriceListsForm.formKey; }
    protected getRowDefinition() { return PriceListsRow; }
    protected getService() { return PriceListsService.baseUrl; }

    protected form = new PriceListsForm(this.idPrefix);
}
