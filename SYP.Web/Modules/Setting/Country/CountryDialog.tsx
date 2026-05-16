import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { CountryForm, CountryRow, CountryService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.CountryDialog')
export class CountryDialog extends DialogBase<CountryRow, any> {
    protected getFormKey() { return CountryForm.formKey; }
    protected getRowDefinition() { return CountryRow; }
    protected getService() { return CountryService.baseUrl; }

    protected form = new CountryForm(this.idPrefix);
}