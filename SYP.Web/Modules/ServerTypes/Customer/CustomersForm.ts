import { BooleanEditor, EmailAddressEditor, initFormType, LookupEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";
import { UserDialog } from "../../Administration/User/UserDialog";

export interface CustomersForm {
    Code: StringEditor;
    VendorTypeId: LookupEditor;
    CurrencyId: LookupEditor;
    PriceListId: LookupEditor;
    ManagerUserId: LookupEditor;
    Name: StringEditor;
    IsActive: BooleanEditor;
    FirstName: StringEditor;
    LastName: StringEditor;
    Phone: StringEditor;
    Phone2: StringEditor;
    Email: EmailAddressEditor;
    Address: TextAreaEditor;
    CountryId: LookupEditor;
    City: StringEditor;
    District: StringEditor;
    TaxOffice: StringEditor;
    TaxNumber: StringEditor;
    UserId: LookupEditor;
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

            initFormType(CustomersForm, [
                'Code', w0,
                'VendorTypeId', w1,
                'CurrencyId', w1,
                'PriceListId', w1,
                'ManagerUserId', w1,
                'Name', w0,
                'IsActive', w2,
                'FirstName', w0,
                'LastName', w0,
                'Phone', w0,
                'Phone2', w0,
                'Email', w3,
                'Address', w4,
                'CountryId', w1,
                'City', w0,
                'District', w0,
                'TaxOffice', w0,
                'TaxNumber', w0,
                'UserId', w1
            ]);
        }
    }
}

queueMicrotask(() => [UserDialog]); // referenced dialogs