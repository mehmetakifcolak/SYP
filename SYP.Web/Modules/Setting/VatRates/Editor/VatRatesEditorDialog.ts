import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { VatRatesEditorForm, VatRatesRow } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.VatRatesEditorDialog')
export class VatRatesEditorDialog extends EditorDialogBase<VatRatesRow> {
    protected getFormKey() { return VatRatesEditorForm.formKey; }
    protected getRowDefinition() { return VatRatesRow; }

    protected form = new VatRatesEditorForm(this.idPrefix);
}