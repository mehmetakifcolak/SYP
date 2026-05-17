import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { TieredDiscountSettingsForm, TieredDiscountSettingsRow, TieredDiscountSettingsService } from '../../ServerTypes/Order';

@Decorators.registerClass('SYP.Order.TieredDiscountSettingsDialog')
export class TieredDiscountSettingsDialog extends EntityDialog<TieredDiscountSettingsRow, any> {
    protected getFormKey() { return TieredDiscountSettingsForm.formKey; }
    protected getRowDefinition() { return TieredDiscountSettingsRow; }
    protected getService() { return TieredDiscountSettingsService.baseUrl; }

    protected form = new TieredDiscountSettingsForm(this.idPrefix);
}