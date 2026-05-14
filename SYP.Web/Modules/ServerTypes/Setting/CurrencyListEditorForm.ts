import { BooleanEditor, DateEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface CurrencyListEditorForm {
    Code: StringEditor;
    Name: StringEditor;
    CodeType: IntegerEditor;
    Symbol: StringEditor;
    IsActive: BooleanEditor;
    InsertUserId: IntegerEditor;
    InsertDate: DateEditor;
    UpdateUserId: IntegerEditor;
    UpdateDate: DateEditor;
    IsDefaultCurrency: BooleanEditor;
    DefaultExchangeType: IntegerEditor;
    TenantId: IntegerEditor;
}

export class CurrencyListEditorForm extends PrefixedContext {
    static readonly formKey = 'Setting.CurrencyListEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!CurrencyListEditorForm.init) {
            CurrencyListEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = IntegerEditor;
            var w2 = BooleanEditor;
            var w3 = DateEditor;

            initFormType(CurrencyListEditorForm, [
                'Code', w0,
                'Name', w0,
                'CodeType', w1,
                'Symbol', w0,
                'IsActive', w2,
                'InsertUserId', w1,
                'InsertDate', w3,
                'UpdateUserId', w1,
                'UpdateDate', w3,
                'IsDefaultCurrency', w2,
                'DefaultExchangeType', w1,
                'TenantId', w1
            ]);
        }
    }
}