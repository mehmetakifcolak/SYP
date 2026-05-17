import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderStatusHistForm, OrderStatusHistRow, OrderStatusHistService } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderStatusHistDialog')
export class OrderStatusHistDialog extends DialogBase<OrderStatusHistRow, any> {
    protected getFormKey() { return OrderStatusHistForm.formKey; }
    protected getRowDefinition() { return OrderStatusHistRow; }
    protected getService() { return OrderStatusHistService.baseUrl; }

    protected form = new OrderStatusHistForm(this.idPrefix);
}