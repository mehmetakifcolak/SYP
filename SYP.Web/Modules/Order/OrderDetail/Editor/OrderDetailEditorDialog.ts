import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDetailEditorForm, OrderDetailRow } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderDetailEditorDialog')
export class OrderDetailEditorDialog extends EditorDialogBase<OrderDetailRow> {
    protected getFormKey() { return OrderDetailEditorForm.formKey; }
    protected getRowDefinition() { return OrderDetailRow; }

    protected form = new OrderDetailEditorForm(this.idPrefix);
}