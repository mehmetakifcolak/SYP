import { GridEditorBase } from '@/Common/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductPackingEditorColumns, ProductPackingRow } from '../../ServerTypes/Catalog';
import { ProductPackingEditorDialog } from './ProductPackingEditorDialog';

@Decorators.registerEditor('SYP.Catalog.ProductPackingGridEditor')
export class ProductPackingGridEditor extends GridEditorBase<ProductPackingRow> {
    protected getColumnsKey() { return ProductPackingEditorColumns.columnsKey; }
    protected getDialogType() { return ProductPackingEditorDialog; }
    protected getRowDefinition() { return ProductPackingRow; }

    constructor(props: any) {
        super(props);
    }
}
