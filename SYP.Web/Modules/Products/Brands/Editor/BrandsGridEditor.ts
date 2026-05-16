import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { BrandsEditorColumns, BrandsRow } from '../../ServerTypes/Products';
import { BrandsEditorDialog } from './BrandsEditorDialog';

@Decorators.registerEditor('SYP.Products.BrandsGridEditor')
export class BrandsGridEditor extends GridEditorBase<BrandsRow> {
    protected getColumnsKey() { return BrandsEditorColumns.columnsKey; }
    protected getDialogType() { return BrandsEditorDialog; }
    protected getRowDefinition() { return BrandsRow; }

    constructor(props: any) {
        super(props);
    }
}