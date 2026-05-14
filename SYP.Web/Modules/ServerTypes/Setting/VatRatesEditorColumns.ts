import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { VatRatesRow } from "./VatRatesRow";

export interface VatRatesEditorColumns {
    Id: Column<VatRatesRow>;
    Name: Column<VatRatesRow>;
    Code: Column<VatRatesRow>;
    IsDefault: Column<VatRatesRow>;
    IsActive: Column<VatRatesRow>;
    SortOrder: Column<VatRatesRow>;
}

export class VatRatesEditorColumns extends ColumnsBase<VatRatesRow> {
    static readonly columnsKey = 'Setting.VatRatesEditor';
    static readonly Fields = fieldsProxy<VatRatesEditorColumns>();
}