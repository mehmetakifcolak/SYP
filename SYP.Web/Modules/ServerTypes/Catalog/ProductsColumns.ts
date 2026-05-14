import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductsRow } from "./ProductsRow";

export interface ProductsColumns {
    Id: Column<ProductsRow>;
    Code: Column<ProductsRow>;
    Name: Column<ProductsRow>;
    Name2: Column<ProductsRow>;
    Description: Column<ProductsRow>;
    Barcode: Column<ProductsRow>;
    InsertDate: Column<ProductsRow>;
    InsertUserId: Column<ProductsRow>;
    UpdateDate: Column<ProductsRow>;
    UpdateUserId: Column<ProductsRow>;
    IsActive: Column<ProductsRow>;
}

export class ProductsColumns extends ColumnsBase<ProductsRow> {
    static readonly columnsKey = 'Catalog.Products';
    static readonly Fields = fieldsProxy<ProductsColumns>();
}