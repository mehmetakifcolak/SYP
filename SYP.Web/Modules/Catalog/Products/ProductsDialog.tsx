import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductsForm, ProductsRow, ProductsService } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.ProductsDialog')
export class ProductsDialog extends DialogBase<ProductsRow, any> {
    protected getFormKey() { return ProductsForm.formKey; }
    protected getRowDefinition() { return ProductsRow; }
    protected getService() { return ProductsService.baseUrl; }

    protected form = new ProductsForm(this.idPrefix);
}