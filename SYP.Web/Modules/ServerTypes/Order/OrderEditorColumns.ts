import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrderRow } from "./OrderRow";
import { OrderStatus } from "./OrderStatus";

export interface OrderEditorColumns {
    Id: Column<OrderRow>;
    OrderNumber: Column<OrderRow>;
    CustomerCode: Column<OrderRow>;
    Status: Column<OrderRow>;
    OrderDate: Column<OrderRow>;
    TotalAmount: Column<OrderRow>;
    DiscountPercentage: Column<OrderRow>;
    DiscountAmount: Column<OrderRow>;
    NetAmount: Column<OrderRow>;
    CurrencyCode: Column<OrderRow>;
    Notes: Column<OrderRow>;
    RejectReason: Column<OrderRow>;
    InsertDate: Column<OrderRow>;
    InsertUserId: Column<OrderRow>;
    UpdateDate: Column<OrderRow>;
    UpdateUserId: Column<OrderRow>;
}

export class OrderEditorColumns extends ColumnsBase<OrderRow> {
    static readonly columnsKey = 'Order.OrderEditor';
    static readonly Fields = fieldsProxy<OrderEditorColumns>();
}

[OrderStatus]; // referenced types