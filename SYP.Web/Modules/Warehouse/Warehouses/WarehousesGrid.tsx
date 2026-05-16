import { Decorators } from '@serenity-is/corelib';
import { WarehousesColumns, WarehousesRow, WarehousesService } from '../../ServerTypes/Warehouse';
import { WarehousesDialog } from './WarehousesDialog';
import { GridBase } from '../../_Ext/Bases/GridBase';

@Decorators.registerClass('SYP.Warehouse.WarehousesGrid')
export class WarehousesGrid extends GridBase<WarehousesRow, any> {
    protected getColumnsKey() { return WarehousesColumns.columnsKey; }
    protected getDialogType() { return WarehousesDialog; }
    protected getRowDefinition() { return WarehousesRow; }
    protected getService() { return WarehousesService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}
