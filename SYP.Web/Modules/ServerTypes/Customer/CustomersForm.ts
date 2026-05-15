import { BooleanEditor, EmailAddressEditor, initFormType, IntegerEditor, LookupEditor, PasswordEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface CustomersForm {
    Code: StringEditor;
    Name: StringEditor;
    Email: EmailAddressEditor;
    Phone: StringEditor;
    Address: StringEditor;
    VendorTypeId: LookupEditor;
    IsActive: BooleanEditor;
    UserId: IntegerEditor;
    Username: StringEditor;
    Password: PasswordEditor;
    PasswordConfirm: PasswordEditor;
}

export class CustomersForm extends PrefixedContext {
    static readonly formKey = 'Customer.Customers';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!CustomersForm.init) {
            CustomersForm.init = true;

            var w0 = StringEditor;
            var w1 = EmailAddressEditor;
            var w2 = LookupEditor;
            var w3 = BooleanEditor;
            var w4 = IntegerEditor;
            var w5 = PasswordEditor;

            initFormType(CustomersForm, [
                'Code', w0,
                'Name', w0,
                'Email', w1,
                'Phone', w0,
                'Address', w0,
                'VendorTypeId', w2,
                'IsActive', w3,
                'UserId', w4,
                'Username', w0,
                'Password', w5,
                'PasswordConfirm', w5
            ]);
        }
    }
}