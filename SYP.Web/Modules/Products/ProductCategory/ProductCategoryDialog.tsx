import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductCategoryForm, ProductCategoryRow, ProductCategoryService } from '../../ServerTypes/Products';

@Decorators.registerClass('SYP.Products.ProductCategoryDialog')
export class ProductCategoryDialog extends DialogBase<ProductCategoryRow, any> {
    protected getFormKey() { return ProductCategoryForm.formKey; }
    protected getRowDefinition() { return ProductCategoryRow; }
    protected getService() { return ProductCategoryService.baseUrl; }

    protected form = new ProductCategoryForm(this.idPrefix);
}