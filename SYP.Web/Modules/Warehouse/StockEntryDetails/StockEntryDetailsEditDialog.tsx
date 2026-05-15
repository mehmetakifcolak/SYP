import { Decorators } from "@serenity-is/corelib";
import { GridEditorDialog } from "@serenity-is/extensions";
import { StockEntryDetailsForm, StockEntryDetailsRow } from "../../ServerTypes/Warehouse";

@Decorators.registerClass("SYP.Warehouse.StockEntryDetailsEditDialog")
export class StockEntryDetailsEditDialog extends GridEditorDialog<StockEntryDetailsRow> {
    protected getFormKey() { return StockEntryDetailsForm.formKey; }
    protected getLocalTextPrefix() { return StockEntryDetailsRow.localTextPrefix; }

    protected form = new StockEntryDetailsForm(this.idPrefix);

    constructor(props?: any) {
        super(props);
    }

    protected getDialogTitle(): string {
        return this.isNew() ? "Yeni Ürün Ekle" : "Ürün Düzenle";
    }
}
