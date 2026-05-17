import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { TieredDiscountSettingsForm, TieredDiscountSettingsRow, TieredDiscountSettingsService } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.TieredDiscountSettingsDialog')
export class TieredDiscountSettingsDialog extends DialogBase<TieredDiscountSettingsRow, any> {
    protected getFormKey() { return TieredDiscountSettingsForm.formKey; }
    protected getRowDefinition() { return TieredDiscountSettingsRow; }
    protected getService() { return TieredDiscountSettingsService.baseUrl; }

    protected form = new TieredDiscountSettingsForm(this.idPrefix);
}