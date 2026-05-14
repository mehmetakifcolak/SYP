import { fieldsProxy } from "@serenity-is/corelib";

export interface VatRatesRow {
    Id?: number;
    Name?: string;
    Code?: string;
    IsDefault?: boolean;
    IsActive?: boolean;
    SortOrder?: number;
}

export abstract class VatRatesRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Setting.VatRates';
    static readonly deletePermission = 'Setting:VatRates:Delete';
    static readonly insertPermission = 'Setting:VatRates:Insert';
    static readonly readPermission = 'Setting:VatRates:Read';
    static readonly updatePermission = 'Setting:VatRates:Update';

    static readonly Fields = fieldsProxy<VatRatesRow>();
}