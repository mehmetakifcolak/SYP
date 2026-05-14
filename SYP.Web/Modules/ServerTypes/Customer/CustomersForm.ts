import { BooleanEditor, initFormType, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface CustomersForm {
    Code: StringEditor;
    Name: StringEditor;
    Email: StringEditor;
    Phone: StringEditor;
    Address: StringEditor;
    IsActive: BooleanEditor;
}

export class CustomersForm extends PrefixedContext {
    static readonly formKey = 'Customer.Customers';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!CustomersForm.init) {
            CustomersForm.init = true;

            var w0 = StringEditor;
            var w1 = BooleanEditor;

            initFormType(CustomersForm, [
                'Code', w0,
                'Name', w0,
                'Email', w0,
                'Phone', w0,
                'Address', w0,
                'IsActive', w1
            ]);
        }
    }
}