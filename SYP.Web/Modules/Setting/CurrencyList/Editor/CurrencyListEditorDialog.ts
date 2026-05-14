import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { CurrencyListEditorForm, CurrencyListRow } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.CurrencyListEditorDialog')
export class CurrencyListEditorDialog extends EditorDialogBase<CurrencyListRow> {
    protected getFormKey() { return CurrencyListEditorForm.formKey; }
    protected getRowDefinition() { return CurrencyListRow; }

    protected form = new CurrencyListEditorForm(this.idPrefix);
}