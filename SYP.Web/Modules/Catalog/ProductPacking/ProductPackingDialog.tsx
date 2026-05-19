import { Decorators, EntityDialog } from '@serenity-is/corelib';
import { ProductPackingForm, ProductPackingRow, ProductPackingService } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.ProductPackingDialog')
export class ProductPackingDialog extends EntityDialog<ProductPackingRow, any> {
    protected getFormKey() { return ProductPackingForm.formKey; }
    protected getRowDefinition() { return ProductPackingRow; }
    protected getService() { return ProductPackingService.baseUrl; }

    protected form = new ProductPackingForm(this.idPrefix);
}
