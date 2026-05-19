import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { ProductPackingColumns, ProductPackingRow, ProductPackingService } from '../../ServerTypes/Catalog';
import { ProductPackingDialog } from './ProductPackingDialog';

@Decorators.registerClass('SYP.Catalog.ProductPackingGrid')
export class ProductPackingGrid extends EntityGrid<ProductPackingRow, any> {
    protected getColumnsKey() { return ProductPackingColumns.columnsKey; }
    protected getDialogType() { return ProductPackingDialog; }
    protected getRowDefinition() { return ProductPackingRow; }
    protected getService() { return ProductPackingService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}
