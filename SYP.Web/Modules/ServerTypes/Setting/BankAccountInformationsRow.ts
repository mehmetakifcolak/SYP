import { fieldsProxy } from "@serenity-is/corelib";

export interface BankAccountInformationsRow {
    Id?: number;
    Firm?: string;
    Bank?: string;
    Branch?: string;
    BranchCode?: string;
    AccountNo?: string;
    Iban?: string;
    Swift?: string;
    Currency?: string;
    Origin?: string;
    Payment?: string;
    Shipment?: string;
    TenantId?: number;
    CurrencyCode?: string;
}

export abstract class BankAccountInformationsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Firm';
    static readonly localTextPrefix = 'Setting.BankAccountInformations';
    static readonly deletePermission = 'Setting:BankAccountInformations:Delete';
    static readonly insertPermission = 'Setting:BankAccountInformations:Insert';
    static readonly readPermission = 'Setting:BankAccountInformations:Read';
    static readonly updatePermission = 'Setting:BankAccountInformations:Update';

    static readonly Fields = fieldsProxy<BankAccountInformationsRow>();
}