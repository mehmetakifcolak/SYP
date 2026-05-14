import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { DailyExchangesEditorColumns, DailyExchangesRow } from '../../ServerTypes/Setting';
import { DailyExchangesEditorDialog } from './DailyExchangesEditorDialog';

@Decorators.registerEditor('SYP.Setting.DailyExchangesGridEditor')
export class DailyExchangesGridEditor extends GridEditorBase<DailyExchangesRow> {
    protected getColumnsKey() { return DailyExchangesEditorColumns.columnsKey; }
    protected getDialogType() { return DailyExchangesEditorDialog; }
    protected getRowDefinition() { return DailyExchangesRow; }

    constructor(props: any) {
        super(props);
    }
}