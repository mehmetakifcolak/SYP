import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface CurrencyListRow {
    Id?: number;
    Code?: string;
    Name?: string;
    CodeType?: number;
    Symbol?: string;
    IsActive?: boolean;
    InsertUserId?: number;
    InsertDate?: string;
    UpdateUserId?: number;
    UpdateDate?: string;
    IsDefaultCurrency?: boolean;
    DefaultExchangeType?: number;
    TenantId?: number;
}

export abstract class CurrencyListRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Code';
    static readonly localTextPrefix = 'Setting.CurrencyList';
    static readonly lookupKey = 'Setting.CurrencyList';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<CurrencyListRow>('Setting.CurrencyList') }
    static async getLookupAsync() { return getLookupAsync<CurrencyListRow>('Setting.CurrencyList') }

    static readonly deletePermission = 'Setting:CurrencyList:Delete';
    static readonly insertPermission = 'Setting:CurrencyList:Insert';
    static readonly readPermission = 'Setting:CurrencyList:Read';
    static readonly updatePermission = 'Setting:CurrencyList:Update';

    static readonly Fields = fieldsProxy<CurrencyListRow>();
}