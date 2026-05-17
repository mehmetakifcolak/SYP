import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderEditorColumns, OrderRow } from '../../ServerTypes/Order';
import { OrderEditorDialog } from './OrderEditorDialog';

@Decorators.registerEditor('SYP.Order.OrderGridEditor')
export class OrderGridEditor extends GridEditorBase<OrderRow> {
    protected getColumnsKey() { return OrderEditorColumns.columnsKey; }
    protected getDialogType() { return OrderEditorDialog; }
    protected getRowDefinition() { return OrderRow; }

    constructor(props: any) {
        super(props);
    }
}