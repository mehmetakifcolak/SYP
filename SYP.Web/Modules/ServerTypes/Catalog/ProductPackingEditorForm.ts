import { BooleanEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface ProductPackingEditorForm {
    Code: StringEditor;
    Name: StringEditor;
    Quantity: IntegerEditor;
    IsActive: BooleanEditor;
}

export class ProductPackingEditorForm extends PrefixedContext {
    static readonly formKey = 'Catalog.ProductPackingEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductPackingEditorForm.init) {
            ProductPackingEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = IntegerEditor;
            var w2 = BooleanEditor;

            initFormType(ProductPackingEditorForm, [
                'Code', w0,
                'Name', w0,
                'Quantity', w1,
                'IsActive', w2
            ]);
        }
    }
}
