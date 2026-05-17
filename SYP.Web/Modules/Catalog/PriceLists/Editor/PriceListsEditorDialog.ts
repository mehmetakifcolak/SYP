import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { PriceListsEditorForm, PriceListsRow } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.PriceListsEditorDialog')
export class PriceListsEditorDialog extends EditorDialogBase<PriceListsRow> {
    protected getFormKey() { return PriceListsEditorForm.formKey; }
    protected getRowDefinition() { return PriceListsRow; }

    protected form = new PriceListsEditorForm(this.idPrefix);
}