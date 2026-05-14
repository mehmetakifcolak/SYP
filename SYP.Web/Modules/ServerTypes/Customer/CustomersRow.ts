import { fieldsProxy } from '@serenity-is/corelib';

export interface CustomersRow {
    Id?: number;
    Code?: string;
    Name?: string;
    Email?: string;
    Phone?: string;
    Address?: string;
    IsActive?: boolean;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class CustomersRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Code';
    static readonly localTextPrefix = 'Customer.Customers';

    static readonly deletePermission = 'Customer:Customers';
    static readonly insertPermission = 'Customer:Customers';
    static readonly readPermission = 'Customer:Customers';
    static readonly updatePermission = 'Customer:Customers';

    static readonly Fields = fieldsProxy<CustomersRow>();
}