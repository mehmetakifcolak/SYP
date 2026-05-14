import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { VatRatesForm, VatRatesRow, VatRatesService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.VatRatesDialog')
export class VatRatesDialog extends DialogBase<VatRatesRow, any> {
    protected getFormKey() { return VatRatesForm.formKey; }
    protected getRowDefinition() { return VatRatesRow; }
    protected getService() { return VatRatesService.baseUrl; }

    protected form = new VatRatesForm(this.idPrefix);
}