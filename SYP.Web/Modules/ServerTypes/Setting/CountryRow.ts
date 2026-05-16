import { fieldsProxy } from "@serenity-is/corelib";

export interface CountryRow {
    Id?: number;
    Name?: string;
    Code?: string;
    IsoCode3?: string;
    PhoneCode?: string;
    CountryNr?: string;
    InsertUserId?: number;
    InsertDate?: string;
    UpdateUserId?: number;
    UpdateDate?: string;
}

export abstract class CountryRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Name';
    static readonly localTextPrefix = 'Setting.Country';
    static readonly deletePermission = 'Setting:Country:Delete';
    static readonly insertPermission = 'Setting:Country:Insert';
    static readonly readPermission = 'Setting:Country:Read';
    static readonly updatePermission = 'Setting:Country:Update';

    static readonly Fields = fieldsProxy<CountryRow>();
}