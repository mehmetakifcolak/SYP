import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { CountryEditorForm, CountryRow } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.CountryEditorDialog')
export class CountryEditorDialog extends EditorDialogBase<CountryRow> {
    protected getFormKey() { return CountryEditorForm.formKey; }
    protected getRowDefinition() { return CountryRow; }

    protected form = new CountryEditorForm(this.idPrefix);
}