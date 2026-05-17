import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { VatRatesForm, VatRatesRow, VatRatesService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.VatRatesDialog')
export class VatRatesDialog extends EntityDialog<VatRatesRow, any> {
    protected getFormKey() { return VatRatesForm.formKey; }
    protected getRowDefinition() { return VatRatesRow; }
    protected getService() { return VatRatesService.baseUrl; }

    protected form = new VatRatesForm(this.idPrefix);
}