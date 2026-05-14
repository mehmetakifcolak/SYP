import { PrefixedContext, IntegerEditor, StringEditor, BooleanEditor, DateEditor, initFormType } from '@serenity-is/corelib';

export interface CustomersForm {
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

export class CustomersForm extends PrefixedContext {
    static readonly formKey = 'Customer.Customers';
    private static init: boolean;
    
    constructor(prefix: string) {
        super(prefix);
        if (!CustomersForm.init)  {
            CustomersForm.init = true;
            
            var w0 = IntegerEditor;
            var w1 = StringEditor;
            var w2 = BooleanEditor;
            var w3 = DateEditor;

            initFormType(CustomersForm, [
            'Code', w1,
            'Name', w1,
            'Email', w1,
            'Phone', w1,
            'Address', w1,
            'IsActive', w2,
            'InsertDate', w3,
            'InsertUserId', w0,
            'UpdateDate', w3,
            'UpdateUserId', w0,
            ]);
        }
    }
}