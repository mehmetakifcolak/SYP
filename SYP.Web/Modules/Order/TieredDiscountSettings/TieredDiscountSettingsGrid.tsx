import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { TieredDiscountSettingsColumns, TieredDiscountSettingsRow, TieredDiscountSettingsService } from '../../ServerTypes/Order';
import { TieredDiscountSettingsDialog } from './TieredDiscountSettingsDialog';

@Decorators.registerClass('SYP.Order.TieredDiscountSettingsGrid')
export class TieredDiscountSettingsGrid extends EntityGrid<TieredDiscountSettingsRow, any> {
    protected getColumnsKey() { return TieredDiscountSettingsColumns.columnsKey; }
    protected getDialogType() { return TieredDiscountSettingsDialog; }
    protected getRowDefinition() { return TieredDiscountSettingsRow; }
    protected getService() { return TieredDiscountSettingsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}