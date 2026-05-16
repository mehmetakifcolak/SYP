import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductCategoryEditorColumns, ProductCategoryRow } from '../../ServerTypes/Products';
import { ProductCategoryEditorDialog } from './ProductCategoryEditorDialog';

@Decorators.registerEditor('SYP.Products.ProductCategoryGridEditor')
export class ProductCategoryGridEditor extends GridEditorBase<ProductCategoryRow> {
    protected getColumnsKey() { return ProductCategoryEditorColumns.columnsKey; }
    protected getDialogType() { return ProductCategoryEditorDialog; }
    protected getRowDefinition() { return ProductCategoryRow; }

    constructor(props: any) {
        super(props);
    }
}