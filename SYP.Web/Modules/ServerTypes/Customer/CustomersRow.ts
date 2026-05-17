import { fieldsProxy, getLookup, getLookupAsync } from "@serenity-is/corelib";

export interface CustomersRow {
    Id?: number;
    Code?: string;
    Name?: string;
    FirstName?: string;
    LastName?: string;
    Email?: string;
    Phone?: string;
    Phone2?: string;
    Address?: string;
    CountryId?: number;
    City?: string;
    District?: string;
    TaxOffice?: string;
    TaxNumber?: string;
    IsActive?: boolean;
    VendorTypeId?: number;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    UserId?: number;
    ManagerUserId?: number;
    Password?: string;
    PasswordConfirm?: string;
    Username?: string;
    VendorTypeTitle?: string;
    UserIsActive?: number;
    CountryName?: string;
    ManagerName?: string;
}

export abstract class CustomersRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Code';
    static readonly localTextPrefix = 'Customer.Customers';
    static readonly lookupKey = 'Customer.Customers';

    /** @deprecated use getLookupAsync instead */
    static getLookup() { return getLookup<CustomersRow>('Customer.Customers') }
    static async getLookupAsync() { return getLookupAsync<CustomersRow>('Customer.Customers') }

    static readonly deletePermission = 'Customer:Customers:Delete';
    static readonly insertPermission = 'Customer:Customers:Insert';
    static readonly readPermission = 'Customer:Customers:Read';
    static readonly updatePermission = 'Customer:Customers:Update';

    static readonly Fields = fieldsProxy<CustomersRow>();
}