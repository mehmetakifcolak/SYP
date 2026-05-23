import { Decorators, DialogButton, TemplatedDialog, htmlEncode, notifyError } from '@serenity-is/corelib';
import { OrderStatusHistRow, OrderStatusHistService } from '../../ServerTypes/Order';

export interface OrderStatusHistoryDialogOptions {
    orderId: number;
    orderNumber?: string;
}

const STATUS_LABELS: Record<number, { label: string; cls: string }> = {
    1:  { label: 'Talep Gönderildi',   cls: 'blue'   },
    2:  { label: 'Revize Edildi',       cls: 'orange' },
    3:  { label: 'Bayi Onayladı',       cls: 'teal'   },
    4:  { label: 'Bayi Reddetti',       cls: 'red'    },
    5:  { label: 'Dekont Yüklendi',     cls: 'blue'   },
    6:  { label: 'Dekont Reddedildi',   cls: 'red'    },
    7:  { label: 'Hazırlanıyor',        cls: 'orange' },
    8:  { label: 'Sevk Aşamasında',     cls: 'blue'   },
    9:  { label: 'Teslim Alındı',       cls: 'green'  },
    10: { label: 'Talep İptal',         cls: 'red'    },
    11: { label: 'Temsilci Onayladı',   cls: 'green'  },
    12: { label: 'Dekont Onaylandı',    cls: 'green'  },
    13: { label: 'Teslim Alınmadı',     cls: 'red'    },
    14: { label: 'Talep Beklette',      cls: 'yellow' },
    15: { label: 'Kargo Hazırlanıyor',  cls: 'orange' },
};

function statusBadge(status: number | undefined | null): string {
    if (status == null) return '<span class="oed-hist-badge oed-hist-badge-gray">—</span>';
    const s = STATUS_LABELS[status];
    if (!s) return `<span class="oed-hist-badge oed-hist-badge-gray">#${status}</span>`;
    return `<span class="oed-hist-badge oed-hist-badge-${s.cls}">${htmlEncode(s.label)}</span>`;
}

@Decorators.registerClass('SYP.Order.OrderStatusHistoryDialog')
export class OrderStatusHistoryDialog extends TemplatedDialog<OrderStatusHistoryDialogOptions> {
    constructor(props: OrderStatusHistoryDialogOptions) {
        super(props);
        this.dialogTitle = `Durum Geçmişi${props.orderNumber ? ' — ' + props.orderNumber : ''}`;
    }

    protected getTemplate(): string {
        return `<div id="~_Body" class="osh-body">
    <div class="oed-history-empty"><i class="fa fa-spinner fa-spin"></i>&nbsp;Yükleniyor...</div>
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
                body.innerHTML = '<div class="oed-history-empty">Durum geçmişi kaydı bulunamadı.</div>';
                return;
            }
            let html = `<table class="oed-history-table">
<thead><tr>
    <th>Tarih</th>
    <th>Eski Durum</th>
    <th>Yeni Durum</th>
    <th>Kullanıcı</th>
    <th>Neden</th>
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
            notifyError('Geçmiş yüklenemedi: ' + (err?.message || ''));
            body.innerHTML = '<div class="oed-history-empty" style="color:#dc2626">Geçmiş yüklenemedi.</div>';
        }
    }

    protected getDialogButtons(): DialogButton[] {
        return [{ text: 'Kapat', cssClass: 'btn-default', click: () => this.dialogClose() }];
    }

    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 680;
        return opt;
    }
}
