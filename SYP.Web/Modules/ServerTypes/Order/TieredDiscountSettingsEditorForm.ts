import { BooleanEditor, DateEditor, DecimalEditor, initFormType, IntegerEditor, PrefixedContext } from "@serenity-is/corelib";

export interface TieredDiscountSettingsEditorForm {
    MinAmount: DecimalEditor;
    DiscountPercentage: DecimalEditor;
    IsActive: BooleanEditor;
    DisplayOrder: IntegerEditor;
    InsertDate: DateEditor;
    InsertUserId: IntegerEditor;
    UpdateDate: DateEditor;
    UpdateUserId: IntegerEditor;
}

export class TieredDiscountSettingsEditorForm extends PrefixedContext {
    static readonly formKey = 'Order.TieredDiscountSettingsEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!TieredDiscountSettingsEditorForm.init) {
            TieredDiscountSettingsEditorForm.init = true;

            var w0 = DecimalEditor;
            var w1 = BooleanEditor;
            var w2 = IntegerEditor;
            var w3 = DateEditor;

            initFormType(TieredDiscountSettingsEditorForm, [
                'MinAmount', w0,
                'DiscountPercentage', w0,
                'IsActive', w1,
                'DisplayOrder', w2,
                'InsertDate', w3,
                'InsertUserId', w2,
                'UpdateDate', w3,
                'UpdateUserId', w2
            ]);
        }
    }
}