import { EditorDialogBase } from '@/Common/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductPackingEditorForm, ProductPackingRow } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.ProductPackingEditorDialog')
export class ProductPackingEditorDialog extends EditorDialogBase<ProductPackingRow> {
    protected getFormKey() { return ProductPackingEditorForm.formKey; }
    protected getRowDefinition() { return ProductPackingRow; }

    protected form = new ProductPackingEditorForm(this.idPrefix);
}
