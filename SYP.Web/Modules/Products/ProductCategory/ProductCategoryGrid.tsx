import { Decorators, TreeGridMixin } from '@serenity-is/corelib';
import { GridBase } from '@/_Ext/Bases/GridBase';
import { ProductCategoryColumns, ProductCategoryRow, ProductCategoryService } from '../../ServerTypes/Products';
import { ProductCategoryDialog } from './ProductCategoryDialog';

@Decorators.registerClass('SYP.Products.ProductCategoryGrid')
export class ProductCategoryGrid extends GridBase<ProductCategoryRow, any> {
    protected getColumnsKey() { return ProductCategoryColumns.columnsKey; }
    protected getDialogType() { return ProductCategoryDialog; }
    protected getRowDefinition() { return ProductCategoryRow; }
    protected getService() { return ProductCategoryService.baseUrl; }

    private treeMixin: TreeGridMixin<ProductCategoryRow>;

    constructor(props: any) {
        super(props);

        this.treeMixin = new TreeGridMixin({
            grid: this,
            getParentId: x => x.ParentId,
            toggleField: "Name",
            initialCollapse: () => false
        });
    }

    protected usePager() {
        return false;
    }

    protected getInitialTitle() {
        return "Kategoriler";
    }
}