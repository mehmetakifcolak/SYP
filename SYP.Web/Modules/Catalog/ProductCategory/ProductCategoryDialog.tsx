import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { ProductCategoryRow, ProductCategoryForm, ProductCategoryService } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.ProductCategoryDialog')
export class ProductCategoryDialog extends EntityDialog<ProductCategoryRow, any> {
    protected getFormKey() { return ProductCategoryForm.formKey; }
    protected getRowDefinition() { return ProductCategoryRow; }
    protected getService() { return ProductCategoryService.baseUrl; }

    protected form = new ProductCategoryForm(this.idPrefix);
}