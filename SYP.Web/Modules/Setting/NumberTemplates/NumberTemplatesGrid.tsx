import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { NumberTemplatesColumns, NumberTemplatesRow, NumberTemplatesService } from '../../ServerTypes/Setting';
import { NumberTemplatesDialog } from './NumberTemplatesDialog';

@Decorators.registerClass('SYP.Setting.NumberTemplatesGrid')
export class NumberTemplatesGrid extends GridBase<NumberTemplatesRow, any> {
    protected getColumnsKey() { return NumberTemplatesColumns.columnsKey; }
    protected getDialogType() { return NumberTemplatesDialog; }
    protected getRowDefinition() { return NumberTemplatesRow; }
    protected getService() { return NumberTemplatesService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}