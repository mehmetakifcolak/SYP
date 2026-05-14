import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { DailyExchangesColumns, DailyExchangesRow, DailyExchangesService } from '../../ServerTypes/Setting';
import { DailyExchangesDialog } from './DailyExchangesDialog';

@Decorators.registerClass('SYP.Setting.DailyExchangesGrid')
export class DailyExchangesGrid extends GridBase<DailyExchangesRow, any> {
    protected getColumnsKey() { return DailyExchangesColumns.columnsKey; }
    protected getDialogType() { return DailyExchangesDialog; }
    protected getRowDefinition() { return DailyExchangesRow; }
    protected getService() { return DailyExchangesService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}