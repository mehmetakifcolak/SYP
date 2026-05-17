import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderStatusHistEditorColumns, OrderStatusHistRow } from '../../ServerTypes/Order';
import { OrderStatusHistEditorDialog } from './OrderStatusHistEditorDialog';

@Decorators.registerEditor('SYP.Order.OrderStatusHistGridEditor')
export class OrderStatusHistGridEditor extends GridEditorBase<OrderStatusHistRow> {
    protected getColumnsKey() { return OrderStatusHistEditorColumns.columnsKey; }
    protected getDialogType() { return OrderStatusHistEditorDialog; }
    protected getRowDefinition() { return OrderStatusHistRow; }

    constructor(props: any) {
        super(props);
    }
}