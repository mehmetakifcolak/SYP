import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { WarehousesColumns, WarehousesRow, WarehousesService } from '../../ServerTypes/Warehouse';
import { WarehousesDialog } from './WarehousesDialog';

@Decorators.registerClass('SYP.Warehouse.WarehousesGrid')
export class WarehousesGrid extends EntityGrid<WarehousesRow, any> {
    protected getColumnsKey() { return WarehousesColumns.columnsKey; }
    protected getDialogType() { return WarehousesDialog; }
    protected getRowDefinition() { return WarehousesRow; }
    protected getService() { return WarehousesService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}
