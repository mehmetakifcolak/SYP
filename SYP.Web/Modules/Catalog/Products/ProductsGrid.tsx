import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { ProductsColumns, ProductsRow, ProductsService } from '../../ServerTypes/Catalog';
import { ProductsDialog } from './ProductsDialog';

@Decorators.registerClass('SYP.Catalog.ProductsGrid')
export class ProductsGrid extends EntityGrid<ProductsRow, any> {
    protected getColumnsKey() { return ProductsColumns.columnsKey; }
    protected getDialogType() { return ProductsDialog; }
    protected getRowDefinition() { return ProductsRow; }
    protected getService() { return ProductsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}