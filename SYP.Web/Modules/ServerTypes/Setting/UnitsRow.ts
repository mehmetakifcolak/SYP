import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface UnitsRow {
    Id?: number;
    Code?: string;
    Name?: string;
    IsActive?: boolean;
    SortOrder?: number;
}

export abstract class UnitsRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Setting.Units';
    static readonly lookupKey = 'Setting.Units';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<UnitsRow>('Setting.Units') }
    static async getLookupAsync() { return getLookupAsync<UnitsRow>('Setting.Units') }

    static readonly deletePermission = 'Setting:Units:Delete';
    static readonly insertPermission = 'Setting:Units:Insert';
    static readonly readPermission = 'Setting:Units:Read';
    static readonly updatePermission = 'Setting:Units:Update';

    static readonly Fields = fieldsProxy<UnitsRow>();
}