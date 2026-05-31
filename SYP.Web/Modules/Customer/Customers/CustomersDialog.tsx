import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { CustomersForm, CustomersRow, CustomersService } from '../../ServerTypes/Customer';

@Decorators.registerClass('SYP.Customer.CustomersDialog')
@Decorators.maximizable()
export class CustomersDialog extends EntityDialog<CustomersRow, any> {
    protected getFormKey() { return CustomersForm.formKey; }
    protected getRowDefinition() { return CustomersRow; }
    protected getService() { return CustomersService.baseUrl; }

    protected form = new CustomersForm(this.idPrefix);

    protected getDialogOptions() {
        let opt = super.getDialogOptions();
        opt.width = 750;
        return opt;
    }
}
