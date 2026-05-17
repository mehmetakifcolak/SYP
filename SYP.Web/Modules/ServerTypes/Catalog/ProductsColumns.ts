import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { InlineImageFormatter } from "../../_Ext/Formatters/InlineImageFormatter";
import { ProductsRow } from "./ProductsRow";

export interface ProductsColumns {
    Id: Column<ProductsRow>;
    ProductImage: Column<ProductsRow>;
    Code: Column<ProductsRow>;
    Name: Column<ProductsRow>;
    CategoryName: Column<ProductsRow>;
    BrandName: Column<ProductsRow>;
    UnitName: Column<ProductsRow>;
    CurrentValidPrice: Column<ProductsRow>;
    CurrencyCode: Column<ProductsRow>;
    VatRateName: Column<ProductsRow>;
    Barcode: Column<ProductsRow>;
    IsActive: Column<ProductsRow>;
}

export class ProductsColumns extends ColumnsBase<ProductsRow> {
    static readonly columnsKey = 'Catalog.Products';
    static readonly Fields = fieldsProxy<ProductsColumns>();
}

[InlineImageFormatter]; // referenced types