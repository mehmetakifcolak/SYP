import { fieldsProxy } from "@serenity-is/corelib";

export interface OrderDetailRow {
    Id?: number;
    OrderId?: number;
    ProductId?: number;
    Quantity?: number;
    UnitId?: number;
    UnitPrice?: number;
    VatRateId?: number;
    VatRate?: number;
    Discount?: number;
    LineTotal?: number;
    Notes?: string;
    OrderNumber?: string;
    ProductCodeName?: string;
    UnitCode?: string;
    VatRateName?: string;
}

export abstract class OrderDetailRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'Notes';
    static readonly localTextPrefix = 'Order.OrderDetail';
    static readonly deletePermission = 'Order:OrderDetail:Delete';
    static readonly insertPermission = 'Order:OrderDetail:Insert';
    static readonly readPermission = 'Order:OrderDetail:Read';
    static readonly updatePermission = 'Order:OrderDetail:Update';

    static readonly Fields = fieldsProxy<OrderDetailRow>();
}