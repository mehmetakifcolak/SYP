import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductCategoryRow } from "./ProductCategoryRow";

export interface ProductCategoryColumns {
    Name: Column<ProductCategoryRow>;
    SortOrder: Column<ProductCategoryRow>;
    IsActive: Column<ProductCategoryRow>;
}

export class ProductCategoryColumns extends ColumnsBase<ProductCategoryRow> {
    static readonly columnsKey = 'Products.ProductCategory';
    static readonly Fields = fieldsProxy<ProductCategoryColumns>();
}