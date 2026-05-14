import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { NumberTemplatesForm, NumberTemplatesRow, NumberTemplatesService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.NumberTemplatesDialog')
export class NumberTemplatesDialog extends DialogBase<NumberTemplatesRow, any> {
    protected getFormKey() { return NumberTemplatesForm.formKey; }
    protected getRowDefinition() { return NumberTemplatesRow; }
    protected getService() { return NumberTemplatesService.baseUrl; }

    protected form = new NumberTemplatesForm(this.idPrefix);
}