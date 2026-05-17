import { GridEditorBase } from '@/Common/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { CurrencyListEditorColumns, CurrencyListRow } from '../../ServerTypes/Setting';
import { CurrencyListEditorDialog } from './CurrencyListEditorDialog';

@Decorators.registerEditor('SYP.Setting.CurrencyListGridEditor')
export class CurrencyListGridEditor extends GridEditorBase<CurrencyListRow> {
    protected getColumnsKey() { return CurrencyListEditorColumns.columnsKey; }
    protected getDialogType() { return CurrencyListEditorDialog; }
    protected getRowDefinition() { return CurrencyListRow; }

    constructor(props: any) {
        super(props);
    }
}