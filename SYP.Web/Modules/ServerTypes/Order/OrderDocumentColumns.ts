import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { OrderDocumentRow } from "./OrderDocumentRow";

export interface OrderDocumentColumns {
    Id: Column<OrderDocumentRow>;
    OrderNumber: Column<OrderDocumentRow>;
    DocumentType: Column<OrderDocumentRow>;
    FileName: Column<OrderDocumentRow>;
    FilePath: Column<OrderDocumentRow>;
    FileSize: Column<OrderDocumentRow>;
    MimeType: Column<OrderDocumentRow>;
    UploadedByUserUsername: Column<OrderDocumentRow>;
    UploadDate: Column<OrderDocumentRow>;
    IsActive: Column<OrderDocumentRow>;
    Notes: Column<OrderDocumentRow>;
}

export class OrderDocumentColumns extends ColumnsBase<OrderDocumentRow> {
    static readonly columnsKey = 'Order.OrderDocument';
    static readonly Fields = fieldsProxy<OrderDocumentColumns>();
}