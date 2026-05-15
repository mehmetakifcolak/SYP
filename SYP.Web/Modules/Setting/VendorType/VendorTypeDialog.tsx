import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators } from '@serenity-is/corelib';
import { VendorTypeForm, VendorTypeRow, VendorTypeService } from '../../ServerTypes/Setting';

@Decorators.registerClass('SYP.Setting.VendorTypeDialog')
export class VendorTypeDialog extends DialogBase<VendorTypeRow, any> {
    protected getFormKey() { return VendorTypeForm.formKey; }
    protected getRowDefinition() { return VendorTypeRow; }
    protected getService() { return VendorTypeService.baseUrl; }

    protected form = new VendorTypeForm(this.idPrefix);
}