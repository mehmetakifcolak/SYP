import { GridEditorBase } from '@/Common/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDocumentEditorDialog } from './OrderDocumentEditorDialog';
import { OrderDocumentEditorColumns, OrderDocumentRow } from '../../../ServerTypes/Order';

@Decorators.registerEditor('SYP.Order.OrderDocumentGridEditor')
export class OrderDocumentGridEditor extends GridEditorBase<OrderDocumentRow> {
    protected getColumnsKey() { return OrderDocumentEditorColumns.columnsKey; }
    protected getDialogType() { return OrderDocumentEditorDialog; }
    protected getRowDefinition() { return OrderDocumentRow; }

    constructor(props: any) {
        super(props);
    }
}