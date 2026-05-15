import { BooleanEditor, DecimalEditor, initFormType, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface VendorTypeForm {
    Title: StringEditor;
    DiscountType: StringEditor;
    DiscountValue: DecimalEditor;
    Description: StringEditor;
    IsActive: BooleanEditor;
}

export class VendorTypeForm extends PrefixedContext {
    static readonly formKey = 'Setting.VendorType';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!VendorTypeForm.init) {
            VendorTypeForm.init = true;

            var w0 = StringEditor;
            var w1 = DecimalEditor;
            var w2 = BooleanEditor;

            initFormType(VendorTypeForm, [
                'Title', w0,
                'DiscountType', w0,
                'DiscountValue', w1,
                'Description', w0,
                'IsActive', w2
            ]);
        }
    }
}