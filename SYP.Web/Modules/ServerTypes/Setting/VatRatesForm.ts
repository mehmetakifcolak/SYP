import { BooleanEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface VatRatesForm {
    Name: StringEditor;
    Code: StringEditor;
    IsDefault: BooleanEditor;
    IsActive: BooleanEditor;
    SortOrder: IntegerEditor;
}

export class VatRatesForm extends PrefixedContext {
    static readonly formKey = 'Setting.VatRates';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!VatRatesForm.init) {
            VatRatesForm.init = true;

            var w0 = StringEditor;
            var w1 = BooleanEditor;
            var w2 = IntegerEditor;

            initFormType(VatRatesForm, [
                'Name', w0,
                'Code', w0,
                'IsDefault', w1,
                'IsActive', w1,
                'SortOrder', w2
            ]);
        }
    }
}