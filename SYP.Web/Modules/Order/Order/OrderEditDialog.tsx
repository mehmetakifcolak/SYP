import {
    Decorators, DialogButton, Lookup, TemplatedDialog,
    getLookupAsync, htmlEncode, notifyError, notifySuccess, notifyWarning
} from '@serenity-is/corelib';
import { ProductsRow } from '../../ServerTypes/Catalog';
import { AllowedTransitionItem, OrderDetailRow, OrderDocumentService, OrderRow, OrderService } from '../../ServerTypes/Order';
import { WarehousesRow } from '../../ServerTypes/Warehouse';
import { OrderDialog } from './OrderDialog';

export interface OrderEditDialogOptions {
    entityId: number;
    onSave?: () => void;
}

// ── Durum ilerleme adımları ───────────────────────────────────────
type StepState = 'done' | 'active' | 'pending' | 'error' | 'cancel';

const STEPS = [
    { label: 'Talep\nGönderildi',    icon: 'fa-paper-plane-o' },
    { label: 'Onaylandı',            icon: 'fa-thumbs-up'     },
    { label: 'Dekont\nGönderildi',   icon: 'fa-upload'        },
    { label: 'İnceleniyor',          icon: 'fa-search'        },
    { label: 'Kargoda',              icon: 'fa-truck'         },
    { label: 'Teslim Edildi',        icon: 'fa-check'         },
];

// Hangi status'da hangi adım hata verdi → gösterilecek mesaj
const STEP_ERR_MSG: Record<number, Partial<Record<number, string>>> = {
    2: { 4: 'Reddedildi', 10: 'İptal Edildi' },
    3: { 6: 'Reddedildi' },
    6: { 13: 'Teslim Alınmadı' },
};

function getStepStates(status: number): StepState[] {
    const [d, a, p, e, c]: StepState[] = ['done','active','pending','error','cancel'];
    switch (status) {
        case 14: return [a,p,p,p,p,p];  // talep beklemede → step 1 aktif
        case 1:  return [d,a,p,p,p,p];  // talep gönderildi → step 2 aktif (onay bekliyor)
        case 2:  return [d,a,p,p,p,p];  // revize edildi → step 2 aktif
        case 3:  return [d,a,p,p,p,p];  // bayi onayladı — temsilci bekliyor
        case 4:  return [d,e,p,p,p,p];  // reddedildi
        case 11: return [d,d,a,p,p,p];  // temsilci onayladı → step 3 aktif (dekont bekleniyor)
        case 5:  return [d,d,d,a,p,p];  // dekont yüklendi → step 4 aktif (inceleniyor)
        case 6:  return [d,d,e,p,p,p];  // dekont reddedildi → step 3 hata
        case 12: return [d,d,d,d,a,p];  // dekont onaylandı → step 5 aktif (kargoda)
        case 7:  return [d,d,d,d,a,p];  // hazırlanıyor (legacy) → step 5 aktif
        case 8:  return [d,d,d,d,d,a];  // kargoya verildi → step 6 aktif (teslim)
        case 9:  return [d,d,d,d,d,d];  // teslim alındı ✓
        case 13: return [d,d,d,d,d,e];  // teslim alınmadı
        case 10: return [c,c,c,c,c,c];  // iptal
        default: return [p,p,p,p,p,p];
    }
}

interface StepAction {
    label: string;
    status: number;
    icon: string;
    cls: string;
    requiresReason?: boolean;
    isUpload?: boolean;   // dosya yükleme modalı açılır
    isView?: boolean;     // belgeyi yeni sekmede görüntüle
}

// Her adım için gösterilecek sabit aksiyonlar
const STEP_ACTIONS: Record<number, StepAction[]> = {
    1: [
        { label: 'Talep Gönder',  status: 1,  icon: 'fa-paper-plane',    cls: 'success' },
        { label: 'Talebi Beklet', status: 14, icon: 'fa-pause-circle',   cls: 'warning' }
    ],
    2: [
        { label: 'Onayla',        status: 11, icon: 'fa-check',           cls: 'success' },
        { label: 'Revize İste',   status: 2,  icon: 'fa-pencil',          cls: 'warning' },
        { label: 'Reddet',        status: 10, icon: 'fa-times',           cls: 'danger',  requiresReason: true }
    ],
    3: [
        { label: 'Dekont Yükle',      status: 5, icon: 'fa-upload', cls: 'primary', isUpload: true },
        { label: 'Dekontu Görüntüle', status: 0, icon: 'fa-eye',    cls: 'info',    isView:   true },
        { label: 'Dekontu Reddet',    status: 6, icon: 'fa-ban',    cls: 'danger',  requiresReason: true }
    ],
    4: [
        { label: 'Dekontu Görüntüle', status: 0,  icon: 'fa-eye',    cls: 'info',    isView:   true },
        { label: 'İncelemeyi Onayla', status: 12, icon: 'fa-check',  cls: 'success' },
        { label: 'Dekontu Reddet',    status: 6,  icon: 'fa-ban',    cls: 'danger',  requiresReason: true }
    ],
    5: [
        { label: 'Kargoya Ver',   status: 8,  icon: 'fa-truck',           cls: 'info'    }
    ],
    6: [
        { label: 'Teslim Alındı',   status: 9,  icon: 'fa-check-circle',       cls: 'success' },
        { label: 'Teslim Alınmadı', status: 13, icon: 'fa-exclamation-circle', cls: 'danger',  requiresReason: true }
    ]
};

// Hangi status'da hangi adımda sebep rozeti gösterilir
const STATUS_REASON_STEP: Record<number, number> = {
    2:  2,   // REVIZE_EDILDI
    4:  2,   // BAYI_REDDETTI
    6:  3,   // DEKONT_REDDEDILDI
    13: 6,   // TESLIM_ALINMADI
};


@Decorators.panel(true)
@Decorators.registerClass('SYP.Order.OrderEditDialog')
export class OrderEditDialog extends TemplatedDialog<OrderEditDialogOptions> {
    private entityId: number;
    private order: OrderRow | null = null;
    private rows: OrderDetailRow[] = [];
    private transitions: AllowedTransitionItem[] = [];
    private productLookup!: Lookup<ProductsRow>;
    private warehouseLookup!: Lookup<WarehousesRow>;

    private statusFlowEl!: HTMLElement;
    private stepPopupEl!: HTMLElement;
    private tableBodyEl!: HTMLElement;
    private totalEl!: HTMLElement;
    private warehouseSelectEl!: HTMLSelectElement;

    // Açıklama modali
    private reasonModalEl!: HTMLElement;
    private reasonModalTitleEl!: HTMLElement;
    private reasonModalBodyEl!: HTMLElement;
    private reasonTextEl: HTMLTextAreaElement | null = null;
    private pendingTransition: AllowedTransitionItem | null = null;

    // Upload modali
    private uploadModalEl!: HTMLElement;
    private uploadInputEl!: HTMLInputElement;
    private uploadPreviewEl!: HTMLElement;
    private uploadFileNameEl!: HTMLElement;
    private uploadSubmitEl!: HTMLButtonElement;
    private selectedFile: File | null = null;
    private outsideClickHandler: ((e: MouseEvent) => void) | null = null;

    constructor(props?: OrderEditDialogOptions) {
        super(props);
        this.entityId = props?.entityId!;
        this.dialogTitle = 'Sipariş Düzenle';
    }

    protected getTemplate(): string {
        return `
<div class="oed-root">
    <div class="oed-header">
        <div id="~_HeaderGrid" class="oed-header-grid"></div>
        <div id="~_StatusFlow" class="oed-status-flow-bar"></div>
    </div>
    <div class="oed-warehouse-bar">
        <label class="oed-warehouse-label">
            <i class="fa fa-building-o"></i>&nbsp;Stok Çıkış Deposu
        </label>
        <select id="~_WarehouseSelect" class="form-control form-control-sm oed-warehouse-select">
            <option value="">— Depo Seçin —</option>
        </select>
    </div>
    <div class="oed-table-wrap">
        <table class="oed-table">
            <thead>
                <tr>
                    <th class="oed-col-product">Ürün</th>
                    <th class="oed-col-qty">Miktar</th>
                    <th class="oed-col-koli">Koli</th>
                    <th class="oed-col-price">Birim Fiyat</th>
                    <th class="oed-col-disc">İndirim %</th>
                    <th class="oed-col-total">Satır Toplamı</th>
                    <th class="oed-col-del"></th>
                </tr>
            </thead>
            <tbody id="~_TableBody">
                <tr><td colspan="7" class="oed-loading">
                    <i class="fa fa-spinner fa-spin"></i>&nbsp;Yükleniyor...
                </td></tr>
            </tbody>
        </table>
    </div>
    <div class="oed-add-row">
        <button id="~_AddBtn" class="btn btn-sm btn-primary oed-add-product-btn">
            <i class="fa fa-shopping-basket"></i>&nbsp;Ürün Ekle
        </button>
    </div>
    <div class="oed-footer">
        <div class="oed-total-wrap">
            <span class="oed-total-label">Genel Toplam</span>
            <strong id="~_Total" class="oed-total-val">0,00 ₺</strong>
        </div>
        <div class="oed-footer-actions">
            <button id="~_DeleteBtn" class="btn btn-danger oed-delete-btn">
                <i class="fa fa-trash"></i>&nbsp;Sil
            </button>
            <button id="~_SaveBtn" class="btn btn-success oed-save-btn">
                <i class="fa fa-save"></i>&nbsp;Kaydet
            </button>
        </div>
    </div>

    <!-- Silme Onay Modali -->
    <div id="~_DeleteModal" class="oed-reason-overlay" style="display:none">
        <div class="oed-reason-box" style="max-width:400px">
            <div class="oed-reason-head">
                <span class="oed-reason-title"><i class="fa fa-trash"></i>&nbsp;Siparişi Sil</span>
                <button id="~_DeleteModalClose" class="oed-reason-close"><i class="fa fa-times"></i></button>
            </div>
            <div class="oed-reason-body" style="padding:20px 18px;font-size:14px;color:#555;">
                Bu siparişi silmek istediğinizden emin misiniz?<br/>
                <strong style="color:#dc3545;">Bu işlem geri alınamaz.</strong>
            </div>
            <div class="oed-reason-foot">
                <button id="~_DeleteModalCancel" class="btn btn-default">
                    <i class="fa fa-times"></i>&nbsp;Vazgeç
                </button>
                <button id="~_DeleteModalConfirm" class="btn btn-danger">
                    <i class="fa fa-trash"></i>&nbsp;Evet, Sil
                </button>
            </div>
        </div>
    </div>

    <div id="~_ReasonModal" class="oed-reason-overlay" style="display:none">
        <div class="oed-reason-box">
            <div class="oed-reason-head">
                <span id="~_ReasonModalTitle" class="oed-reason-title"></span>
                <button id="~_ReasonModalClose" class="oed-reason-close"><i class="fa fa-times"></i></button>
            </div>
            <div id="~_ReasonModalBody" class="oed-status-modal-body"></div>
            <div class="oed-reason-foot">
                <button id="~_ReasonModalCancel" class="btn btn-default">
                    <i class="fa fa-times"></i>&nbsp;İptal
                </button>
                <button id="~_ReasonModalConfirm" class="btn btn-primary">
                    <i class="fa fa-check"></i>&nbsp;Onayla
                </button>
            </div>
        </div>
    </div>

    <!-- Upload Modali -->
    <div id="~_UploadModal" class="oed-reason-overlay" style="display:none">
        <div class="oed-reason-box oed-upload-box">
            <div class="oed-reason-head">
                <span class="oed-reason-title"><i class="fa fa-upload"></i>&nbsp;Dekont Yükle</span>
                <button id="~_UploadClose" class="oed-reason-close"><i class="fa fa-times"></i></button>
            </div>
            <div class="oed-upload-body">
                <label id="~_UploadDropzone" class="oed-upload-dropzone">
                    <i class="fa fa-cloud-upload oed-upload-icon"></i>
                    <span class="oed-upload-text">PDF veya resim seçin</span>
                    <span class="oed-upload-sub">JPG · PNG · PDF · maks. 10 MB</span>
                    <input id="~_UploadInput" type="file" accept="image/jpeg,image/png,image/webp,application/pdf"
                           class="oed-upload-input" />
                </label>
                <div id="~_UploadPreview" class="oed-upload-preview" style="display:none">
                    <i class="fa fa-file oed-preview-icon"></i>
                    <span id="~_UploadFileName" class="oed-preview-name"></span>
                    <button id="~_UploadClear" class="oed-preview-clear" title="Kaldır">
                        <i class="fa fa-times"></i>
                    </button>
                </div>
            </div>
            <div class="oed-reason-foot">
                <button id="~_UploadCancel" class="btn btn-default">İptal</button>
                <button id="~_UploadSubmit" class="btn btn-primary" disabled>
                    <i class="fa fa-upload"></i>&nbsp;Yükle ve Kaydet
                </button>
            </div>
        </div>
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
        this.statusFlowEl       = n('StatusFlow');
        this.tableBodyEl        = n('TableBody');
        this.totalEl            = n('Total');
        this.warehouseSelectEl  = n('WarehouseSelect') as HTMLSelectElement;
        this.reasonModalEl      = n('ReasonModal');
        this.reasonModalTitleEl = n('ReasonModalTitle');
        this.reasonModalBodyEl  = n('ReasonModalBody');

        // Upload modal
        this.uploadModalEl    = n('UploadModal');
        this.uploadInputEl    = n('UploadInput') as HTMLInputElement;
        this.uploadPreviewEl  = n('UploadPreview');
        this.uploadFileNameEl = n('UploadFileName');
        this.uploadSubmitEl   = n('UploadSubmit') as HTMLButtonElement;

        this.uploadInputEl.addEventListener('change', () => this.onFileSelected());
        n('UploadClose').addEventListener('click',  () => this.closeUploadModal());
        n('UploadCancel').addEventListener('click', () => this.closeUploadModal());
        n('UploadClear').addEventListener('click',  () => this.clearFile());
        this.uploadSubmitEl.addEventListener('click', () => this.submitUpload());

        // Popup'ı document.body'ye ekle (template dışı ID sorununu önler)
        this.stepPopupEl = document.createElement('div');
        this.stepPopupEl.className = 'oed-step-popup';
        this.stepPopupEl.style.display = 'none';
        document.body.appendChild(this.stepPopupEl);

        n('AddBtn').addEventListener('click', () => this.openProductPicker());
        n('SaveBtn').addEventListener('click', () => this.saveOrder());
        n('DeleteBtn').addEventListener('click', () => this.openDeleteModal());
        n('DeleteModalClose').addEventListener('click', () => this.closeDeleteModal());
        n('DeleteModalCancel').addEventListener('click', () => this.closeDeleteModal());
        n('DeleteModalConfirm').addEventListener('click', () => this.confirmDelete());
        n('ReasonModalClose').addEventListener('click', () => this.closeReasonModal());
        n('ReasonModalCancel').addEventListener('click', () => this.closeReasonModal());
        n('ReasonModalConfirm').addEventListener('click', () => this.confirmTransition());
    }

    private async loadData(): Promise<void> {
        try {
            [this.productLookup, this.warehouseLookup] = await Promise.all([
                getLookupAsync<ProductsRow>(ProductsRow.lookupKey),
                getLookupAsync<WarehousesRow>(WarehousesRow.lookupKey)
            ]);
            this.populateWarehouseSelect();

            const resp = await OrderService.Retrieve({ EntityId: this.entityId });
            this.order = resp.Entity;
            if (!this.order) { notifyError('Sipariş bulunamadı.'); return; }

            this.rows = this.order.DetailList ? [...this.order.DetailList] : [];
            if (this.order.WarehouseId)
                this.warehouseSelectEl.value = String(this.order.WarehouseId);

            this.renderHeaderGrid();
            this.renderTable();
            await this.loadTransitions();
        } catch (err: any) {
            notifyError('Sipariş yüklenemedi: ' + (err?.message || ''));
        }
    }

    private async loadTransitions(): Promise<void> {
        try {
            const resp = await OrderService.GetAllowedTransitions({ OrderId: this.entityId });
            this.transitions = resp?.Transitions ?? [];
        } catch (err: any) {
            this.transitions = [];
            notifyError('Durum izinleri alınamadı: ' + (err?.message || String(err)));
        }
        this.renderStatusFlow();
        this.bindStepClicks();
    }

    /** Event delegation — innerHTML değişse bile çalışır */
    private bindStepClicks(): void {
        const allowedStatuses = new Set(this.transitions.map(t => t.Status));

        this.statusFlowEl.onclick = (e: MouseEvent) => {
            e.stopPropagation(); // document'a kabarmayı önle → outsideClickHandler tetiklenmesin

            // Rozete tıklandıysa sebebi göster
            const dot = (e.target as HTMLElement).closest<HTMLElement>('.oed-ps-reason-dot');
            if (dot) { this.showReasonPopup(dot); return; }

            const step = (e.target as HTMLElement).closest<HTMLElement>('.oed-ps-interactive');
            if (!step) return;
            const stepNo  = parseInt(step.dataset.step!);
            // O adım için tanımlı aksiyonları göster; isView olanlar her zaman gösterilir
            const actions = (STEP_ACTIONS[stepNo] ?? [])
                .filter(a => a.isView || allowedStatuses.size === 0 || allowedStatuses.has(a.status));
            if (actions.length > 0) this.showStepPopup(step, actions);
        };
    }

    public destroy(): void {
        this.hideStepPopup();
        if (this.stepPopupEl?.parentNode)
            this.stepPopupEl.parentNode.removeChild(this.stepPopupEl);
        super.destroy();
    }

    // ── Header ────────────────────────────────────────────────────
    private renderHeaderGrid(): void {
        const grid = this.byId('HeaderGrid')?.getNode() as HTMLElement;
        if (!grid || !this.order) return;
        const o = this.order;
        grid.innerHTML = `
<div class="oed-hfield">
    <span class="oed-hlabel">Sipariş No</span>
    <span class="oed-hval">${htmlEncode(o.OrderNumber || '—')}</span>
</div>
<div class="oed-hfield">
    <span class="oed-hlabel">Müşteri</span>
    <span class="oed-hval">${htmlEncode(o.CustomerName || '—')}</span>
</div>
<div class="oed-hfield">
    <span class="oed-hlabel">Tarih</span>
    <span class="oed-hval">${o.OrderDate ? new Date(o.OrderDate).toLocaleDateString('tr-TR') : '—'}</span>
</div>
<div class="oed-hfield">
    <span class="oed-hlabel">Net Tutar</span>
    <span class="oed-hval oed-net-amount">${this.fmt(o.NetAmount ?? 0)}&nbsp;₺</span>
</div>`;
    }

    // ── İlerleme çubuğu ──────────────────────────────────────────
    private renderStatusFlow(): void {
        if (!this.statusFlowEl || !this.order) return;

        const status = this.order.Status as number ?? 0;
        const states = getStepStates(status);

        if (states[0] === 'cancel') {
            const reason = this.order.RejectReason?.trim();
            this.statusFlowEl.innerHTML = `
<div class="oed-progress-cancelled">
    <i class="fa fa-ban"></i>&nbsp;Sipariş İptal Edildi
    ${reason ? `<span class="oed-cancel-reason">"${htmlEncode(reason)}"</span>` : ''}
    <button class="btn btn-sm oed-restore-btn" id="oed-restore-btn">
        <i class="fa fa-undo"></i>&nbsp;Geri Al
    </button>
</div>`;
            this.statusFlowEl.querySelector('#oed-restore-btn')
                ?.addEventListener('click', () => this.restoreOrder());
            return;
        }

        let html = '<div class="oed-progress">';

        STEPS.forEach((step, idx) => {
            const state      = states[idx];
            const isLast     = idx === STEPS.length - 1;
            const stepNo     = idx + 1;
            const errMsg     = state === 'error'
                ? (STEP_ERR_MSG[stepNo]?.[status] ?? 'Hata') : '';

            const circleIcon = state === 'done'  ? 'fa-check'
                             : state === 'error' ? 'fa-times'
                             : step.icon;

            // Duruma göre adım etiketi
            const stepLabel = (stepNo === 1 && status === 14) ? 'Talep Beklemede'
                            : (stepNo === 3 && status === 11) ? 'Dekont Bekleniyor'
                            : step.label.replace('\n', ' ');

            const isInteractive = state !== 'cancel';

            // Sebep rozeti: bu adım mevcut durumun "neden" adımıysa göster
            const reason     = this.order?.RejectReason?.trim() ?? '';
            const showBadge  = reason && STATUS_REASON_STEP[status] === stepNo;

            html += `
<div class="oed-ps oed-ps-${state}${isInteractive ? ' oed-ps-interactive' : ''}"
     data-step="${stepNo}"
     ${isInteractive ? 'role="button" tabindex="0"' : ''}>
    <div class="oed-ps-circle" style="position:relative">
        <i class="fa ${circleIcon}"></i>
        ${showBadge ? `<span class="oed-ps-reason-dot" data-reason="${htmlEncode(reason)}" title="${htmlEncode(reason)}">!</span>` : ''}
    </div>
    <div class="oed-ps-label">${htmlEncode(stepLabel)}</div>
    ${errMsg ? `<div class="oed-ps-errnote">${htmlEncode(errMsg)}</div>` : ''}
</div>`;

            if (!isLast) {
                // Sadece 'done' adımlardan sonraki çizgi yeşil; aktif/bekleyen adımdan sonrası gri
                const lineClass = state === 'done'   ? 'oed-pl-done'
                                : state === 'error'  ? 'oed-pl-error'
                                : 'oed-pl-pending';
                html += `<div class="oed-ps-line ${lineClass}"></div>`;
            }
        });

        html += '</div>';
        this.statusFlowEl.innerHTML = html;
    }

    private async viewDekont(): Promise<void> {
        try {
            const resp = await OrderDocumentService.List({
                EqualityFilter: { OrderId: String(this.entityId) },
                Sort: ['-Id']
            });
            const doc = resp?.Entities?.find(d => d.DocumentType === 1 && d.IsActive !== false);
            if (!doc?.FilePath) { notifyError('Dekont bulunamadı.'); return; }
            window.open('/' + doc.FilePath, '_blank');
        } catch (err: any) {
            notifyError('Dekont açılamadı: ' + (err?.message || ''));
        }
    }

    // ── Adım popup'ı ─────────────────────────────────────────────
    private showStepPopup(stepEl: HTMLElement, actions: StepAction[]): void {
        if (!this.stepPopupEl) return;
        this.hideStepPopup(); // eski handler varsa temizle

        this.stepPopupEl.innerHTML = actions.map(a => {
            const req = a.requiresReason ? ' <span class="oed-spp-req">*</span>' : '';
            return `<button class="oed-spp-btn oed-spp-${a.cls}" data-status="${a.status}">
                <i class="fa ${a.icon}"></i>&nbsp;${htmlEncode(a.label)}${req}
            </button>`;
        }).join('');

        const rect = stepEl.getBoundingClientRect();
        const pw   = 200;
        const left = Math.max(8, Math.min(rect.left + rect.width / 2 - pw / 2, window.innerWidth - pw - 8));
        this.stepPopupEl.style.cssText =
            `display:flex; flex-direction:column; gap:3px;
             position:fixed; top:${rect.bottom + 8}px; left:${left}px; width:${pw}px; z-index:9999`;

        this.stepPopupEl.querySelectorAll<HTMLButtonElement>('.oed-spp-btn').forEach(btn => {
            btn.addEventListener('click', e => {
                e.stopPropagation();
                const status = parseInt(btn.dataset.status!);
                const a = actions.find(x => x.status === status);
                if (a) { this.hideStepPopup(); this.executeStepAction(a); }
            });
        });

        setTimeout(() => {
            this.outsideClickHandler = (e: MouseEvent) => {
                if (!this.stepPopupEl.contains(e.target as Node)) this.hideStepPopup();
            };
            document.addEventListener('click', this.outsideClickHandler);
        }, 0);
    }

    private showReasonPopup(dot: HTMLElement): void {
        if (!this.stepPopupEl) return;
        this.hideStepPopup();
        const reason = dot.dataset.reason ?? '';
        this.stepPopupEl.innerHTML = `
<div class="oed-reason-popup-row">
    <i class="fa fa-exclamation-triangle oed-reason-popup-icon"></i>
    <span class="oed-reason-popup-text">${htmlEncode(reason)}</span>
</div>`;
        const rect = dot.getBoundingClientRect();
        const pw   = 240;
        const left = Math.max(8, Math.min(rect.left + rect.width / 2 - pw / 2, window.innerWidth - pw - 8));
        this.stepPopupEl.style.cssText =
            `display:flex; position:fixed; top:${rect.bottom + 6}px; left:${left}px;
             width:${pw}px; z-index:9999; padding:10px 12px;`;
        setTimeout(() => {
            this.outsideClickHandler = (e: MouseEvent) => {
                if (!this.stepPopupEl.contains(e.target as Node)) this.hideStepPopup();
            };
            document.addEventListener('click', this.outsideClickHandler);
        }, 0);
    }

    private hideStepPopup(): void {
        if (!this.stepPopupEl) return;
        this.stepPopupEl.style.display = 'none';
        if (this.outsideClickHandler) {
            document.removeEventListener('click', this.outsideClickHandler);
            this.outsideClickHandler = null;
        }
    }

    private async restoreOrder(): Promise<void> {
        this.pendingTransition = { Status: 1, Label: 'Talep Gönderildi', RequiresReason: false };
        await this.executeTransition(null);
    }

    // ── Geçiş mantığı ────────────────────────────────────────────
    private executeStepAction(a: StepAction): void {
        if (a.isUpload) { this.openUploadModal(); return; }
        if (a.isView)   { this.viewDekont();       return; }
        this.pendingTransition = {
            Status:         a.status,
            Label:          a.label,
            RequiresReason: a.requiresReason ?? false
        };
        if (a.requiresReason) {
            this.openReasonModal(a.label, a.icon);
        } else {
            this.executeTransition(null);
        }
    }

    // ── Upload modal ──────────────────────────────────────────────
    private openUploadModal(): void {
        this.clearFile();
        this.uploadModalEl.style.display = 'flex';
    }

    private closeUploadModal(): void {
        this.uploadModalEl.style.display = 'none';
        this.clearFile();
    }

    private clearFile(): void {
        this.selectedFile = null;
        this.uploadInputEl.value = '';
        this.uploadPreviewEl.style.display  = 'none';
        this.uploadSubmitEl.disabled        = true;
        const dz = this.uploadModalEl.querySelector<HTMLElement>('.oed-upload-dropzone');
        if (dz) dz.style.display = '';
    }

    private onFileSelected(): void {
        const file = this.uploadInputEl.files?.[0];
        if (!file) return;

        const allowed = ['image/jpeg', 'image/png', 'image/webp', 'application/pdf'];
        if (!allowed.includes(file.type)) {
            notifyError('Sadece PDF, JPG veya PNG yükleyebilirsiniz.');
            this.uploadInputEl.value = '';
            return;
        }
        if (file.size > 10 * 1024 * 1024) {
            notifyError('Dosya boyutu 10 MB\'ı geçemez.');
            this.uploadInputEl.value = '';
            return;
        }

        this.selectedFile = file;

        // İkon: PDF mi resim mi?
        const isPdf = file.type === 'application/pdf';
        const icon  = isPdf ? 'fa-file-pdf-o' : 'fa-file-image-o';

        this.uploadFileNameEl.textContent    = file.name;
        this.uploadPreviewEl.querySelector('.oed-preview-icon')
            ?.setAttribute('class', `fa ${icon} oed-preview-icon`);
        this.uploadPreviewEl.style.display   = '';
        this.uploadModalEl.querySelector<HTMLElement>('.oed-upload-dropzone')!
            .style.display = 'none';
        this.uploadSubmitEl.disabled = false;
    }

    private async submitUpload(): Promise<void> {
        if (!this.selectedFile) return;

        this.uploadSubmitEl.disabled = true;
        this.uploadSubmitEl.innerHTML = '<i class="fa fa-spinner fa-spin"></i>&nbsp;Yükleniyor...';

        try {
            const base64 = await this.toBase64(this.selectedFile);

            const csrf = document.cookie.match(/(?:^|;\s*)CSRF-TOKEN=([^;]*)/)?.[1] ?? '';
            const resp = await fetch('/Services/Order/Order/UploadDekont', {
                method:  'POST',
                headers: {
                    'Content-Type':     'application/json',
                    'X-Requested-With': 'XMLHttpRequest',
                    'X-CSRF-TOKEN':     decodeURIComponent(csrf)
                },
                body:    JSON.stringify({
                    OrderId:    this.entityId,
                    FileName:   this.selectedFile.name,
                    FileBase64: base64,
                    MimeType:   this.selectedFile.type
                })
            });

            const text = await resp.text();
            const json = text ? JSON.parse(text) : {};
            if (!resp.ok || json?.Error)
                throw new Error(json?.Error?.Message ?? `HTTP ${resp.status}`);

            notifySuccess('Dekont başarıyla yüklendi!');
            this.closeUploadModal();
            this.order!.Status = 5 as any;  // DEKONT_YUKLENDI
            this.renderHeaderGrid();
            this.options?.onSave?.();
            await this.loadTransitions();
        } catch (err: any) {
            notifyError('Yükleme başarısız: ' + (err?.message || ''));
            this.uploadSubmitEl.disabled = false;
            this.uploadSubmitEl.innerHTML = '<i class="fa fa-upload"></i>&nbsp;Yükle ve Kaydet';
        }
    }

    private toBase64(file: File): Promise<string> {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload  = () => resolve((reader.result as string).split(',')[1]);
            reader.onerror = reject;
            reader.readAsDataURL(file);
        });
    }

    private openReasonModal(label: string, icon: string): void {
        this.reasonModalTitleEl.innerHTML =
            `<i class="fa ${icon}"></i>&nbsp;${htmlEncode(label)}`;
        this.reasonModalBodyEl.innerHTML = `
<label class="oed-reason-lbl">Açıklama / Neden <span class="text-danger">*</span></label>
<textarea class="form-control oed-reason-textarea" rows="3"
          placeholder="Lütfen açıklama giriniz..."></textarea>`;
        this.reasonTextEl = this.reasonModalBodyEl.querySelector('textarea');
        this.reasonModalEl.style.display = 'flex';
        setTimeout(() => this.reasonTextEl?.focus(), 50);
    }

    private closeReasonModal(): void {
        this.reasonModalEl.style.display = 'none';
        this.pendingTransition = null;
        this.reasonTextEl = null;
    }

    private confirmTransition(): void {
        const reason = this.reasonTextEl?.value?.trim();
        if (!reason) {
            notifyWarning('Lütfen açıklama giriniz.');
            this.reasonTextEl?.focus();
            return;
        }
        this.reasonModalEl.style.display = 'none';
        this.reasonTextEl = null;
        this.executeTransition(reason);
    }

    private async executeTransition(reason: string | null): Promise<void> {
        if (!this.pendingTransition || !this.order) return;
        const t = this.pendingTransition;
        this.pendingTransition = null;

        this.statusFlowEl.querySelectorAll<HTMLElement>('.oed-ps-interactive')
            .forEach(el => el.classList.add('oed-ps-loading'));

        try {
            await OrderService.Update({
                EntityId: this.entityId,
                Entity: {
                    ...this.order,
                    Status:       t.Status as any,
                    RejectReason: reason ?? undefined,
                    DetailList:   undefined
                }
            });
            notifySuccess(`Durum "${t.Label}" olarak güncellendi.`);
            this.order.Status = t.Status as any;
            if (reason) this.order.RejectReason = reason;
            this.renderHeaderGrid();
            this.options?.onSave?.();
            await this.loadTransitions();
        } catch (err: any) {
            notifyError('Durum değiştirilemedi: ' + (err?.message || ''));
            this.statusFlowEl.querySelectorAll<HTMLElement>('.oed-ps-loading')
                .forEach(el => el.classList.remove('oed-ps-loading'));
        }
    }

    // ── Ürün tablosu ─────────────────────────────────────────────
    private populateWarehouseSelect(): void {
        const sel = this.warehouseSelectEl;
        if (!sel) return;
        this.warehouseLookup.items
            .filter(w => w.IsActive !== false)
            .sort((a, b) => (a.Name || '').localeCompare(b.Name || '', 'tr'))
            .forEach(w => {
                const opt = document.createElement('option');
                opt.value = String(w.Id);
                opt.textContent = w.Name || '';
                sel.appendChild(opt);
            });
    }

    private openProductPicker(): void {
        new OrderDialog({
            onProductsSelected: items => this.mergePickedProducts(items),
            preSelectedCustomerId: this.order?.CustomerId ?? undefined
        }).dialogOpen();
    }

    private mergePickedProducts(items: OrderDetailRow[]): void {
        for (const item of items) {
            const base = (item.Quantity ?? 0) * (item.UnitPrice ?? 0);
            const discRate = base > 0 ? ((item.Discount ?? 0) / base) * 100 : 0;
            const existing = this.rows.find(r => r.ProductId === item.ProductId);
            if (existing) {
                existing.Quantity  = (existing.Quantity ?? 0) + (item.Quantity ?? 0);
                existing.LineTotal = existing.Quantity * (existing.UnitPrice ?? 0) * (1 - (existing.Discount ?? 0) / 100);
            } else {
                this.rows.push({ ...item, Discount: discRate });
            }
        }
        this.renderTable();
    }

    private renderTable(): void {
        if (!this.tableBodyEl) return;
        if (this.rows.length === 0) {
            this.tableBodyEl.innerHTML =
                '<tr><td colspan="7" class="oed-empty">Sipariş kalemi yok.</td></tr>';
            this.updateTotal();
            return;
        }
        let html = '';
        this.rows.forEach((row, idx) => {
            const p          = row.ProductId ? this.productLookup?.itemById[row.ProductId] : null;
            const name       = p ? `${p.Code || ''} - ${p.Name || ''}` : (row.ProductCodeName || '—');
            const packingQty = ((p as any)?.PackingQuantity as number) || 1;
            const qty   = row.Quantity ?? 1, price = row.UnitPrice ?? 0,
                  disc  = row.Discount ?? 0, total = row.LineTotal ?? qty * price * (1 - disc / 100);
            const boxCount   = packingQty > 1 ? Math.round(qty / packingQty) : qty;
            html += `
<tr data-idx="${idx}">
    <td class="oed-col-product">${htmlEncode(name)}</td>
    <td class="oed-col-qty">
        <input type="number" class="form-control form-control-sm oed-num oed-qty"
               data-idx="${idx}" value="${qty}" min="0.001" step="1" />
    </td>
    <td class="oed-col-koli">
        <input type="number" class="form-control form-control-sm oed-num oed-koli"
               data-idx="${idx}" data-packing="${packingQty}" value="${boxCount}" min="1" step="1" />
        ${packingQty > 1 ? `<small class="oed-koli-hint">${packingQty} adet/koli</small>` : ''}
    </td>
    <td class="oed-col-price">
        <input type="number" class="form-control form-control-sm oed-num oed-price"
               data-idx="${idx}" value="${price}" min="0" step="0.01" />
    </td>
    <td class="oed-col-disc">
        <input type="number" class="form-control form-control-sm oed-num oed-disc"
               data-idx="${idx}" value="${disc}" min="0" max="100" step="0.01" />
    </td>
    <td class="oed-col-total oed-line-total" data-idx="${idx}">${this.fmt(total)}&nbsp;₺</td>
    <td class="oed-col-del">
        <button class="btn btn-sm btn-link oed-del-btn" data-idx="${idx}">
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
        tb.querySelectorAll<HTMLInputElement>('.oed-qty').forEach(inp =>
            inp.addEventListener('input', () => {
                const idx = parseInt(inp.dataset.idx!);
                // Koli inputunu güncelle
                const koliInp = tb.querySelector<HTMLInputElement>(`.oed-koli[data-idx="${idx}"]`);
                if (koliInp) {
                    const packing = parseInt(koliInp.dataset.packing ?? '1') || 1;
                    koliInp.value = String(Math.max(1, Math.round((parseFloat(inp.value) || 0) / packing)));
                }
                this.recalcRow(idx);
            })
        );
        tb.querySelectorAll<HTMLInputElement>('.oed-koli').forEach(inp =>
            inp.addEventListener('input', () => {
                const idx     = parseInt(inp.dataset.idx!);
                const packing = parseInt(inp.dataset.packing ?? '1') || 1;
                const qtyInp  = tb.querySelector<HTMLInputElement>(`.oed-qty[data-idx="${idx}"]`);
                if (qtyInp) {
                    qtyInp.value = String((parseFloat(inp.value) || 1) * packing);
                }
                this.recalcRow(idx);
            })
        );
        tb.querySelectorAll<HTMLInputElement>('.oed-price, .oed-disc').forEach(inp =>
            inp.addEventListener('input', () => this.recalcRow(parseInt(inp.dataset.idx!)))
        );
        tb.querySelectorAll<HTMLButtonElement>('.oed-del-btn').forEach(btn =>
            btn.addEventListener('click', () => { this.rows.splice(parseInt(btn.dataset.idx!), 1); this.renderTable(); })
        );
    }

    private recalcRow(idx: number): void {
        const tb  = this.tableBodyEl;
        const row = this.rows[idx];
        if (!row) return;
        const qty   = parseFloat(tb.querySelector<HTMLInputElement>(`.oed-qty[data-idx="${idx}"]`)?.value   || '0') || 0;
        const price = parseFloat(tb.querySelector<HTMLInputElement>(`.oed-price[data-idx="${idx}"]`)?.value || '0') || 0;
        const disc  = parseFloat(tb.querySelector<HTMLInputElement>(`.oed-disc[data-idx="${idx}"]`)?.value  || '0') || 0;
        row.Quantity = qty; row.UnitPrice = price; row.Discount = disc;
        row.LineTotal = qty * price * (1 - disc / 100);
        const cell = tb.querySelector<HTMLElement>(`.oed-line-total[data-idx="${idx}"]`);
        if (cell) cell.textContent = this.fmt(row.LineTotal) + ' ₺';
        this.updateTotal();
    }

    private updateTotal(): void {
        const total = this.rows.reduce((s, r) => s + (r.LineTotal ?? 0), 0);
        if (this.totalEl) this.totalEl.textContent = this.fmt(total) + ' ₺';
    }

    private openDeleteModal(): void {
        (this.byId('DeleteModal')?.getNode() as HTMLElement).style.display = 'flex';
    }

    private closeDeleteModal(): void {
        (this.byId('DeleteModal')?.getNode() as HTMLElement).style.display = 'none';
    }

    private async confirmDelete(): Promise<void> {
        this.closeDeleteModal();
        const confirmBtn = this.byId('DeleteModalConfirm')?.getNode() as HTMLButtonElement;
        if (confirmBtn) { confirmBtn.disabled = true; confirmBtn.innerHTML = '<i class="fa fa-spinner fa-spin"></i>&nbsp;Siliniyor...'; }
        try {
            await OrderService.Delete({ EntityId: this.entityId });
            notifySuccess('Sipariş başarıyla silindi.');
            this.options?.onSave?.();
            this.dialogClose();
        } catch (err: any) {
            notifyError('Sipariş silinemedi: ' + (err?.message || ''));
            if (confirmBtn) { confirmBtn.disabled = false; confirmBtn.innerHTML = '<i class="fa fa-trash"></i>&nbsp;Evet, Sil'; }
        }
    }

    private async saveOrder(): Promise<void> {
        if (!this.order) return;
        if (this.rows.length === 0) { notifyWarning('Sipariş kalemlerini doldurun.'); return; }
        const total = this.rows.reduce((s, r) => s + (r.LineTotal ?? 0), 0);
        const wid   = this.warehouseSelectEl?.value ? parseInt(this.warehouseSelectEl.value) : undefined;
        try {
            await OrderService.Update({
                EntityId: this.entityId,
                Entity: { ...this.order, WarehouseId: wid, TotalAmount: total, NetAmount: total, DetailList: this.rows }
            });
            notifySuccess('Sipariş başarıyla güncellendi!');
            this.options?.onSave?.();
            this.dialogClose();
        } catch (err: any) {
            notifyError('Güncelleme sırasında hata: ' + (err?.message || ''));
        }
    }

    private fmt(n: number): string {
        return new Intl.NumberFormat('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
    }

    protected getDialogButtons(): DialogButton[] { return []; }
    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 980;
        return opt;
    }
}
