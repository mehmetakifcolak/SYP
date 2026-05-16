import { DateEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface CountryForm {
    Name: StringEditor;
    Code: StringEditor;
    IsoCode3: StringEditor;
    PhoneCode: StringEditor;
    CountryNr: StringEditor;
    InsertUserId: IntegerEditor;
    InsertDate: DateEditor;
    UpdateUserId: IntegerEditor;
    UpdateDate: DateEditor;
}

export class CountryForm extends PrefixedContext {
    static readonly formKey = 'Setting.Country';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!CountryForm.init) {
            CountryForm.init = true;

            var w0 = StringEditor;
            var w1 = IntegerEditor;
            var w2 = DateEditor;

            initFormType(CountryForm, [
                'Name', w0,
                'Code', w0,
                'IsoCode3', w0,
                'PhoneCode', w0,
                'CountryNr', w0,
                'InsertUserId', w1,
                'InsertDate', w2,
                'UpdateUserId', w1,
                'UpdateDate', w2
            ]);
        }
    }
}