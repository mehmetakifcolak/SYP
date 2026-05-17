import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { DailyExchangesForm, DailyExchangesRow, DailyExchangesService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.DailyExchangesDialog')
export class DailyExchangesDialog extends EntityDialog<DailyExchangesRow, any> {
    protected getFormKey() { return DailyExchangesForm.formKey; }
    protected getRowDefinition() { return DailyExchangesRow; }
    protected getService() { return DailyExchangesService.baseUrl; }

    protected form = new DailyExchangesForm(this.idPrefix);
}