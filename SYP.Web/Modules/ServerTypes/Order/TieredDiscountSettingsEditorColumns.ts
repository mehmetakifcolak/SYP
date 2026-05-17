import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { TieredDiscountSettingsRow } from "./TieredDiscountSettingsRow";

export interface TieredDiscountSettingsEditorColumns {
    Id: Column<TieredDiscountSettingsRow>;
    MinAmount: Column<TieredDiscountSettingsRow>;
    DiscountPercentage: Column<TieredDiscountSettingsRow>;
    IsActive: Column<TieredDiscountSettingsRow>;
    DisplayOrder: Column<TieredDiscountSettingsRow>;
    InsertDate: Column<TieredDiscountSettingsRow>;
    InsertUserId: Column<TieredDiscountSettingsRow>;
    UpdateDate: Column<TieredDiscountSettingsRow>;
    UpdateUserId: Column<TieredDiscountSettingsRow>;
}

export class TieredDiscountSettingsEditorColumns extends ColumnsBase<TieredDiscountSettingsRow> {
    static readonly columnsKey = 'Order.TieredDiscountSettingsEditor';
    static readonly Fields = fieldsProxy<TieredDiscountSettingsEditorColumns>();
}