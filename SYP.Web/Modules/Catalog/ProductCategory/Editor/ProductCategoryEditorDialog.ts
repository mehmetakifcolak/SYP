import { EditorDialogBase } from '@/_Ext/Editors/EditorDialogBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductCategoryRow, ProductCategoryEditorForm } from '../../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.ProductCategoryEditorDialog')
export class ProductCategoryEditorDialog extends EditorDialogBase<ProductCategoryRow> {
    protected getFormKey() { return ProductCategoryEditorForm.formKey; }
    protected getRowDefinition() { return ProductCategoryRow; }

    protected form = new ProductCategoryEditorForm(this.idPrefix);
}