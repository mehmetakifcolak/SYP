import { BooleanEditor, EmailAddressEditor, initFormType, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";

export interface WarehousesForm {
    Code: StringEditor;
    Name: StringEditor;
    Address: TextAreaEditor;
    City: StringEditor;
    Phone: StringEditor;
    Email: EmailAddressEditor;
    ManagerName: StringEditor;
    Description: TextAreaEditor;
    IsActive: BooleanEditor;
    IsDefault: BooleanEditor;
}

export class WarehousesForm extends PrefixedContext {
    static readonly formKey = 'Warehouse.Warehouses';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!WarehousesForm.init) {
            WarehousesForm.init = true;

            var w0 = StringEditor;
            var w1 = TextAreaEditor;
            var w2 = EmailAddressEditor;
            var w3 = BooleanEditor;

            initFormType(WarehousesForm, [
                'Code', w0,
                'Name', w0,
                'Address', w1,
                'City', w0,
                'Phone', w0,
                'Email', w2,
                'ManagerName', w0,
                'Description', w1,
                'IsActive', w3,
                'IsDefault', w3
            ]);
        }
    }
}