import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { UserStatusFormatter } from "../../Customer/Customers/UserStatusFormatter";
import { CustomersRow } from "./CustomersRow";

export interface CustomersColumns {
    Id: Column<CustomersRow>;
    Code: Column<CustomersRow>;
    Name: Column<CustomersRow>;
    FirstName: Column<CustomersRow>;
    LastName: Column<CustomersRow>;
    Email: Column<CustomersRow>;
    Phone: Column<CustomersRow>;
    Phone2: Column<CustomersRow>;
    CountryName: Column<CustomersRow>;
    City: Column<CustomersRow>;
    District: Column<CustomersRow>;
    TaxOffice: Column<CustomersRow>;
    TaxNumber: Column<CustomersRow>;
    VendorTypeTitle: Column<CustomersRow>;
    IsActive: Column<CustomersRow>;
    UserIsActive: Column<CustomersRow>;
}

export class CustomersColumns extends ColumnsBase<CustomersRow> {
    static readonly columnsKey = 'Customer.Customers';
    static readonly Fields = fieldsProxy<CustomersColumns>();
}

[UserStatusFormatter]; // referenced types