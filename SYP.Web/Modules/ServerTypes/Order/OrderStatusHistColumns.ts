import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrderStatusHistRow } from "./OrderStatusHistRow";

export interface OrderStatusHistColumns {
    Id: Column<OrderStatusHistRow>;
    OrderNumber: Column<OrderStatusHistRow>;
    OldStatus: Column<OrderStatusHistRow>;
    NewStatus: Column<OrderStatusHistRow>;
    ChangedByUserUsername: Column<OrderStatusHistRow>;
    ChangedByUserRole: Column<OrderStatusHistRow>;
    ChangeReason: Column<OrderStatusHistRow>;
    ChangeDate: Column<OrderStatusHistRow>;
}

export class OrderStatusHistColumns extends ColumnsBase<OrderStatusHistRow> {
    static readonly columnsKey = 'Order.OrderStatusHist';
    static readonly Fields = fieldsProxy<OrderStatusHistColumns>();
}