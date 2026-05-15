import { DecimalEditor, initFormType, LookupEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface StockEntryDetailsForm {
    ProductId: LookupEditor;
    Quantity: DecimalEditor;
    Notes: StringEditor;
}

export class StockEntryDetailsForm extends PrefixedContext {
    static readonly formKey = 'Warehouse.StockEntryDetails';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!StockEntryDetailsForm.init) {
            StockEntryDetailsForm.init = true;

            var w0 = LookupEditor;
            var w1 = DecimalEditor;
            var w2 = StringEditor;

            initFormType(StockEntryDetailsForm, [
                'ProductId', w0,
                'Quantity', w1,
                'Notes', w2
            ]);
        }
    }
}