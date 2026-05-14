import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { VatRatesRow } from "./VatRatesRow";

export interface VatRatesColumns {
    Id: Column<VatRatesRow>;
    Name: Column<VatRatesRow>;
    Code: Column<VatRatesRow>;
    IsDefault: Column<VatRatesRow>;
    IsActive: Column<VatRatesRow>;
    SortOrder: Column<VatRatesRow>;
}

export class VatRatesColumns extends ColumnsBase<VatRatesRow> {
    static readonly columnsKey = 'Setting.VatRates';
    static readonly Fields = fieldsProxy<VatRatesColumns>();
}