import { BooleanEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";

export interface ProductPackingForm {
    Code: StringEditor;
    Name: StringEditor;
    Description: TextAreaEditor;
    Quantity: IntegerEditor;
    IsActive: BooleanEditor;
}

export class ProductPackingForm extends PrefixedContext {
    static readonly formKey = 'Catalog.ProductPacking';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductPackingForm.init) {
            ProductPackingForm.init = true;

            var w0 = StringEditor;
            var w1 = TextAreaEditor;
            var w2 = IntegerEditor;
            var w3 = BooleanEditor;

            initFormType(ProductPackingForm, [
                'Code', w0,
                'Name', w0,
                'Description', w1,
                'Quantity', w2,
                'IsActive', w3
            ]);
        }
    }
}
