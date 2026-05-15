import { Decorators } from '@serenity-is/corelib';
import { EntityDialog } from '@serenity-is/corelib';
import { WarehousesForm, WarehousesRow, WarehousesService } from '../../ServerTypes/Warehouse';

@Decorators.registerClass('SYP.Warehouse.WarehousesDialog')
export class WarehousesDialog extends EntityDialog<WarehousesRow, any> {
    protected getFormKey() { return WarehousesForm.formKey; }
    protected getRowDefinition() { return WarehousesRow; }
    protected getService() { return WarehousesService.baseUrl; }

    protected form = new WarehousesForm(this.idPrefix);

    constructor(opt?: any) {
        super(opt);
    }
}
