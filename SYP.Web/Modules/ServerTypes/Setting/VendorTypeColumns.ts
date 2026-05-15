import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { VendorTypeRow } from "./VendorTypeRow";

export interface VendorTypeColumns {
    Id: Column<VendorTypeRow>;
    Title: Column<VendorTypeRow>;
    DiscountType: Column<VendorTypeRow>;
    DiscountValue: Column<VendorTypeRow>;
    Description: Column<VendorTypeRow>;
    IsActive: Column<VendorTypeRow>;
}

export class VendorTypeColumns extends ColumnsBase<VendorTypeRow> {
    static readonly columnsKey = 'Setting.VendorType';
    static readonly Fields = fieldsProxy<VendorTypeColumns>();
}