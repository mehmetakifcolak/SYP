import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { ProductsColumns, ProductsRow, ProductsService } from '../../ServerTypes/Catalog';
import { ProductsDialog } from './ProductsDialog';

@Decorators.registerClass('SYP.Catalog.ProductsGrid')
export class ProductsGrid extends GridBase<ProductsRow, any> {
    protected getColumnsKey() { return ProductsColumns.columnsKey; }
    protected getDialogType() { return ProductsDialog; }
    protected getRowDefinition() { return ProductsRow; }
    protected getService() { return ProductsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}