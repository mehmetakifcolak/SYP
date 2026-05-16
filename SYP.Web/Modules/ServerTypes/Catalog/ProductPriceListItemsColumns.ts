import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { PriceListItemsRow } from "./PriceListItemsRow";

export interface ProductPriceListItemsColumns {
    PriceListCode: Column<PriceListItemsRow>;
    PriceListName: Column<PriceListItemsRow>;
    UnitPrice: Column<PriceListItemsRow>;
    DiscountRate: Column<PriceListItemsRow>;
    Notes: Column<PriceListItemsRow>;
}

export class ProductPriceListItemsColumns extends ColumnsBase<PriceListItemsRow> {
    static readonly columnsKey = 'Catalog.ProductPriceListItems';
    static readonly Fields = fieldsProxy<ProductPriceListItemsColumns>();
}