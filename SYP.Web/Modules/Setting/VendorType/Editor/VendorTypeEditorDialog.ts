import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { VendorTypeEditorForm, VendorTypeRow } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.VendorTypeEditorDialog')
export class VendorTypeEditorDialog extends EditorDialogBase<VendorTypeRow> {
    protected getFormKey() { return VendorTypeEditorForm.formKey; }
    protected getRowDefinition() { return VendorTypeRow; }

    protected form = new VendorTypeEditorForm(this.idPrefix);
}