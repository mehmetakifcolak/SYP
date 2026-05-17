import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { OrderStatusHistColumns, OrderStatusHistRow, OrderStatusHistService } from '../../ServerTypes/Order';
import { OrderStatusHistDialog } from './OrderStatusHistDialog';

@Decorators.registerClass('SYP.Order.OrderStatusHistGrid')
export class OrderStatusHistGrid extends EntityGrid<OrderStatusHistRow, any> {
    protected getColumnsKey() { return OrderStatusHistColumns.columnsKey; }
    protected getDialogType() { return OrderStatusHistDialog; }
    protected getRowDefinition() { return OrderStatusHistRow; }
    protected getService() { return OrderStatusHistService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}