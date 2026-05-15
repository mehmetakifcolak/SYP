import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { VendorTypeRow } from "./VendorTypeRow";

export interface VendorTypeEditorColumns {
    Id: Column<VendorTypeRow>;
    Title: Column<VendorTypeRow>;
    DiscountType: Column<VendorTypeRow>;
    DiscountValue: Column<VendorTypeRow>;
    Description: Column<VendorTypeRow>;
    IsActive: Column<VendorTypeRow>;
}

export class VendorTypeEditorColumns extends ColumnsBase<VendorTypeRow> {
    static readonly columnsKey = 'Setting.VendorTypeEditor';
    static readonly Fields = fieldsProxy<VendorTypeEditorColumns>();
}