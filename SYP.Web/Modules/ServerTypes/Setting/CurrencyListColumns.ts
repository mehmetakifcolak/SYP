import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { CurrencyListRow } from "./CurrencyListRow";

export interface CurrencyListColumns {
    Id: Column<CurrencyListRow>;
    Code: Column<CurrencyListRow>;
    Name: Column<CurrencyListRow>;
    CodeType: Column<CurrencyListRow>;
    Symbol: Column<CurrencyListRow>;
    IsActive: Column<CurrencyListRow>;
    InsertUserId: Column<CurrencyListRow>;
    InsertDate: Column<CurrencyListRow>;
    UpdateUserId: Column<CurrencyListRow>;
    UpdateDate: Column<CurrencyListRow>;
    IsDefaultCurrency: Column<CurrencyListRow>;
    DefaultExchangeType: Column<CurrencyListRow>;
    TenantId: Column<CurrencyListRow>;
}

export class CurrencyListColumns extends ColumnsBase<CurrencyListRow> {
    static readonly columnsKey = 'Setting.CurrencyList';
    static readonly Fields = fieldsProxy<CurrencyListColumns>();
}