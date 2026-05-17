import { fieldsProxy } from "@serenity-is/corelib";
import { OrderDetailRow } from "./OrderDetailRow";
import { OrderStatus } from "./OrderStatus";

export interface OrderRow {
    Id?: number;
    OrderNumber?: string;
    CustomerId?: number;
    ManagerUserId?: number;
    Status?: OrderStatus;
    OrderDate?: string;
    TotalAmount?: number;
    DiscountPercentage?: number;
    DiscountAmount?: number;
    NetAmount?: number;
    CurrencyId?: number;
    Notes?: string;
    RejectReason?: string;
    InsertDate?: string;
    InsertUserId?: number;
    UpdateDate?: string;
    UpdateUserId?: number;
    CustomerName?: string;
    CustomerCode?: string;
    ManagerName?: string;
    CurrencyCode?: string;
    DetailList?: OrderDetailRow[];
}

export abstract class OrderRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'OrderNumber';
    static readonly localTextPrefix = 'Order.Order';
    static readonly deletePermission = 'Order:Order:Delete';
    static readonly insertPermission = 'Order:Order:Insert';
    static readonly readPermission = 'Order:Order:Read';
    static readonly updatePermission = 'Order:Order:Update';

    static readonly Fields = fieldsProxy<OrderRow>();
}