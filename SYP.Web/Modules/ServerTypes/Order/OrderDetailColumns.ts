import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrderDetailRow } from "./OrderDetailRow";

export interface OrderDetailColumns {
    Id: Column<OrderDetailRow>;
    OrderNumber: Column<OrderDetailRow>;
    ProductCodeName: Column<OrderDetailRow>;
    Quantity: Column<OrderDetailRow>;
    UnitCode: Column<OrderDetailRow>;
    UnitPrice: Column<OrderDetailRow>;
    VatRateName: Column<OrderDetailRow>;
    VatRate: Column<OrderDetailRow>;
    Discount: Column<OrderDetailRow>;
    LineTotal: Column<OrderDetailRow>;
    Notes: Column<OrderDetailRow>;
}

export class OrderDetailColumns extends ColumnsBase<OrderDetailRow> {
    static readonly columnsKey = 'Order.OrderDetail';
    static readonly Fields = fieldsProxy<OrderDetailColumns>();
}