import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrderRow } from "./OrderRow";
import { OrderStatus } from "./OrderStatus";

export interface OrderColumns {
    OrderNumber: Column<OrderRow>;
    CustomerName: Column<OrderRow>;
    CustomerCode: Column<OrderRow>;
    ManagerName: Column<OrderRow>;
    OrderDate: Column<OrderRow>;
    Status: Column<OrderRow>;
    TotalAmount: Column<OrderRow>;
    DiscountPercentage: Column<OrderRow>;
    DiscountAmount: Column<OrderRow>;
    NetAmount: Column<OrderRow>;
    CurrencyCode: Column<OrderRow>;
}

export class OrderColumns extends ColumnsBase<OrderRow> {
    static readonly columnsKey = 'Order.Order';
    static readonly Fields = fieldsProxy<OrderColumns>();
}

[OrderStatus]; // referenced types