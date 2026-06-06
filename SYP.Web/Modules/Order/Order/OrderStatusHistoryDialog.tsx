import { Decorators, DialogButton, TemplatedDialog, htmlEncode, localText, notifyError } from '@serenity-is/corelib';
import { OrderStatusHistRow, OrderStatusHistService } from '../../ServerTypes/Order';

export interface OrderStatusHistoryDialogOptions {
    orderId: number;
    orderNumber?: string;
}

const STATUS_LABELS: Record<number, { key: string; label: string; cls: string }> = {
    1:  { key: 'Site.OrderHistory.StatusRequestSent',      label: 'Talep Gönderildi',   cls: 'blue'   },
    2:  { key: 'Site.OrderHistory.StatusRevised',          label: 'Revize Edildi',       cls: 'orange' },
    3:  { key: 'Site.OrderHistory.StatusDealerApproved',   label: 'Bayi Onayladı',       cls: 'teal'   },
    4:  { key: 'Site.OrderHistory.StatusDealerRejected',   label: 'Bayi Reddetti',       cls: 'red'    },
    5:  { key: 'Site.OrderHistory.StatusReceiptUploaded',  label: 'Dekont Yüklendi',     cls: 'blue'   },
    6:  { key: 'Site.OrderHistory.StatusReceiptRejected',  label: 'Dekont Reddedildi',   cls: 'red'    },
    7:  { key: 'Site.OrderHistory.StatusPreparing',        label: 'Hazırlanıyor',        cls: 'orange' },
    8:  { key: 'Site.OrderHistory.StatusInShipment',       label: 'Sevk Aşamasında',     cls: 'blue'   },
    9:  { key: 'Site.OrderHistory.StatusDelivered',        label: 'Teslim Alındı',       cls: 'green'  },
    10: { key: 'Site.OrderHistory.StatusRequestCancelled', label: 'Talep İptal',         cls: 'red'    },
    11: { key: 'Site.OrderHistory.StatusRepApproved',      label: 'Temsilci Onayladı',   cls: 'green'  },
    12: { key: 'Site.OrderHistory.StatusReceiptApproved',  label: 'Dekont Onaylandı',    cls: 'green'  },
    13: { key: 'Site.OrderHistory.StatusNotDelivered',     label: 'Teslim Alınmadı',     cls: 'red'    },
    14: { key: 'Site.OrderHistory.StatusRequestOnHold',    label: 'Talep Beklette',      cls: 'yellow' },
    15: { key: 'Site.OrderHistory.StatusShipmentPreparing', label: 'Kargo Hazırlanıyor', cls: 'orange' },
};

function statusBadge(status: number | undefined | null): string {
    if (status == null) return '<span class="oed-hist-badge oed-hist-badge-gray">—</span>';
    const s = STATUS_LABELS[status];
    if (!s) return `<span class="oed-hist-badge oed-hist-badge-gray">#${status}</span>`;
    return `<span class="oed-hist-badge oed-hist-badge-${s.cls}">${htmlEncode(localText(s.key, s.label))}</span>`;
}

@Decorators.registerClass('SYP.Order.OrderStatusHistoryDialog')
export class OrderStatusHistoryDialog extends TemplatedDialog<OrderStatusHistoryDialogOptions> {
    constructor(props: OrderStatusHistoryDialogOptions) {
        super(props);
        this.dialogTitle = `${localText('Site.OrderHistory.Title', 'Durum Geçmişi')}${props.orderNumber ? ' — ' + props.orderNumber : ''}`;
    }

    protected getTemplate(): string {
        return `<div id="~_Body" class="osh-body">
    <div class="oed-history-empty"><i class="fa fa-spinner fa-spin"></i>&nbsp;${localText('Site.OrderHistory.Loading', 'Yükleniyor...')}</div>
</div>`;
    }

    protected onDialogOpen(): void {
        super.onDialogOpen();
        this.loadHistory();
    }

    private async loadHistory(): Promise<void> {
        const body = this.byId('Body')?.getNode() as HTMLElement;
        if (!body) return;
        try {
            const resp = await OrderStatusHistService.List({
                EqualityFilter: { OrderId: String(this.options.orderId) },
                Sort: ['-Id'],
                Take: 500
            });
            const rows: OrderStatusHistRow[] = resp?.Entities ?? [];
            if (rows.length === 0) {
                body.innerHTML = `<div class="oed-history-empty">${localText('Site.OrderHistory.NoRecords', 'Durum geçmişi kaydı bulunamadı.')}</div>`;
                return;
            }
            let html = `<table class="oed-history-table">
<thead><tr>
    <th>${localText('Site.OrderHistory.ColDate', 'Tarih')}</th>
    <th>${localText('Site.OrderHistory.ColOldStatus', 'Eski Durum')}</th>
    <th>${localText('Site.OrderHistory.ColNewStatus', 'Yeni Durum')}</th>
    <th>${localText('Site.OrderHistory.ColUser', 'Kullanıcı')}</th>
    <th>${localText('Site.OrderHistory.ColReason', 'Neden')}</th>
</tr></thead><tbody>`;
            for (const r of rows) {
                const date = r.ChangeDate
                    ? new Date(r.ChangeDate).toLocaleString('tr-TR', {
                        day: '2-digit', month: '2-digit', year: 'numeric',
                        hour: '2-digit', minute: '2-digit'
                      })
                    : '—';
                html += `<tr>
    <td style="white-space:nowrap;color:#6b7280;font-size:11px">${htmlEncode(date)}</td>
    <td>${statusBadge(r.OldStatus)}</td>
    <td>${statusBadge(r.NewStatus)}</td>
    <td style="font-size:11px;color:#4b5563">${htmlEncode(r.ChangedByUserUsername || r.ChangedByUserRole || '—')}</td>
    <td style="font-size:11px;color:#6b7280;max-width:220px;overflow:hidden;text-overflow:ellipsis;white-space:nowrap"
        title="${htmlEncode(r.ChangeReason || '')}">${r.ChangeReason ? htmlEncode(r.ChangeReason) : '—'}</td>
</tr>`;
            }
            html += '</tbody></table>';
            body.innerHTML = html;
        } catch (err: any) {
            notifyError(localText('Site.OrderHistory.LoadFailedPrefix', 'Geçmiş yüklenemedi: ') + (err?.message || ''));
            body.innerHTML = `<div class="oed-history-empty" style="color:#dc2626">${localText('Site.OrderHistory.LoadFailed', 'Geçmiş yüklenemedi.')}</div>`;
        }
    }

    protected getDialogButtons(): DialogButton[] {
        return [{ text: localText('Site.OrderHistory.Close', 'Kapat'), cssClass: 'btn-default', click: () => this.dialogClose() }];
    }

    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 680;
        return opt;
    }
}
