import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { TieredDiscountSettingsRow } from "./TieredDiscountSettingsRow";

export interface TieredDiscountSettingsColumns {
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

export class TieredDiscountSettingsColumns extends ColumnsBase<TieredDiscountSettingsRow> {
    static readonly columnsKey = 'Order.TieredDiscountSettings';
    static readonly Fields = fieldsProxy<TieredDiscountSettingsColumns>();
}