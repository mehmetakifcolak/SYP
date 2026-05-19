import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductPackingRow } from "./ProductPackingRow";

export interface ProductPackingEditorColumns {
    Id: Column<ProductPackingRow>;
    Code: Column<ProductPackingRow>;
    Name: Column<ProductPackingRow>;
    Quantity: Column<ProductPackingRow>;
    IsActive: Column<ProductPackingRow>;
}

export class ProductPackingEditorColumns extends ColumnsBase<ProductPackingRow> {
    static readonly columnsKey = 'Catalog.ProductPackingEditor';
    static readonly Fields = fieldsProxy<ProductPackingEditorColumns>();
}
