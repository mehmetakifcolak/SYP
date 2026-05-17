import { Decorators, EntityDialog} from '@serenity-is/corelib';
import { VendorTypeForm, VendorTypeRow, VendorTypeService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.VendorTypeDialog')
export class VendorTypeDialog extends EntityDialog<VendorTypeRow, any> {
    protected getFormKey() { return VendorTypeForm.formKey; }
    protected getRowDefinition() { return VendorTypeRow; }
    protected getService() { return VendorTypeService.baseUrl; }

    protected form = new VendorTypeForm(this.idPrefix);
}