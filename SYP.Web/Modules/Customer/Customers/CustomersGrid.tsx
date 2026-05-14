import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { CustomersColumns, CustomersRow, CustomersService } from '../../ServerTypes/Customer';
import { CustomersDialog } from './CustomersDialog';

@Decorators.registerClass('SYP.Customer.CustomersGrid')
export class CustomersGrid extends GridBase<CustomersRow, any> {
    protected getColumnsKey() { return CustomersColumns.columnsKey; }
    protected getDialogType() { return CustomersDialog; }
    protected getRowDefinition() { return CustomersRow; }
    protected getService() { return CustomersService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}