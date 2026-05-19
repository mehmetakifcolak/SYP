import {
    Decorators, DialogButton, Lookup, TemplatedDialog,
    getLookupAsync, htmlEncode, notifyError, notifySuccess, notifyWarning
} from '@serenity-is/corelib';
import { ProductsRow } from '../../ServerTypes/Catalog';
import { CustomersRow } from '../../ServerTypes/Customer';
import { OrderDetailRow, OrderRow, OrderService, OrderStatus } from '../../ServerTypes/Order';

export interface OrderEditDialogOptions {
    entityId: number;
    onSave?: () => void;
}

const STATUS_LABEL: Record<number, string> = {
    1:  'Talep Gönderildi',
    2:  'Revize Edildi',
    3:  'Bayi Onayladı',
    4:  'Bayi Reddetti',
    5:  'Dekont Yüklendi',
    6:  'Dekont Reddedildi',
    7:  'Hazırlanıyor',
    8:  'Sevk Aşamasında',
    9:  'Teslim Edildi',
    10: 'Talep İptal'
};

@Decorators.panel(true)
@Decorators.registerClass('SYP.Order.OrderEditDialog')
export class OrderEditDialog extends TemplatedDialog<OrderEditDialogOptions> {
    private entityId: number;
    private order: OrderRow | null = null;
    private rows: OrderDetailRow[] = [];
    private productLookup!: Lookup<ProductsRow>;
    private customerLookup!: Lookup<CustomersRow>;

    private headerEl!: HTMLElement;
    private tableBodyEl!: HTMLElement;
    private totalEl!: HTMLElement;
    private addProductSelectEl!: HTMLSelectElement;

    constructor(props?: OrderEditDialogOptions) {
        super(props);
        this.entityId = props?.entityId!;
        this.dialogTitle = 'Sipariş Düzenle';
    }

    protected getTemplate(): string {
        return `
<div class="oed-root">
    <div id="~_Header" class="oed-header"></div>
    <div class="oed-table-wrap">
        <table class="oed-table">
            <thead>
                <tr>
                    <th class="oed-col-product">Ürün</th>
                    <th class="oed-col-qty">Miktar</th>
                    <th class="oed-col-price">Birim Fiyat</th>
                    <th class="oed-col-disc">İndirim</th>
                    <th class="oed-col-total">Satır Toplamı</th>
                    <th class="oed-col-del"></th>
                </tr>
            </thead>
            <tbody id="~_TableBody">
                <tr><td colspan="6" class="oed-loading">
                    <i class="fa fa-spinner fa-spin"></i>&nbsp;Yükleniyor...
                </td></tr>
            </tbody>
        </table>
    </div>
    <div class="oed-add-row">
        <label class="oed-add-label">Ürün Ekle</label>
        <select id="~_AddProductSelect" class="form-control form-control-sm oed-add-select">
            <option value="">— Ürün Seçin —</option>
        </select>
        <button id="~_AddBtn" class="btn btn-sm btn-primary oed-add-product-btn">
            <i class="fa fa-plus"></i>&nbsp;Ekle
        </button>
    </div>
    <div class="oed-footer">
        <div class="oed-total-wrap">
            <span class="oed-total-label">Genel Toplam</span>
            <strong id="~_Total" class="oed-total-val">0,00 ₺</strong>
        </div>
        <button id="~_SaveBtn" class="btn btn-success oed-save-btn">
            <i class="fa fa-save"></i>&nbsp;Kaydet
        </button>
    </div>
</div>`;
    }

    protected onDialogOpen(): void {
        super.onDialogOpen();
        this.bindStatic();
        this.loadData();
    }

    private bindStatic(): void {
        const n = (id: string) => this.byId(id)?.getNode() as HTMLElement;
        this.headerEl           = n('Header');
        this.tableBodyEl        = n('TableBody');
        this.totalEl            = n('Total');
        this.addProductSelectEl = n('AddProductSelect') as HTMLSelectElement;

        n('AddBtn').addEventListener('click', () => this.addProduct());
        n('SaveBtn').addEventListener('click', () => this.saveOrder());
    }

    private async loadData(): Promise<void> {
        try {
            [this.productLookup, this.customerLookup] = await Promise.all([
                getLookupAsync<ProductsRow>(ProductsRow.lookupKey),
                getLookupAsync<CustomersRow>(CustomersRow.lookupKey)
            ]);

            this.populateProductSelect();

            const resp = await OrderService.Retrieve({ EntityId: this.entityId });
            this.order = resp.Entity;
            if (!this.order) {
                notifyError('Sipariş bulunamadı.');
                return;
            }

            this.rows = this.order.DetailList ? [...this.order.DetailList] : [];
            this.renderHeader();
            this.renderTable();
        } catch (err: any) {
            notifyError('Sipariş yüklenemedi: ' + (err?.message || ''));
        }
    }

    private populateProductSelect(): void {
        const sel = this.addProductSelectEl;
        if (!sel) return;
        const activeProducts = this.productLookup.items.filter(p => p.IsActive !== 0);
        activeProducts.sort((a, b) => (a.Name || '').localeCompare(b.Name || '', 'tr'));
        activeProducts.forEach(p => {
            const opt = document.createElement('option');
            opt.value = String(p.Id);
            opt.textContent = `${p.Code || ''} - ${p.Name || ''}`;
            sel.appendChild(opt);
        });
    }

    private renderHeader(): void {
        if (!this.headerEl || !this.order) return;
        const o = this.order;
        const customer = o.CustomerId ? this.customerLookup?.itemById[o.CustomerId] : null;
        const customerName = o.CustomerName || customer?.Name || '—';
        const dateStr = o.OrderDate
            ? new Date(o.OrderDate).toLocaleDateString('tr-TR')
            : '—';
        const statusText = o.Status != null ? (STATUS_LABEL[o.Status as number] ?? String(o.Status)) : '—';

        this.headerEl.innerHTML = `
<div class="oed-header-grid">
    <div class="oed-hfield">
        <span class="oed-hlabel">Sipariş No</span>
        <span class="oed-hval">${htmlEncode(o.OrderNumber || '—')}</span>
    </div>
    <div class="oed-hfield">
        <span class="oed-hlabel">Müşteri</span>
        <span class="oed-hval">${htmlEncode(customerName)}</span>
    </div>
    <div class="oed-hfield">
        <span class="oed-hlabel">Tarih</span>
        <span class="oed-hval">${htmlEncode(dateStr)}</span>
    </div>
    <div class="oed-hfield">
        <span class="oed-hlabel">Durum</span>
        <span class="oed-hval oed-status">${htmlEncode(statusText)}</span>
    </div>
    <div class="oed-hfield">
        <span class="oed-hlabel">Net Tutar</span>
        <span class="oed-hval oed-net-amount">${this.fmt(o.NetAmount ?? 0)}&nbsp;₺</span>
    </div>
</div>`;
    }

    private renderTable(): void {
        if (!this.tableBodyEl) return;

        if (this.rows.length === 0) {
            this.tableBodyEl.innerHTML =
                '<tr><td colspan="6" class="oed-empty">Sipariş kalemi yok. Yukarıdan ürün ekleyebilirsiniz.</td></tr>';
            this.updateTotal();
            return;
        }

        let html = '';
        this.rows.forEach((row, idx) => {
            const p = row.ProductId ? this.productLookup?.itemById[row.ProductId] : null;
            const productName = p
                ? `${p.Code || ''} - ${p.Name || ''}`
                : (row.ProductCodeName || '—');
            const qty   = row.Quantity  ?? 1;
            const price = row.UnitPrice ?? 0;
            const disc  = row.Discount  ?? 0;
            const total = row.LineTotal ?? (qty * price - disc);

            html += `
<tr data-idx="${idx}">
    <td class="oed-col-product">${htmlEncode(productName)}</td>
    <td class="oed-col-qty">
        <input type="number" class="form-control form-control-sm oed-num oed-qty"
               data-idx="${idx}" value="${qty}" min="0.001" step="1" />
    </td>
    <td class="oed-col-price">
        <input type="number" class="form-control form-control-sm oed-num oed-price"
               data-idx="${idx}" value="${price}" min="0" step="0.01" />
    </td>
    <td class="oed-col-disc">
        <input type="number" class="form-control form-control-sm oed-num oed-disc"
               data-idx="${idx}" value="${disc}" min="0" step="0.01" />
    </td>
    <td class="oed-col-total oed-line-total" data-idx="${idx}">
        ${this.fmt(total)}&nbsp;₺
    </td>
    <td class="oed-col-del">
        <button class="btn btn-sm btn-link oed-del-btn" data-idx="${idx}" title="Kaldır">
            <i class="fa fa-trash-o text-danger"></i>
        </button>
    </td>
</tr>`;
        });

        this.tableBodyEl.innerHTML = html;
        this.bindTableEvents();
        this.updateTotal();
    }

    private bindTableEvents(): void {
        const tb = this.tableBodyEl;

        tb.querySelectorAll<HTMLInputElement>('.oed-num').forEach(inp => {
            inp.addEventListener('input', () => {
                const idx = parseInt(inp.dataset.idx!);
                this.recalcRow(idx);
            });
        });

        tb.querySelectorAll<HTMLButtonElement>('.oed-del-btn').forEach(btn => {
            btn.addEventListener('click', () => {
                const idx = parseInt(btn.dataset.idx!);
                this.rows.splice(idx, 1);
                this.renderTable();
            });
        });
    }

    private recalcRow(idx: number): void {
        const tb = this.tableBodyEl;
        const row = this.rows[idx];
        if (!row) return;

        const qty   = parseFloat(tb.querySelector<HTMLInputElement>(`.oed-qty[data-idx="${idx}"]`)?.value   || '0') || 0;
        const price = parseFloat(tb.querySelector<HTMLInputElement>(`.oed-price[data-idx="${idx}"]`)?.value || '0') || 0;
        const disc  = parseFloat(tb.querySelector<HTMLInputElement>(`.oed-disc[data-idx="${idx}"]`)?.value  || '0') || 0;

        row.Quantity  = qty;
        row.UnitPrice = price;
        row.Discount  = disc;
        row.LineTotal = qty * price - disc;

        const cell = tb.querySelector<HTMLElement>(`.oed-line-total[data-idx="${idx}"]`);
        if (cell) cell.textContent = this.fmt(row.LineTotal) + ' ₺';
        this.updateTotal();
    }

    private addProduct(): void {
        const sel = this.addProductSelectEl;
        if (!sel?.value) {
            notifyWarning('Lütfen eklenecek ürünü seçin.');
            return;
        }

        const productId = parseInt(sel.value);
        const p = this.productLookup?.itemById[productId];
        if (!p) return;

        const existing = this.rows.find(r => r.ProductId === productId);
        if (existing) {
            existing.Quantity  = (existing.Quantity ?? 1) + 1;
            existing.LineTotal = existing.Quantity * (existing.UnitPrice ?? 0) - (existing.Discount ?? 0);
            this.renderTable();
            sel.value = '';
            return;
        }

        const unitPrice = p.CurrentValidPrice ?? p.UnitPrice ?? 0;
        this.rows.push({
            ProductId: p.Id,
            Quantity:  1,
            UnitId:    p.UnitId,
            UnitPrice: unitPrice,
            VatRateId: p.VatRateId,
            VatRate:   p.VatRate,
            Discount:  0,
            LineTotal: unitPrice
        });

        this.renderTable();
        sel.value = '';
    }

    private updateTotal(): void {
        const total = this.rows.reduce((s, r) => s + (r.LineTotal ?? 0), 0);
        if (this.totalEl) this.totalEl.textContent = this.fmt(total) + ' ₺';
    }

    private async saveOrder(): Promise<void> {
        if (!this.order) return;

        if (this.rows.length === 0) {
            notifyWarning('Sipariş kalemlerini doldurun.');
            return;
        }

        const totalAmount = this.rows.reduce((s, r) => s + (r.LineTotal ?? 0), 0);
        const updatedOrder: OrderRow = {
            ...this.order,
            TotalAmount: totalAmount,
            NetAmount:   totalAmount,
            DetailList:  this.rows
        };

        try {
            await OrderService.Update({ EntityId: this.entityId, Entity: updatedOrder });
            notifySuccess('Sipariş başarıyla güncellendi!');
            this.options?.onSave?.();
            this.dialogClose();
        } catch (err: any) {
            notifyError('Güncelleme sırasında hata: ' + (err?.message || 'Bilinmeyen hata'));
        }
    }

    private fmt(n: number): string {
        return new Intl.NumberFormat('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
    }

    protected getDialogButtons(): DialogButton[] {
        return [];
    }

    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 980;
        return opt;
    }
}
