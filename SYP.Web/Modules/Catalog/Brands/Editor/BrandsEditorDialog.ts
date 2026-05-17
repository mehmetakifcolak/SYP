import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { BrandsEditorForm, BrandsRow } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.BrandsEditorDialog')
export class BrandsEditorDialog extends EditorDialogBase<BrandsRow> {
    protected getFormKey() { return BrandsEditorForm.formKey; }
    protected getRowDefinition() { return BrandsRow; }

    protected form = new BrandsEditorForm(this.idPrefix);
}