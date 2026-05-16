import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { CountryEditorColumns, CountryRow } from '../../ServerTypes/Setting';
import { CountryEditorDialog } from './CountryEditorDialog';

@Decorators.registerEditor('SYP.Setting.CountryGridEditor')
export class CountryGridEditor extends GridEditorBase<CountryRow> {
    protected getColumnsKey() { return CountryEditorColumns.columnsKey; }
    protected getDialogType() { return CountryEditorDialog; }
    protected getRowDefinition() { return CountryRow; }

    constructor(props: any) {
        super(props);
    }
}