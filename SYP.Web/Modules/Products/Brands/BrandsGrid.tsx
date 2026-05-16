import { GridBase } from '@/_Ext/Bases/GridBase';
import { Decorators } from '@serenity-is/corelib';
import { BrandsColumns, BrandsRow, BrandsService } from '../../ServerTypes/Products';
import { BrandsDialog } from './BrandsDialog';

@Decorators.registerClass('SYP.Products.BrandsGrid')
export class BrandsGrid extends GridBase<BrandsRow, any> {
    protected getColumnsKey() { return BrandsColumns.columnsKey; }
    protected getDialogType() { return BrandsDialog; }
    protected getRowDefinition() { return BrandsRow; }
    protected getService() { return BrandsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}