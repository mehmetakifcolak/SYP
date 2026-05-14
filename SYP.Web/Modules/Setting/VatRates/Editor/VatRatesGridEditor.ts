import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { VatRatesEditorColumns, VatRatesRow } from '../../ServerTypes/Setting';
import { VatRatesEditorDialog } from './VatRatesEditorDialog';

@Decorators.registerEditor('SYP.Setting.VatRatesGridEditor')
export class VatRatesGridEditor extends GridEditorBase<VatRatesRow> {
    protected getColumnsKey() { return VatRatesEditorColumns.columnsKey; }
    protected getDialogType() { return VatRatesEditorDialog; }
    protected getRowDefinition() { return VatRatesRow; }

    constructor(props: any) {
        super(props);
    }
}