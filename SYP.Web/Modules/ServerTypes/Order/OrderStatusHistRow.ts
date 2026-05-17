import { fieldsProxy } from "@serenity-is/corelib";

export interface OrderStatusHistRow {
    Id?: number;
    OrderId?: number;
    OldStatus?: number;
    NewStatus?: number;
    ChangedByUserId?: number;
    ChangedByUserRole?: string;
    ChangeReason?: string;
    ChangeDate?: string;
    OrderNumber?: string;
    ChangedByUserUsername?: string;
}

export abstract class OrderStatusHistRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'ChangedByUserRole';
    static readonly localTextPrefix = 'Order.OrderStatusHist';
    static readonly deletePermission = 'Order:OrderStatusHist:Delete';
    static readonly insertPermission = 'Order:OrderStatusHist:Insert';
    static readonly readPermission = 'Order:OrderStatusHist:Read';
    static readonly updatePermission = 'Order:OrderStatusHist:Update';

    static readonly Fields = fieldsProxy<OrderStatusHistRow>();
}