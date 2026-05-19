import { Decorators, EntityGrid, ToolButton, notifyWarning } from '@serenity-is/corelib';
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

    protected getButtons(): ToolButton[] {
        const buttons = super.getButtons();
        buttons.push({
            title: 'Siparişi Düzenle',
            cssClass: 'edit-order-btn',
            icon: 'fa-edit',
            separator: true,
            onClick: () => {
                const selectedRows = (this as any).slickGrid?.getSelectedRows?.() as number[];
                if (!selectedRows?.length) {
                    notifyWarning('Lütfen düzenlemek istediğiniz siparişi seçin.');
                    return;
                }
                const item = (this as any).view?.getItem(selectedRows[0]) as OrderRow;
                if (!item?.Id) return;
                new OrderEditDialog({ entityId: item.Id, onSave: () => this.refresh() }).dialogOpen();
            }
        });
        return buttons;
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
