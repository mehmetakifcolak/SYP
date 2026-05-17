import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { DailyExchangesEditorForm, DailyExchangesRow } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.DailyExchangesEditorDialog')
export class DailyExchangesEditorDialog extends EditorDialogBase<DailyExchangesRow> {
    protected getFormKey() { return DailyExchangesEditorForm.formKey; }
    protected getRowDefinition() { return DailyExchangesRow; }

    protected form = new DailyExchangesEditorForm(this.idPrefix);
}