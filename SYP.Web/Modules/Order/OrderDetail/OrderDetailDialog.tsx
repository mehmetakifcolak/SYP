import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDetailForm, OrderDetailRow, OrderDetailService } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderDetailDialog')
export class OrderDetailDialog extends DialogBase<OrderDetailRow, any> {
    protected getFormKey() { return OrderDetailForm.formKey; }
    protected getRowDefinition() { return OrderDetailRow; }
    protected getService() { return OrderDetailService.baseUrl; }

    protected form = new OrderDetailForm(this.idPrefix);
}