import { DecimalEditor, initFormType, LookupEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface PriceListItemsForm {
    ProductId: LookupEditor;
    UnitPrice: DecimalEditor;
    DiscountRate: DecimalEditor;
    Notes: StringEditor;
}

export class PriceListItemsForm extends PrefixedContext {
    static readonly formKey = 'Catalog.PriceListItems';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!PriceListItemsForm.init) {
            PriceListItemsForm.init = true;

            var w0 = LookupEditor;
            var w1 = DecimalEditor;
            var w2 = StringEditor;

            initFormType(PriceListItemsForm, [
                'ProductId', w0,
                'UnitPrice', w1,
                'DiscountRate', w1,
                'Notes', w2
            ]);
        }
    }
}