import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { DailyExchangesRow } from "./DailyExchangesRow";

export interface DailyExchangesColumns {
    Id: Column<DailyExchangesRow>;
    CurrencyId: Column<DailyExchangesRow>;
    CurrencyCode: Column<DailyExchangesRow>;
    ForexBuying: Column<DailyExchangesRow>;
    ForexSelling: Column<DailyExchangesRow>;
    BanknoteBuying: Column<DailyExchangesRow>;
    BanknoteSelling: Column<DailyExchangesRow>;
    InsertDate: Column<DailyExchangesRow>;
    InsertUserId: Column<DailyExchangesRow>;
    UpdateUserId: Column<DailyExchangesRow>;
    UpdateDate: Column<DailyExchangesRow>;
    IsActive: Column<DailyExchangesRow>;
    DefaultExchangeTypeId: Column<DailyExchangesRow>;
}

export class DailyExchangesColumns extends ColumnsBase<DailyExchangesRow> {
    static readonly columnsKey = 'Setting.DailyExchanges';
    static readonly Fields = fieldsProxy<DailyExchangesColumns>();
}