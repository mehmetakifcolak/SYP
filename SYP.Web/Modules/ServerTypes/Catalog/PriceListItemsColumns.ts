import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { PriceListItemsRow } from "./PriceListItemsRow";

export interface PriceListItemsColumns {
    ProductCode: Column<PriceListItemsRow>;
    ProductName: Column<PriceListItemsRow>;
    UnitPrice: Column<PriceListItemsRow>;
    DiscountRate: Column<PriceListItemsRow>;
    Notes: Column<PriceListItemsRow>;
}

export class PriceListItemsColumns extends ColumnsBase<PriceListItemsRow> {
    static readonly columnsKey = 'Catalog.PriceListItems';
    static readonly Fields = fieldsProxy<PriceListItemsColumns>();
}