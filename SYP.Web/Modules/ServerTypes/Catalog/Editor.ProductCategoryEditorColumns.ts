import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { ProductCategoryRow } from "./ProductCategoryRow";

export interface ProductCategoryEditorColumns {
    Id: Column<ProductCategoryRow>;
    ParentName: Column<ProductCategoryRow>;
    Name: Column<ProductCategoryRow>;
    FullPath: Column<ProductCategoryRow>;
    SortOrder: Column<ProductCategoryRow>;
    IsActive: Column<ProductCategoryRow>;
    CreatedAt: Column<ProductCategoryRow>;
}

export class ProductCategoryEditorColumns extends ColumnsBase<ProductCategoryRow> {
    static readonly columnsKey = 'Catalog.ProductCategoryEditor';
    static readonly Fields = fieldsProxy<ProductCategoryEditorColumns>();
}