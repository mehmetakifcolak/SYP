import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { BrandsForm, BrandsRow, BrandsService } from '../../ServerTypes/Products';

@Decorators.registerClass('SYP.Products.BrandsDialog')
export class BrandsDialog extends DialogBase<BrandsRow, any> {
    protected getFormKey() { return BrandsForm.formKey; }
    protected getRowDefinition() { return BrandsRow; }
    protected getService() { return BrandsService.baseUrl; }

    protected form = new BrandsForm(this.idPrefix);
}