import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { OrderDocumentColumns, OrderDocumentRow, OrderDocumentService } from '../../ServerTypes/Order';
import { OrderDocumentDialog } from './OrderDocumentDialog';

@Decorators.registerClass('SYP.Order.OrderDocumentGrid')
export class OrderDocumentGrid extends EntityGrid<OrderDocumentRow, any> {
    protected getColumnsKey() { return OrderDocumentColumns.columnsKey; }
    protected getDialogType() { return OrderDocumentDialog; }
    protected getRowDefinition() { return OrderDocumentRow; }
    protected getService() { return OrderDocumentService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}