import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderColumns, OrderRow, OrderService } from '../../ServerTypes/Order';
import { OrderDialog } from './OrderDialog';

@Decorators.registerClass('SYP.Order.OrderGrid')
export class OrderGrid extends GridBase<OrderRow, any> {
    protected getColumnsKey() { return OrderColumns.columnsKey; }
    protected getDialogType() { return OrderDialog; }
    protected getRowDefinition() { return OrderRow; }
    protected getService() { return OrderService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}