import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderEditorForm, OrderRow } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderEditorDialog')
export class OrderEditorDialog extends EditorDialogBase<OrderRow> {
    protected getFormKey() { return OrderEditorForm.formKey; }
    protected getRowDefinition() { return OrderRow; }

    protected form = new OrderEditorForm(this.idPrefix);
}