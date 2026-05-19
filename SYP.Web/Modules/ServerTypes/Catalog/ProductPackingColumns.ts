import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductPackingRow } from "./ProductPackingRow";

export interface ProductPackingColumns {
    Id: Column<ProductPackingRow>;
    Code: Column<ProductPackingRow>;
    Name: Column<ProductPackingRow>;
    Quantity: Column<ProductPackingRow>;
    IsActive: Column<ProductPackingRow>;
    InsertDate: Column<ProductPackingRow>;
}

export class ProductPackingColumns extends ColumnsBase<ProductPackingRow> {
    static readonly columnsKey = 'Catalog.ProductPacking';
    static readonly Fields = fieldsProxy<ProductPackingColumns>();
}
