import { BooleanEditor, EmailAddressEditor, initFormType, IntegerEditor, LookupEditor, PasswordEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";

export interface CustomersForm {
    Code: StringEditor;
    VendorTypeId: LookupEditor;
    Name: StringEditor;
    IsActive: BooleanEditor;
    FirstName: StringEditor;
    LastName: StringEditor;
    Phone: StringEditor;
    Phone2: StringEditor;
    Email: EmailAddressEditor;
    Address: TextAreaEditor;
    CountryId: IntegerEditor;
    City: StringEditor;
    District: StringEditor;
    TaxOffice: StringEditor;
    TaxNumber: StringEditor;
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
            var w1 = LookupEditor;
            var w2 = BooleanEditor;
            var w3 = EmailAddressEditor;
            var w4 = TextAreaEditor;
            var w5 = IntegerEditor;
            var w6 = PasswordEditor;

            initFormType(CustomersForm, [
                'Code', w0,
                'VendorTypeId', w1,
                'Name', w0,
                'IsActive', w2,
                'FirstName', w0,
                'LastName', w0,
                'Phone', w0,
                'Phone2', w0,
                'Email', w3,
                'Address', w4,
                'CountryId', w5,
                'City', w0,
                'District', w0,
                'TaxOffice', w0,
                'TaxNumber', w0,
                'UserId', w5,
                'Username', w0,
                'Password', w6,
                'PasswordConfirm', w6
            ]);
        }
    }
}