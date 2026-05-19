import { Decorators, EntityGrid } from '@serenity-is/corelib';
import { OrderColumns, OrderRow, OrderService } from '../../ServerTypes/Order';
import { OrderDialog } from './OrderDialog';
import { OrderEditDialog } from './OrderEditDialog';

@Decorators.registerClass('SYP.Order.OrderGrid')
export class OrderGrid extends EntityGrid<OrderRow, any> {
    protected getColumnsKey() { return OrderColumns.columnsKey; }
    protected getRowDefinition() { return OrderRow; }
    protected getService() { return OrderService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected override createColumns(): any[] {
        const cols = super.createColumns();
        cols.push({
            field: '_editAction',
            name: '',
            width: 110,
            minWidth: 110,
            maxWidth: 110,
            sortable: false,
            format: (_ctx: any) => {
                const btn = document.createElement('button');
                btn.type = 'button';
                btn.className = 'btn btn-xs btn-primary row-edit-order-btn';
                const icon = document.createElement('i');
                icon.className = 'fa fa-edit';
                btn.appendChild(icon);
                btn.appendChild(document.createTextNode(' Düzenle'));
                return btn;
            }
        });
        return cols;
    }

    protected onClick(e: Event, row: number, _cell: number): void {
        super.onClick(e, row, _cell);
        if ((e.target as HTMLElement).closest?.('.row-edit-order-btn')) {
            const item = (this as any).view?.getItem(row) as OrderRow;
            if (item?.Id)
                new OrderEditDialog({ entityId: item.Id, onSave: () => this.refresh() }).dialogOpen();
        }
    }

    protected editItem(entityOrId: any): void {
        const id = typeof entityOrId === 'number'
            ? entityOrId
            : (entityOrId?.Id != null ? entityOrId.Id : null);

        if (!id) return;
        new OrderEditDialog({ entityId: id, onSave: () => this.refresh() }).dialogOpen();
    }

    protected addButtonClick(): void {
        new OrderDialog({ entityId: null, onSave: () => this.refresh() }).dialogOpen();
    }
}
