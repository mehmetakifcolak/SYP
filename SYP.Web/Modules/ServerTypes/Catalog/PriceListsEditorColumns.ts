import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { PriceListsRow } from "./PriceListsRow";

export interface PriceListsEditorColumns {
    Id: Column<PriceListsRow>;
    Code: Column<PriceListsRow>;
    Name: Column<PriceListsRow>;
    Description: Column<PriceListsRow>;
    CurrencyCode: Column<PriceListsRow>;
    ValidFrom: Column<PriceListsRow>;
    ValidTo: Column<PriceListsRow>;
    IsActive: Column<PriceListsRow>;
    IsDefault: Column<PriceListsRow>;
    InsertDate: Column<PriceListsRow>;
    InsertUserId: Column<PriceListsRow>;
    UpdateDate: Column<PriceListsRow>;
    UpdateUserId: Column<PriceListsRow>;
}

export class PriceListsEditorColumns extends ColumnsBase<PriceListsRow> {
    static readonly columnsKey = 'Catalog.PriceListsEditor';
    static readonly Fields = fieldsProxy<PriceListsEditorColumns>();
}