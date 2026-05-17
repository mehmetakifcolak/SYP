import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { PriceListsRow } from "./PriceListsRow";

export interface PriceListsColumns {
    Id: Column<PriceListsRow>;
    Code: Column<PriceListsRow>;
    Name: Column<PriceListsRow>;
    Description: Column<PriceListsRow>;
    CurrencyCode: Column<PriceListsRow>;
    ValidFrom: Column<PriceListsRow>;
    ValidTo: Column<PriceListsRow>;
    IsActive: Column<PriceListsRow>;
    IsDefault: Column<PriceListsRow>;
}

export class PriceListsColumns extends ColumnsBase<PriceListsRow> {
    static readonly columnsKey = 'Catalog.PriceLists';
    static readonly Fields = fieldsProxy<PriceListsColumns>();
}