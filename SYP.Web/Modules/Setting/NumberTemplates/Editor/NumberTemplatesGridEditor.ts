import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { NumberTemplatesEditorColumns, NumberTemplatesRow } from '../../ServerTypes/Setting';
import { NumberTemplatesEditorDialog } from './NumberTemplatesEditorDialog';

@Decorators.registerEditor('SYP.Setting.NumberTemplatesGridEditor')
export class NumberTemplatesGridEditor extends GridEditorBase<NumberTemplatesRow> {
    protected getColumnsKey() { return NumberTemplatesEditorColumns.columnsKey; }
    protected getDialogType() { return NumberTemplatesEditorDialog; }
    protected getRowDefinition() { return NumberTemplatesRow; }

    constructor(props: any) {
        super(props);
    }
}