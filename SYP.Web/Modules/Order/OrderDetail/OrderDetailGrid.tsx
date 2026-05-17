import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { OrderDetailColumns, OrderDetailRow, OrderDetailService } from '../../ServerTypes/Order';
import { OrderDetailDialog } from './OrderDetailDialog';

@Decorators.registerClass('SYP.Order.OrderDetailGrid')
export class OrderDetailGrid extends EntityGrid<OrderDetailRow, any> {
    protected getColumnsKey() { return OrderDetailColumns.columnsKey; }
    protected getDialogType() { return OrderDetailDialog; }
    protected getRowDefinition() { return OrderDetailRow; }
    protected getService() { return OrderDetailService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}