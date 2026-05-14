import { fieldsProxy } from "@serenity-is/corelib";

export interface DailyExchangesRow {
    Id?: number;
    CurrencyId?: number;
    CurrencyCode?: string;
    ForexBuying?: number;
    ForexSelling?: number;
    BanknoteBuying?: number;
    BanknoteSelling?: number;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateUserId?: number;
    UpdateDate?: string;
    IsActive?: boolean;
    DefaultExchangeTypeId?: number;
}

export abstract class DailyExchangesRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'CurrencyCode';
    static readonly localTextPrefix = 'Setting.DailyExchanges';
    static readonly deletePermission = 'Setting:DailyExchanges:Delete';
    static readonly insertPermission = 'Setting:DailyExchanges:Insert';
    static readonly readPermission = 'Setting:DailyExchanges:Read';
    static readonly updatePermission = 'Setting:DailyExchanges:Update';

    static readonly Fields = fieldsProxy<DailyExchangesRow>();
}