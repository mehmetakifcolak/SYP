import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderStatusHistEditorForm, OrderStatusHistRow } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderStatusHistEditorDialog')
export class OrderStatusHistEditorDialog extends EditorDialogBase<OrderStatusHistRow> {
    protected getFormKey() { return OrderStatusHistEditorForm.formKey; }
    protected getRowDefinition() { return OrderStatusHistRow; }

    protected form = new OrderStatusHistEditorForm(this.idPrefix);
}