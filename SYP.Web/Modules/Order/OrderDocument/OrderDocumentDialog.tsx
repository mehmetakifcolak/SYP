import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDocumentForm, OrderDocumentRow, OrderDocumentService } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.OrderDocumentDialog')
export class OrderDocumentDialog extends DialogBase<OrderDocumentRow, any> {
    protected getFormKey() { return OrderDocumentForm.formKey; }
    protected getRowDefinition() { return OrderDocumentRow; }
    protected getService() { return OrderDocumentService.baseUrl; }

    protected form = new OrderDocumentForm(this.idPrefix);
}