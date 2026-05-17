import { BooleanEditor, DateEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface PriceListsEditorForm {
    Code: StringEditor;
    Name: StringEditor;
    Description: StringEditor;
    CurrencyId: LookupEditor;
    ValidFrom: DateEditor;
    ValidTo: DateEditor;
    IsActive: IntegerEditor;
    IsDefault: BooleanEditor;
    InsertDate: DateEditor;
    InsertUserId: IntegerEditor;
    UpdateDate: DateEditor;
    UpdateUserId: IntegerEditor;
}

export class PriceListsEditorForm extends PrefixedContext {
    static readonly formKey = 'Catalog.PriceListsEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!PriceListsEditorForm.init) {
            PriceListsEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = LookupEditor;
            var w2 = DateEditor;
            var w3 = IntegerEditor;
            var w4 = BooleanEditor;

            initFormType(PriceListsEditorForm, [
                'Code', w0,
                'Name', w0,
                'Description', w0,
                'CurrencyId', w1,
                'ValidFrom', w2,
                'ValidTo', w2,
                'IsActive', w3,
                'IsDefault', w4,
                'InsertDate', w2,
                'InsertUserId', w3,
                'UpdateDate', w2,
                'UpdateUserId', w3
            ]);
        }
    }
}