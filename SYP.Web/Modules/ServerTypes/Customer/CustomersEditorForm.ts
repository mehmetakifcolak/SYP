import { BooleanEditor, DateEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface CustomersEditorForm {
    Code: StringEditor;
    Name: StringEditor;
    Email: StringEditor;
    Phone: StringEditor;
    Address: StringEditor;
    IsActive: BooleanEditor;
    InsertDate: DateEditor;
    InsertUserId: IntegerEditor;
    UpdateDate: DateEditor;
    UpdateUserId: IntegerEditor;
}

export class CustomersEditorForm extends PrefixedContext {
    static readonly formKey = 'Customer.CustomersEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!CustomersEditorForm.init) {
            CustomersEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = BooleanEditor;
            var w2 = DateEditor;
            var w3 = IntegerEditor;

            initFormType(CustomersEditorForm, [
                'Code', w0,
                'Name', w0,
                'Email', w0,
                'Phone', w0,
                'Address', w0,
                'IsActive', w1,
                'InsertDate', w2,
                'InsertUserId', w3,
                'UpdateDate', w2,
                'UpdateUserId', w3
            ]);
        }
    }
}