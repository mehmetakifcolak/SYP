import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductsEditorColumns, ProductsRow } from '../../ServerTypes/Catalog';
import { ProductsEditorDialog } from './ProductsEditorDialog';

@Decorators.registerEditor('SYP.Catalog.ProductsGridEditor')
export class ProductsGridEditor extends GridEditorBase<ProductsRow> {
    protected getColumnsKey() { return ProductsEditorColumns.columnsKey; }
    protected getDialogType() { return ProductsEditorDialog; }
    protected getRowDefinition() { return ProductsRow; }

    constructor(props: any) {
        super(props);
    }
}