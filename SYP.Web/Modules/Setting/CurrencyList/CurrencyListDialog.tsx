import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { CurrencyListForm, CurrencyListRow, CurrencyListService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.CurrencyListDialog')
export class CurrencyListDialog extends EntityDialog<CurrencyListRow, any> {
    protected getFormKey() { return CurrencyListForm.formKey; }
    protected getRowDefinition() { return CurrencyListRow; }
    protected getService() { return CurrencyListService.baseUrl; }

    protected form = new CurrencyListForm(this.idPrefix);
}