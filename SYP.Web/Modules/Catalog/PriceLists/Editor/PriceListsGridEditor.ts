import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { PriceListsEditorColumns, PriceListsRow } from '../../ServerTypes/Catalog';
import { PriceListsEditorDialog } from './PriceListsEditorDialog';

@Decorators.registerEditor('SYP.Catalog.PriceListsGridEditor')
export class PriceListsGridEditor extends GridEditorBase<PriceListsRow> {
    protected getColumnsKey() { return PriceListsEditorColumns.columnsKey; }
    protected getDialogType() { return PriceListsEditorDialog; }
    protected getRowDefinition() { return PriceListsRow; }

    constructor(props: any) {
        super(props);
    }
}