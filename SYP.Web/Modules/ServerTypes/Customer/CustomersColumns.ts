import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { UserStatusFormatter } from "../../Customer/Customers/UserStatusFormatter";
import { CustomersRow } from "./CustomersRow";

export interface CustomersColumns {
    Id: Column<CustomersRow>;
    Code: Column<CustomersRow>;
    Name: Column<CustomersRow>;
    Email: Column<CustomersRow>;
    Phone: Column<CustomersRow>;
    VendorTypeTitle: Column<CustomersRow>;
    IsActive: Column<CustomersRow>;
    Username: Column<CustomersRow>;
    UserIsActive: Column<CustomersRow>;
}

export class CustomersColumns extends ColumnsBase<CustomersRow> {
    static readonly columnsKey = 'Customer.Customers';
    static readonly Fields = fieldsProxy<CustomersColumns>();
}

[UserStatusFormatter]; // referenced types