import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { CustomersRow } from "./CustomersRow";

export interface CustomersEditorColumns {
    Id: Column<CustomersRow>;
    Code: Column<CustomersRow>;
    Name: Column<CustomersRow>;
    Email: Column<CustomersRow>;
    Phone: Column<CustomersRow>;
    Address: Column<CustomersRow>;
    IsActive: Column<CustomersRow>;
    InsertDate: Column<CustomersRow>;
    InsertUserId: Column<CustomersRow>;
    UpdateDate: Column<CustomersRow>;
    UpdateUserId: Column<CustomersRow>;
}

export class CustomersEditorColumns extends ColumnsBase<CustomersRow> {
    static readonly columnsKey = 'Customer.CustomersEditor';
    static readonly Fields = fieldsProxy<CustomersEditorColumns>();
}