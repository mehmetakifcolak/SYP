import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { Column } from '@serenity-is/sleekgrid';
import { CustomersColumns, CustomersRow, CustomersService } from '../../ServerTypes/Customer';
import { CustomersDialog } from './CustomersDialog';
import { UserStatusFormatter } from './UserStatusFormatter';

@Decorators.registerClass('SYP.Customer.CustomersGrid')
export class CustomersGrid extends EntityGrid<CustomersRow, any> {
    protected getColumnsKey() { return CustomersColumns.columnsKey; }
    protected getDialogType() { return CustomersDialog; }
    protected getRowDefinition() { return CustomersRow; }
    protected getService() { return CustomersService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected get_ExtGridOptions(): ExtGridOptions {
        let opt = super.get_ExtGridOptions();
        opt.ShowInlineActionsColumn = true;
        opt.ShowEditInlineButtun = true;
        return opt;
    }

    protected getColumns(): Column[] {
        let columns = super.getColumns();

        console.log('All columns:', columns.map(c => c.field));

        let userIsActiveCol = columns.find(c => c.field === 'UserIsActive');
        console.log('UserIsActive column found:', !!userIsActiveCol);

        if (userIsActiveCol) {
            let formatter = new UserStatusFormatter();
            userIsActiveCol.format = ctx => {
                console.log('Formatter called, value:', ctx.value);
                return formatter.format(ctx);
            };
        }

        return columns;
    }
}