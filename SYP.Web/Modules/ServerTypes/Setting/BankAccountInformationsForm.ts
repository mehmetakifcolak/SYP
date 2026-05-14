import { initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface BankAccountInformationsForm {
    Firm: StringEditor;
    Bank: StringEditor;
    Branch: StringEditor;
    BranchCode: StringEditor;
    AccountNo: StringEditor;
    Iban: StringEditor;
    Swift: StringEditor;
    Currency: StringEditor;
    Origin: StringEditor;
    Payment: StringEditor;
    Shipment: StringEditor;
    TenantId: IntegerEditor;
}

export class BankAccountInformationsForm extends PrefixedContext {
    static readonly formKey = 'Setting.BankAccountInformations';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!BankAccountInformationsForm.init) {
            BankAccountInformationsForm.init = true;

            var w0 = StringEditor;
            var w1 = IntegerEditor;

            initFormType(BankAccountInformationsForm, [
                'Firm', w0,
                'Bank', w0,
                'Branch', w0,
                'BranchCode', w0,
                'AccountNo', w0,
                'Iban', w0,
                'Swift', w0,
                'Currency', w0,
                'Origin', w0,
                'Payment', w0,
                'Shipment', w0,
                'TenantId', w1
            ]);
        }
    }
}