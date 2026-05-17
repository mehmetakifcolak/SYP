import { Decorators, TreeGridMixin, EntityGrid} from '@serenity-is/corelib';
import { ProductCategoryDialog } from './ProductCategoryDialog';
import { ProductCategoryRow, ProductCategoryColumns, ProductCategoryService } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.ProductCategoryGrid')
export class ProductCategoryGrid extends EntityGrid<ProductCategoryRow, any> {
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