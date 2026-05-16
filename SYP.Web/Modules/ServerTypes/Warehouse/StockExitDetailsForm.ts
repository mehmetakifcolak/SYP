import { DecimalEditor, initFormType, LookupEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface StockExitDetailsForm {
    ProductId: LookupEditor;
    Quantity: DecimalEditor;
    Notes: StringEditor;
}

export class StockExitDetailsForm extends PrefixedContext {
    static readonly formKey = 'Warehouse.StockExitDetails';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!StockExitDetailsForm.init) {
            StockExitDetailsForm.init = true;

            var w0 = LookupEditor;
            var w1 = DecimalEditor;
            var w2 = StringEditor;

            initFormType(StockExitDetailsForm, [
                'ProductId', w0,
                'Quantity', w1,
                'Notes', w2
            ]);
        }
    }
}
