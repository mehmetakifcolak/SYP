import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductsEditorForm, ProductsRow } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.ProductsEditorDialog')
export class ProductsEditorDialog extends EditorDialogBase<ProductsRow> {
    protected getFormKey() { return ProductsEditorForm.formKey; }
    protected getRowDefinition() { return ProductsRow; }

    protected form = new ProductsEditorForm(this.idPrefix);
}