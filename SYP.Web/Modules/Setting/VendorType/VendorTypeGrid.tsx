import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { VendorTypeColumns, VendorTypeRow, VendorTypeService } from '../../ServerTypes/Setting';
import { VendorTypeDialog } from './VendorTypeDialog';

@Decorators.registerClass('SYP.Setting.VendorTypeGrid')
export class VendorTypeGrid extends EntityGrid<VendorTypeRow, any> {
    protected getColumnsKey() { return VendorTypeColumns.columnsKey; }
    protected getDialogType() { return VendorTypeDialog; }
    protected getRowDefinition() { return VendorTypeRow; }
    protected getService() { return VendorTypeService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}