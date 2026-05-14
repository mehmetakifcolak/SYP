import { BooleanEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface VatRatesEditorForm {
    Name: StringEditor;
    Code: StringEditor;
    IsDefault: BooleanEditor;
    IsActive: BooleanEditor;
    SortOrder: IntegerEditor;
}

export class VatRatesEditorForm extends PrefixedContext {
    static readonly formKey = 'Setting.VatRatesEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!VatRatesEditorForm.init) {
            VatRatesEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = BooleanEditor;
            var w2 = IntegerEditor;

            initFormType(VatRatesEditorForm, [
                'Name', w0,
                'Code', w0,
                'IsDefault', w1,
                'IsActive', w1,
                'SortOrder', w2
            ]);
        }
    }
}