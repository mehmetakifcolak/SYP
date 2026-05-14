import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { BankAccountInformationsForm, BankAccountInformationsRow, BankAccountInformationsService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.BankAccountInformationsDialog')
export class BankAccountInformationsDialog extends DialogBase<BankAccountInformationsRow, any> {
    protected getFormKey() { return BankAccountInformationsForm.formKey; }
    protected getRowDefinition() { return BankAccountInformationsRow; }
    protected getService() { return BankAccountInformationsService.baseUrl; }

    protected form = new BankAccountInformationsForm(this.idPrefix);
}