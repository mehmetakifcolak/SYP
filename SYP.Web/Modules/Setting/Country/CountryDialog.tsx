import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { CountryForm, CountryRow, CountryService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.CountryDialog')
export class CountryDialog extends EntityDialog<CountryRow, any> {
    protected getFormKey() { return CountryForm.formKey; }
    protected getRowDefinition() { return CountryRow; }
    protected getService() { return CountryService.baseUrl; }

    protected form = new CountryForm(this.idPrefix);
}