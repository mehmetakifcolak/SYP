import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { CurrencyListColumns, CurrencyListRow, CurrencyListService } from '../../ServerTypes/Setting';
import { CurrencyListDialog } from './CurrencyListDialog';

@Decorators.registerClass('SYP.Setting.CurrencyListGrid')
export class CurrencyListGrid extends EntityGrid<CurrencyListRow, any> {
    protected getColumnsKey() { return CurrencyListColumns.columnsKey; }
    protected getDialogType() { return CurrencyListDialog; }
    protected getRowDefinition() { return CurrencyListRow; }
    protected getService() { return CurrencyListService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}