import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDocumentEditorForm, OrderDocumentRow } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderDocumentEditorDialog')
export class OrderDocumentEditorDialog extends EditorDialogBase<OrderDocumentRow> {
    protected getFormKey() { return OrderDocumentEditorForm.formKey; }
    protected getRowDefinition() { return OrderDocumentRow; }

    protected form = new OrderDocumentEditorForm(this.idPrefix);
}