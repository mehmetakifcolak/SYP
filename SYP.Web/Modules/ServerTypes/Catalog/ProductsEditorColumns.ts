import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductsRow } from "./ProductsRow";

export interface ProductsEditorColumns {
    Id: Column<ProductsRow>;
    Code: Column<ProductsRow>;
    Name: Column<ProductsRow>;
    Name2: Column<ProductsRow>;
    Description: Column<ProductsRow>;
    Barcode: Column<ProductsRow>;
    Currency: Column<ProductsRow>;
    VatRate: Column<ProductsRow>;
    InsertDate: Column<ProductsRow>;
    InsertUserId: Column<ProductsRow>;
    UpdateDate: Column<ProductsRow>;
    UpdateUserId: Column<ProductsRow>;
    IsActive: Column<ProductsRow>;
}

export class ProductsEditorColumns extends ColumnsBase<ProductsRow> {
    static readonly columnsKey = 'Catalog.ProductsEditor';
    static readonly Fields = fieldsProxy<ProductsEditorColumns>();
}