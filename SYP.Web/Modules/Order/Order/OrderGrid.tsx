import { Decorators, EntityGrid, localText } from '@serenity-is/corelib';
import { OrderColumns, OrderRow, OrderService } from '../../ServerTypes/Order';
import { OrderDialog } from './OrderDialog';
import { OrderEditDialog } from './OrderEditDialog';
import { OrderStatusHistoryDialog } from './OrderStatusHistoryDialog';

// Durum → ilerleme yüzdesi
const STATUS_PCT: Record<number, number> = {
    14: 0, 1: 17, 2: 17, 3: 25, 4: 17,
    11: 33, 5: 50, 6: 50, 12: 58, 7: 67,
    15: 75, 8: 83, 9: 100, 13: 100, 10: 0
};

const STATUS_LABEL: Record<number, [string, string]> = {
    14: ['Site.OrderGrid.StatusRequestPending', 'Talep Beklemede'],
    1:  ['Site.OrderGrid.StatusRequestSent', 'Talep Gönderildi'],
    2:  ['Site.OrderGrid.StatusRevised', 'Revize Edildi'],
    3:  ['Site.OrderGrid.StatusDealerApproved', 'Bayi Onayladı'],
    4:  ['Site.OrderGrid.StatusDealerRejected', 'Bayi Reddetti'],
    11: ['Site.OrderGrid.StatusRepApproved', 'Temsilci Onayladı'],
    5:  ['Site.OrderGrid.StatusReceiptUploaded', 'Dekont Yüklendi'],
    6:  ['Site.OrderGrid.StatusReceiptRejected', 'Dekont Reddedildi'],
    12: ['Site.OrderGrid.StatusReceiptApproved', 'Dekont Onaylandı'],
    7:  ['Site.OrderGrid.StatusPreparing', 'Hazırlanıyor'],
    15: ['Site.OrderGrid.StatusShipmentPreparing', 'Kargo Hazırlanıyor'],
    8:  ['Site.OrderGrid.StatusInCargo', 'Kargoda'],
    9:  ['Site.OrderGrid.StatusDelivered', 'Teslim Alındı'],
    13: ['Site.OrderGrid.StatusNotDelivered', 'Teslim Alınmadı'],
    10: ['Site.OrderGrid.StatusCancelled', 'İptal']
};

// Renk türü
function statusColor(status: number): 'cancel' | 'error' | 'done' | 'normal' {
    if (status === 10)              return 'cancel';
    if (status === 4 || status === 6 || status === 13) return 'error';
    if (status === 9)               return 'done';
    return 'normal';
}

function buildProgressCell(status: number): HTMLElement {
    const pct   = STATUS_PCT[status] ?? 0;
    const lp    = STATUS_LABEL[status];
    const label = lp ? localText(lp[0], lp[1]) : String(status);
    const type  = statusColor(status);

    // Track: arka plan gri, bar absolute olarak içinde
    const track = document.createElement('div');
    track.className = `ogr-prog-track ogr-prog-${type}`;

    // Renkli dolgu (bar)
    const bar = document.createElement('div');
    bar.className = 'ogr-prog-bar';
    bar.style.width = pct + '%';
    track.appendChild(bar);

    // Yazı katmanı (bar'ın üstünde, z-index ile)
    const content = document.createElement('div');
    content.className = 'ogr-prog-content';

    const lbl = document.createElement('span');
    lbl.className = 'ogr-prog-label';
    lbl.textContent = label;
    content.appendChild(lbl);

    const pctEl = document.createElement('span');
    pctEl.className = 'ogr-prog-pct';
    pctEl.textContent = pct + '%';
    content.appendChild(pctEl);

    track.appendChild(content);
    return track;
}

@Decorators.registerClass('SYP.Order.OrderGrid')
export class OrderGrid extends EntityGrid<OrderRow, any> {
    protected getColumnsKey() { return OrderColumns.columnsKey; }
    protected getRowDefinition() { return OrderRow; }
    protected getService() { return OrderService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected override getSlickOptions(): any {
        const opt = super.getSlickOptions();
        // Hücre yüksekliği CSS'te 44px'e sabitlendiği için SlickGrid'in satır
        // konumlamasıyla uyuşması adına rowHeight de 44 olmalı; aksi halde
        // hover renklendirmesi dikeyde kayıyor.
        opt.rowHeight = 44;
        return opt;
    }

    protected override createColumns(): any[] {
        const cols = super.createColumns();

        // Status sütununu progress bar ile değiştir
        const statusCol = cols.find((c: any) => c.field === 'Status');
        if (statusCol) {
            statusCol.width    = 200;
            statusCol.minWidth = 160;
            statusCol.format   = (ctx: any) => buildProgressCell(ctx.item?.Status as number ?? 0);
        }

        const actionsCol = {
            field: '_actions',
            name: '',
            width: 190,
            minWidth: 190,
            maxWidth: 190,
            sortable: false,
            format: (_ctx: any) => {
                const wrap = document.createElement('div');
                wrap.style.cssText = 'display:flex;gap:4px';

                const editBtn = document.createElement('button');
                editBtn.type = 'button';
                editBtn.className = 'btn btn-xs btn-primary row-edit-order-btn';
                editBtn.innerHTML = `<i class="fa fa-edit"></i> ${localText('Site.OrderGrid.Edit', 'Düzenle')}`;
                wrap.appendChild(editBtn);

                const histBtn = document.createElement('button');
                histBtn.type = 'button';
                histBtn.className = 'btn btn-xs btn-default row-history-btn';
                histBtn.innerHTML = `<i class="fa fa-history"></i> ${localText('Site.OrderGrid.History', 'Geçmiş')}`;
                wrap.appendChild(histBtn);

                return wrap;
            }
        };

        const rowNumberCol = {
            field: '_rowNumber',
            name: '#',
            width: 50,
            minWidth: 40,
            maxWidth: 60,
            sortable: false,
            format: (ctx: any) => String((ctx.row ?? 0) + 1)
        };

        // Düzenle/Geçmiş ve sıra numarası ilk kolonlarda listelensin
        cols.unshift(actionsCol, rowNumberCol);
        return cols;
    }

    protected onClick(e: Event, row: number, _cell: number): void {
        super.onClick(e, row, _cell);
        const item = (this as any).view?.getItem(row) as OrderRow;
        if (!item?.Id) return;

        if ((e.target as HTMLElement).closest?.('.row-edit-order-btn'))
            new OrderEditDialog({ entityId: item.Id, onSave: () => this.refresh() }).dialogOpen();

        if ((e.target as HTMLElement).closest?.('.row-history-btn'))
            new OrderStatusHistoryDialog({ orderId: item.Id, orderNumber: item.OrderNumber }).dialogOpen();
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
