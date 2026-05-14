import { BooleanEditor, DateEditor, DecimalEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface DailyExchangesEditorForm {
    CurrencyId: IntegerEditor;
    CurrencyCode: StringEditor;
    ForexBuying: DecimalEditor;
    ForexSelling: DecimalEditor;
    BanknoteBuying: DecimalEditor;
    BanknoteSelling: DecimalEditor;
    InsertDate: DateEditor;
    InsertUserId: IntegerEditor;
    UpdateUserId: IntegerEditor;
    UpdateDate: DateEditor;
    IsActive: BooleanEditor;
    DefaultExchangeTypeId: IntegerEditor;
}

export class DailyExchangesEditorForm extends PrefixedContext {
    static readonly formKey = 'Setting.DailyExchangesEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!DailyExchangesEditorForm.init) {
            DailyExchangesEditorForm.init = true;

            var w0 = IntegerEditor;
            var w1 = StringEditor;
            var w2 = DecimalEditor;
            var w3 = DateEditor;
            var w4 = BooleanEditor;

            initFormType(DailyExchangesEditorForm, [
                'CurrencyId', w0,
                'CurrencyCode', w1,
                'ForexBuying', w2,
                'ForexSelling', w2,
                'BanknoteBuying', w2,
                'BanknoteSelling', w2,
                'InsertDate', w3,
                'InsertUserId', w0,
                'UpdateUserId', w0,
                'UpdateDate', w3,
                'IsActive', w4,
                'DefaultExchangeTypeId', w0
            ]);
        }
    }
}