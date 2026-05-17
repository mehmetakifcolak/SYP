import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { TieredDiscountSettingsColumns, TieredDiscountSettingsRow, TieredDiscountSettingsService } from '../../ServerTypes/Order';
import { TieredDiscountSettingsDialog } from './TieredDiscountSettingsDialog';

@Decorators.registerClass('SYP.Order.TieredDiscountSettingsGrid')
export class TieredDiscountSettingsGrid extends GridBase<TieredDiscountSettingsRow, any> {
    protected getColumnsKey() { return TieredDiscountSettingsColumns.columnsKey; }
    protected getDialogType() { return TieredDiscountSettingsDialog; }
    protected getRowDefinition() { return TieredDiscountSettingsRow; }
    protected getService() { return TieredDiscountSettingsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}