import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { BankAccountInformationsRow } from "./BankAccountInformationsRow";

export interface BankAccountInformationsColumns {
    Id: Column<BankAccountInformationsRow>;
    Firm: Column<BankAccountInformationsRow>;
    Bank: Column<BankAccountInformationsRow>;
    Branch: Column<BankAccountInformationsRow>;
    BranchCode: Column<BankAccountInformationsRow>;
    AccountNo: Column<BankAccountInformationsRow>;
    Iban: Column<BankAccountInformationsRow>;
    Swift: Column<BankAccountInformationsRow>;
    CurrencyCode: Column<BankAccountInformationsRow>;
    Origin: Column<BankAccountInformationsRow>;
    Payment: Column<BankAccountInformationsRow>;
    Shipment: Column<BankAccountInformationsRow>;
    TenantId: Column<BankAccountInformationsRow>;
}

export class BankAccountInformationsColumns extends ColumnsBase<BankAccountInformationsRow> {
    static readonly columnsKey = 'Setting.BankAccountInformations';
    static readonly Fields = fieldsProxy<BankAccountInformationsColumns>();
}