import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { CustomersForm, CustomersRow, CustomersService } from '../../ServerTypes/Customer';

@Decorators.registerClass('SYP.Customer.CustomersDialog')
export class CustomersDialog extends DialogBase<CustomersRow, any> {
    protected getFormKey() { return CustomersForm.formKey; }
    protected getRowDefinition() { return CustomersRow; }
    protected getService() { return CustomersService.baseUrl; }

    protected form = new CustomersForm(this.idPrefix);
}