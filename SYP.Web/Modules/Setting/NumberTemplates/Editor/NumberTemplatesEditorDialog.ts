import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { NumberTemplatesEditorForm, NumberTemplatesRow } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.NumberTemplatesEditorDialog')
export class NumberTemplatesEditorDialog extends EditorDialogBase<NumberTemplatesRow> {
    protected getFormKey() { return NumberTemplatesEditorForm.formKey; }
    protected getRowDefinition() { return NumberTemplatesRow; }

    protected form = new NumberTemplatesEditorForm(this.idPrefix);
}