import { GridEditorBase } from '@/Common/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductCategoryEditorDialog } from './ProductCategoryEditorDialog';
import { ProductCategoryRow, ProductCategoryEditorColumns } from '../../../ServerTypes/Catalog';

@Decorators.registerEditor('SYP.Catalog.ProductCategoryGridEditor')
export class ProductCategoryGridEditor extends GridEditorBase<ProductCategoryRow> {
    protected getColumnsKey() { return ProductCategoryEditorColumns.columnsKey; }
    protected getDialogType() { return ProductCategoryEditorDialog; }
    protected getRowDefinition() { return ProductCategoryRow; }

    constructor(props: any) {
        super(props);
    }
}