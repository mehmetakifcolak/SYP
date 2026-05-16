import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductCategoryEditorForm, ProductCategoryRow } from '../../ServerTypes/Products';

@Decorators.registerClass('SYP.Products.ProductCategoryEditorDialog')
export class ProductCategoryEditorDialog extends EditorDialogBase<ProductCategoryRow> {
    protected getFormKey() { return ProductCategoryEditorForm.formKey; }
    protected getRowDefinition() { return ProductCategoryRow; }

    protected form = new ProductCategoryEditorForm(this.idPrefix);
}