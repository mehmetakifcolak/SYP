import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDetailEditorColumns, OrderDetailRow } from '../../ServerTypes/Order';
import { OrderDetailEditorDialog } from './OrderDetailEditorDialog';

@Decorators.registerEditor('SYP.Order.OrderDetailGridEditor')
export class OrderDetailGridEditor extends GridEditorBase<OrderDetailRow> {
    protected getColumnsKey() { return OrderDetailEditorColumns.columnsKey; }
    protected getDialogType() { return OrderDetailEditorDialog; }
    protected getRowDefinition() { return OrderDetailRow; }

    constructor(props: any) {
        super(props);
    }
}