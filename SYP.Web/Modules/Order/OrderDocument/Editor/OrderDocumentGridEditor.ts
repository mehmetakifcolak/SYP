import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDocumentEditorColumns, OrderDocumentRow } from '../../ServerTypes/Order';
import { OrderDocumentEditorDialog } from './OrderDocumentEditorDialog';

@Decorators.registerEditor('SYP.Order.OrderDocumentGridEditor')
export class OrderDocumentGridEditor extends GridEditorBase<OrderDocumentRow> {
    protected getColumnsKey() { return OrderDocumentEditorColumns.columnsKey; }
    protected getDialogType() { return OrderDocumentEditorDialog; }
    protected getRowDefinition() { return OrderDocumentRow; }

    constructor(props: any) {
        super(props);
    }
}