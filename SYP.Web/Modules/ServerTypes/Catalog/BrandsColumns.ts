import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { BrandsRow } from "./BrandsRow";

export interface BrandsColumns {
    Code: Column<BrandsRow>;
    Name: Column<BrandsRow>;
    Logo: Column<BrandsRow>;
    IsActive: Column<BrandsRow>;
    SortOrder: Column<BrandsRow>;
}

export class BrandsColumns extends ColumnsBase<BrandsRow> {
    static readonly columnsKey = 'Catalog.Brands';
    static readonly Fields = fieldsProxy<BrandsColumns>();
}