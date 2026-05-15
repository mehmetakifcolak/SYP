import { BooleanEditor, DecimalEditor, initFormType, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface VendorTypeEditorForm {
    Title: StringEditor;
    DiscountType: StringEditor;
    DiscountValue: DecimalEditor;
    Description: StringEditor;
    IsActive: BooleanEditor;
}

export class VendorTypeEditorForm extends PrefixedContext {
    static readonly formKey = 'Setting.VendorTypeEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!VendorTypeEditorForm.init) {
            VendorTypeEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = DecimalEditor;
            var w2 = BooleanEditor;

            initFormType(VendorTypeEditorForm, [
                'Title', w0,
                'DiscountType', w0,
                'DiscountValue', w1,
                'Description', w0,
                'IsActive', w2
            ]);
        }
    }
}