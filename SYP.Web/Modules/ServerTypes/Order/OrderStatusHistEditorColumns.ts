import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrderStatusHistRow } from "./OrderStatusHistRow";

export interface OrderStatusHistEditorColumns {
    Id: Column<OrderStatusHistRow>;
    OrderNumber: Column<OrderStatusHistRow>;
    OldStatus: Column<OrderStatusHistRow>;
    NewStatus: Column<OrderStatusHistRow>;
    ChangedByUserUsername: Column<OrderStatusHistRow>;
    ChangedByUserRole: Column<OrderStatusHistRow>;
    ChangeReason: Column<OrderStatusHistRow>;
    ChangeDate: Column<OrderStatusHistRow>;
}

export class OrderStatusHistEditorColumns extends ColumnsBase<OrderStatusHistRow> {
    static readonly columnsKey = 'Order.OrderStatusHistEditor';
    static readonly Fields = fieldsProxy<OrderStatusHistEditorColumns>();
}