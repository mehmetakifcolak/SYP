import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { PriceListsColumns, PriceListsRow, PriceListsService } from '../../ServerTypes/Catalog';
import { PriceListsDialog } from './PriceListsDialog';

@Decorators.registerClass('SYP.Catalog.PriceListsGrid')
export class PriceListsGrid extends EntityGrid<PriceListsRow, any> {
    protected getColumnsKey() { return PriceListsColumns.columnsKey; }
    protected getDialogType() { return PriceListsDialog; }
    protected getRowDefinition() { return PriceListsRow; }
    protected getService() { return PriceListsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}