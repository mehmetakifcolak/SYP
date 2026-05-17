import { fieldsProxy } from "@serenity-is/corelib";

export interface TieredDiscountSettingsRow {
    Id?: number;
    MinAmount?: number;
    DiscountPercentage?: number;
    IsActive?: boolean;
    DisplayOrder?: number;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
}

export abstract class TieredDiscountSettingsRow {
    static readonly idProperty = 'Id';
    static readonly localTextPrefix = 'Order.TieredDiscountSettings';
    static readonly deletePermission = 'Order:TieredDiscountSettings:Delete';
    static readonly insertPermission = 'Order:TieredDiscountSettings:Insert';
    static readonly readPermission = 'Order:TieredDiscountSettings:Read';
    static readonly updatePermission = 'Order:TieredDiscountSettings:Update';

    static readonly Fields = fieldsProxy<TieredDiscountSettingsRow>();
}