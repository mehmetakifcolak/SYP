import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { OrderStatusHistForm, OrderStatusHistRow, OrderStatusHistService } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderStatusHistDialog')
export class OrderStatusHistDialog extends EntityDialog<OrderStatusHistRow, any> {
    protected getFormKey() { return OrderStatusHistForm.formKey; }
    protected getRowDefinition() { return OrderStatusHistRow; }
    protected getService() { return OrderStatusHistService.baseUrl; }

    protected form = new OrderStatusHistForm(this.idPrefix);
}