import {
    Decorators, DialogButton, Lookup, TemplatedDialog,
    getLookupAsync, htmlEncode, notifyError, notifySuccess, notifyWarning
} from '@serenity-is/corelib';
import { ProductCategoryRow, ProductsRow } from '../../ServerTypes/Catalog';
import { CustomersRow } from '../../ServerTypes/Customer';
import { OrderDetailRow, OrderDetailService, OrderRow, OrderService } from '../../ServerTypes/Order';
import { VendorTypeRow } from '../../ServerTypes/Setting';

export interface OrderDialogOptions {
    entityId?: number | null;
    onSave?: () => void;
}

interface CartItem {
    productId: number;
    productCode: string;
    productName: string;
    quantity: number;
    unitId?: number;
    unitName?: string;
    unitPrice: number;
    vatRateId?: number;
    vatRate?: number;
    discount: number;
    lineTotal: number;
}

@Decorators.panel(true)
@Decorators.registerClass('SYP.Order.OrderDialog')
export class OrderDialog extends TemplatedDialog<OrderDialogOptions> {
    private entityId: number | null = null;
    private cart = new Map<number, CartItem>();
    private allProducts: ProductsRow[] = [];
    private productLookup!: Lookup<ProductsRow>;
    private customerLookup!: Lookup<CustomersRow>;
    private vendorTypeLookup!: Lookup<VendorTypeRow>;
    private categoryLookup!: Lookup<ProductCategoryRow>;
    private selectedCategoryId: number | null = null;
    private searchTerm = '';
    private customerId: number | null = null;
    private cartVisible = false;

    private catListEl!: HTMLElement;
    private productGridEl!: HTMLElement;
    private searchEl!: HTMLInputElement;
    private cartPanelEl!: HTMLElement;
    private cartItemsEl!: HTMLElement;
    private cartTotalEl!: HTMLElement;
    private cartBadgeEl!: HTMLElement;

    constructor(props?: OrderDialogOptions) {
        super(props);
        this.entityId = props?.entityId ?? null;
        this.dialogTitle = this.entityId ? 'Sipariş Düzenle' : 'Sipariş Oluştur';
    }

    protected getTemplate(): string {
        return `
<div class="opd-root">
    <div class="opd-topbar">
        <div class="opd-search-wrap">
            <i class="fa fa-search opd-search-icon"></i>
            <input id="~_SearchInput" type="text" class="form-control form-control-sm"
                   placeholder="Ürün adı veya kodu ile ara..." />
        </div>
        <button id="~_CartToggle" class="btn opd-cart-btn">
            <i class="fa fa-shopping-cart"></i>
            <span>Sepet</span>
            <span class="opd-cart-badge" id="~_CartBadge">0</span>
        </button>
    </div>
    <div class="opd-body">
        <aside class="opd-sidebar">
            <div class="opd-sidebar-head">Kategoriler</div>
            <div id="~_CategoryList" class="opd-cat-list"></div>
        </aside>
        <main class="opd-main">
            <div id="~_ProductGrid" class="opd-product-grid">
                <div class="opd-loading">
                    <i class="fa fa-spinner fa-spin"></i>&nbsp;Ürünler yükleniyor...
                </div>
            </div>
        </main>
        <aside id="~_CartPanel" class="opd-cart-panel opd-cart-closed">
            <div class="opd-cart-head">
                <span><i class="fa fa-shopping-cart"></i>&nbsp;Sepetim</span>
                <button id="~_CloseCart" class="btn btn-link opd-close-cart">
                    <i class="fa fa-times"></i>
                </button>
            </div>
            <div id="~_CartItems" class="opd-cart-items">
                <div class="opd-cart-empty">Sepet boş</div>
            </div>
            <div class="opd-cart-foot">
                <div class="opd-cart-total-row">
                    <span>Toplam</span>
                    <strong id="~_CartTotal">0,00 ₺</strong>
                </div>
                <button id="~_CompleteOrder" class="btn opd-complete-btn">
                    <i class="fa fa-check"></i>&nbsp;
                    ${this.entityId ? 'Siparişi Güncelle' : 'Siparişi Tamamla'}
                </button>
            </div>
        </aside>
    </div>
</div>`;
    }

    protected onDialogOpen(): void {
        super.onDialogOpen();
        this.bindElements();
        this.loadData();
    }

    private bindElements(): void {
        const n = (id: string) => this.byId(id)?.getNode() as HTMLElement;

        this.catListEl     = n('CategoryList');
        this.productGridEl = n('ProductGrid');
        this.searchEl      = n('SearchInput') as HTMLInputElement;
        this.cartPanelEl   = n('CartPanel');
        this.cartItemsEl   = n('CartItems');
        this.cartTotalEl   = n('CartTotal');
        this.cartBadgeEl   = n('CartBadge');

        n('CartToggle').addEventListener('click', () => this.toggleCart());
        n('CloseCart').addEventListener('click', () => this.toggleCart(false));
        n('CompleteOrder').addEventListener('click', () => this.completeOrder());

        this.searchEl.addEventListener('input', () => {
            this.searchTerm = this.searchEl.value.toLowerCase().trim();
            this.renderProducts();
        });
    }

    private async loadData(): Promise<void> {
        [this.productLookup, this.customerLookup, this.vendorTypeLookup, this.categoryLookup] = await Promise.all([
            getLookupAsync<ProductsRow>(ProductsRow.lookupKey),
            getLookupAsync<CustomersRow>(CustomersRow.lookupKey),
            getLookupAsync<VendorTypeRow>(VendorTypeRow.lookupKey),
            getLookupAsync<ProductCategoryRow>('Catalog.ProductCategory')
        ]);

        this.allProducts = this.productLookup.items.filter(p => p.IsActive !== 0);
        this.renderCategories();
        this.renderProducts();
        this.tryAutoSelectCustomer();

        if (this.entityId) {
            await this.loadExistingOrder(this.entityId);
        }
    }

    private tryAutoSelectCustomer(): void {
        if (this.entityId || this.customerId) return;

        OrderService.GetCurrentBayiiCustomerId({}).then(
            resp => { if (resp?.CustomerId) this.customerId = resp.CustomerId; },
            () => {}
        );
    }

    private async loadExistingOrder(id: number): Promise<void> {
        try {
            // Sipariş başlığını yükle; MasterDetailRelationBehavior DetailList'i de doldurur
            const orderResp = await OrderService.Retrieve({ EntityId: id });
            const order = orderResp.Entity;
            if (!order) return;

            if (order.CustomerId)
                this.customerId = order.CustomerId;

            // Birincil kaynak: Retrieve yanıtındaki DetailList (MasterDetailRelation)
            let rows = order.DetailList ?? [];

            // Yedek: DetailList boşsa OrderDetail servisini doğrudan sorgula
            if (rows.length === 0) {
                const detailResp = await OrderDetailService.List({
                    EqualityFilter: { OrderId: String(id) }
                });
                rows = detailResp?.Entities ?? [];
            }

            for (const d of rows) {
                if (!d.ProductId) continue;
                const p = this.productLookup?.itemById[d.ProductId];
                this.cart.set(d.ProductId, {
                    productId:   d.ProductId,
                    productCode: p?.Code     ?? '',
                    productName: p?.Name     ?? (d.ProductCodeName ?? ''),
                    quantity:    d.Quantity  ?? 1,
                    unitId:      d.UnitId    ?? p?.UnitId,
                    unitName:    p?.UnitName ?? (d.UnitCode ?? ''),
                    unitPrice:   d.UnitPrice ?? 0,
                    vatRateId:   d.VatRateId ?? p?.VatRateId,
                    vatRate:     d.VatRate   ?? p?.VatRate,
                    discount:    d.Discount  ?? 0,
                    lineTotal:   d.LineTotal ?? 0
                });
            }

            this.updateCartUI();
            this.renderProducts();
            if (this.cart.size > 0) this.toggleCart(true);

        } catch (err: any) {
            notifyError('Sipariş yüklenemedi: ' + (err?.message || ''));
        }
    }

    private renderCategories(): void {
        if (!this.catListEl) return;

        // Ürünlerde kullanılan kategori ID'leri
        const usedIds = new Set(this.allProducts.map(p => p.CategoryId).filter(Boolean) as number[]);

        // Kullanılan kategoriler + tüm üst kategorileri topla
        const visibleCats = new Map<number, ProductCategoryRow>();
        const addWithAncestors = (cat: ProductCategoryRow) => {
            if (!cat.Id || visibleCats.has(cat.Id)) return;
            visibleCats.set(cat.Id, cat);
            if (cat.ParentId) {
                const parent = this.categoryLookup.itemById[cat.ParentId];
                if (parent) addWithAncestors(parent);
            }
        };
        this.categoryLookup.items
            .filter(c => c.IsActive !== false && usedIds.has(c.Id!))
            .forEach(c => addWithAncestors(c));

        // FullPath'e göre sırala → hiyerarşik görünüm
        const sorted = Array.from(visibleCats.values())
            .sort((a, b) => (a.FullPath ?? a.Name ?? '').localeCompare(b.FullPath ?? b.Name ?? '', 'tr'));

        this.catListEl.replaceChildren();

        // "Tüm Ürünler" satırı
        const allItem = document.createElement('div');
        allItem.className = 'opd-cat-item' + (this.selectedCategoryId === null ? ' active' : '');
        allItem.dataset.id = '';
        const allIcon = document.createElement('i');
        allIcon.className = 'fa fa-th-large';
        allItem.append(allIcon, ' Tüm Ürünler');
        this.catListEl.appendChild(allItem);

        sorted.forEach(cat => {
            const depth = (cat.FullPath?.split(' > ').length ?? 1) - 1;
            const hasChildren = sorted.some(c => c.ParentId === cat.Id);

            const el = document.createElement('div');
            el.className = 'opd-cat-item' + (this.selectedCategoryId === cat.Id ? ' active' : '');
            el.dataset.id = String(cat.Id);
            el.style.paddingLeft = (12 + depth * 14) + 'px';

            const icon = document.createElement('i');
            icon.className = hasChildren ? 'fa fa-folder-o' : 'fa fa-tag';
            el.append(icon, ' ' + (cat.Name ?? ''));
            this.catListEl.appendChild(el);
        });

        this.catListEl.querySelectorAll<HTMLElement>('.opd-cat-item').forEach(el => {
            el.addEventListener('click', () => {
                const idStr = el.dataset.id;
                this.selectedCategoryId = idStr ? parseInt(idStr) : null;
                this.catListEl.querySelectorAll('.opd-cat-item').forEach(x => x.classList.remove('active'));
                el.classList.add('active');
                this.renderProducts();
            });
        });
    }

    private renderProducts(): void {
        if (!this.productGridEl) return;

        let products = this.allProducts;

        if (this.selectedCategoryId !== null) {
            const selCat = this.categoryLookup?.itemById[this.selectedCategoryId];
            if (selCat?.FullPath) {
                const prefix = selCat.FullPath;
                const subtreeIds = new Set(
                    this.categoryLookup.items
                        .filter(c => c.FullPath === prefix || c.FullPath?.startsWith(prefix + ' > '))
                        .map(c => c.Id!)
                );
                products = products.filter(p => p.CategoryId != null && subtreeIds.has(p.CategoryId));
            } else {
                products = products.filter(p => p.CategoryId === this.selectedCategoryId);
            }
        }

        if (this.searchTerm) {
            products = products.filter(p =>
                p.Code?.toLowerCase().includes(this.searchTerm) ||
                p.Name?.toLowerCase().includes(this.searchTerm)
            );
        }

        if (products.length === 0) {
            this.productGridEl.innerHTML = '<div class="opd-no-results"><i class="fa fa-search"></i><br>Ürün bulunamadı</div>';
            return;
        }

        let html = '';
        products.forEach(product => {
            const inCart   = this.cart.has(product.Id!);
            const price    = product.CurrentValidPrice ?? product.UnitPrice ?? 0;
            const currency = htmlEncode(product.CurrencyCode || '₺');

            html += `
<div class="opd-card${inCart ? ' opd-card--in-cart' : ''}" data-id="${product.Id}">
    <div class="opd-card-body">
        <div class="opd-card-code">${htmlEncode(product.Code || '')}</div>
        <div class="opd-card-name" title="${htmlEncode(product.Name || '')}">${htmlEncode(product.Name || '')}</div>
        <div class="opd-card-cat">${htmlEncode(product.CategoryName || '')}</div>
        <div class="opd-card-price">${this.fmt(price)}&nbsp;${currency}</div>
        ${product.UnitName ? `<div class="opd-card-unit">${htmlEncode(product.UnitName)}</div>` : ''}
    </div>
    <div class="opd-card-footer">
        <div class="opd-qty-wrap">
            <button class="opd-qty-btn opd-qty-minus" data-id="${product.Id}">−</button>
            <input type="number" class="opd-qty-input" data-id="${product.Id}"
                   value="1" min="0.001" step="1" />
            <button class="opd-qty-btn opd-qty-plus" data-id="${product.Id}">+</button>
        </div>
        <button class="btn opd-add-btn" data-id="${product.Id}">
            <i class="fa fa-plus"></i>&nbsp;Sepete Ekle
        </button>
    </div>
</div>`;
        });

        this.productGridEl.innerHTML = html;
        this.bindProductEvents();
    }

    private bindProductEvents(): void {
        const g = this.productGridEl;

        g.querySelectorAll<HTMLButtonElement>('.opd-qty-minus').forEach(btn => {
            btn.addEventListener('click', e => {
                e.stopPropagation();
                const id    = parseInt(btn.dataset.id!);
                const input = g.querySelector<HTMLInputElement>(`.opd-qty-input[data-id="${id}"]`)!;
                input.value = String(Math.max(0.001, (parseFloat(input.value) || 1) - 1));
            });
        });

        g.querySelectorAll<HTMLButtonElement>('.opd-qty-plus').forEach(btn => {
            btn.addEventListener('click', e => {
                e.stopPropagation();
                const id    = parseInt(btn.dataset.id!);
                const input = g.querySelector<HTMLInputElement>(`.opd-qty-input[data-id="${id}"]`)!;
                input.value = String((parseFloat(input.value) || 1) + 1);
            });
        });

        g.querySelectorAll<HTMLButtonElement>('.opd-add-btn').forEach(btn => {
            btn.addEventListener('click', () => {
                const id    = parseInt(btn.dataset.id!);
                const input = g.querySelector<HTMLInputElement>(`.opd-qty-input[data-id="${id}"]`)!;
                this.addToCart(id, parseFloat(input.value) || 1);
            });
        });
    }

    private addToCart(productId: number, addQty: number): void {
        const p = this.productLookup.itemById[productId];
        if (!p) return;

        const price    = p.CurrentValidPrice ?? p.UnitPrice ?? 0;
        const existing = this.cart.get(productId);
        const quantity = (existing?.quantity ?? 0) + addQty;
        const discount = this.calcDiscount(price, quantity);

        this.cart.set(productId, {
            productId,
            productCode: p.Code    || '',
            productName: p.Name    || '',
            quantity,
            unitId:   p.UnitId,
            unitName: p.UnitName,
            unitPrice: price,
            vatRateId: p.VatRateId,
            vatRate:   p.VatRate,
            discount,
            lineTotal: price * quantity - discount
        });

        this.updateCartUI();
        this.renderProducts();

        if (!this.cartVisible) this.toggleCart(true);
    }

    private removeFromCart(productId: number): void {
        this.cart.delete(productId);
        this.updateCartUI();
        this.renderProducts();
    }

    private calcDiscount(price: number, quantity: number): number {
        if (!this.customerId) return 0;
        const customer = this.customerLookup?.itemById[this.customerId];
        if (!customer?.VendorTypeId) return 0;
        const vt = this.vendorTypeLookup?.itemById[customer.VendorTypeId];
        if (!vt?.DiscountValue) return 0;
        if (vt.DiscountType === 'Percentage' || vt.DiscountType === '%') {
            return (price * quantity * vt.DiscountValue) / 100;
        }
        return vt.DiscountValue;
    }

    private updateCartUI(): void {
        const count = this.cart.size;
        if (this.cartBadgeEl) this.cartBadgeEl.textContent = String(count);

        if (count === 0) {
            if (this.cartItemsEl) this.cartItemsEl.innerHTML = '<div class="opd-cart-empty">Sepet boş</div>';
            if (this.cartTotalEl) this.cartTotalEl.textContent = '0,00 ₺';
            return;
        }

        let total = 0;
        let html  = '';

        this.cart.forEach(item => {
            total += item.lineTotal;
            html  += `
<div class="opd-ci">
    <div class="opd-ci-info">
        <div class="opd-ci-name">${htmlEncode(item.productName)}</div>
        <div class="opd-ci-meta">${htmlEncode(item.productCode)}${item.unitName ? ' · ' + htmlEncode(item.unitName) : ''}</div>
    </div>
    <input type="number" class="opd-cart-qty" data-id="${item.productId}"
           value="${Math.round(item.quantity)}" min="1" step="1" title="Miktar" />
    <div class="opd-ci-price">
        <div>${this.fmt(item.lineTotal)}&nbsp;₺</div>
        <small>${this.fmt(item.unitPrice)}&nbsp;×&nbsp;${item.quantity}</small>
    </div>
    <button class="opd-ci-del" data-id="${item.productId}" title="Kaldır">
        <i class="fa fa-trash-o"></i>
    </button>
</div>`;
        });

        if (this.cartItemsEl) {
            this.cartItemsEl.innerHTML = html;

            this.cartItemsEl.querySelectorAll<HTMLInputElement>('.opd-cart-qty').forEach(inp => {
                inp.addEventListener('change', () => {
                    const id   = parseInt(inp.dataset.id!);
                    const qty  = Math.max(1, parseInt(inp.value, 10) || 1);
                    const item = this.cart.get(id);
                    if (item) {
                        item.quantity  = qty;
                        item.discount  = this.calcDiscount(item.unitPrice, qty);
                        item.lineTotal = item.unitPrice * qty - item.discount;
                        this.updateCartUI();
                        this.renderProducts();
                    }
                });
            });

            this.cartItemsEl.querySelectorAll<HTMLButtonElement>('.opd-ci-del').forEach(btn => {
                btn.addEventListener('click', () => {
                    this.removeFromCart(parseInt(btn.dataset.id!));
                });
            });
        }

        if (this.cartTotalEl) this.cartTotalEl.textContent = this.fmt(total) + ' ₺';
    }

    private toggleCart(show?: boolean): void {
        this.cartVisible = show !== undefined ? show : !this.cartVisible;
        if (this.cartPanelEl) {
            this.cartPanelEl.classList.toggle('opd-cart-closed', !this.cartVisible);
            this.cartPanelEl.classList.toggle('opd-cart-open',   this.cartVisible);
        }
    }

    private async completeOrder(): Promise<void> {
        if (this.cart.size === 0) {
            notifyWarning('Sepet boş! Lütfen en az bir ürün ekleyin.');
            return;
        }

        const detailList: OrderDetailRow[] = Array.from(this.cart.values()).map(item => ({
            ProductId: item.productId,
            Quantity:  item.quantity,
            UnitId:    item.unitId,
            UnitPrice: item.unitPrice,
            VatRateId: item.vatRateId,
            VatRate:   item.vatRate,
            Discount:  item.discount,
            LineTotal: item.lineTotal
        }));

        const totalAmount = detailList.reduce((s, d) => s + (d.LineTotal || 0), 0);

        const order: OrderRow = {
            CustomerId:  this.customerId ?? undefined,
            OrderDate:   new Date().toISOString(),
            Status:      1,
            TotalAmount: totalAmount,
            NetAmount:   totalAmount,
            DetailList:  detailList
        };

        try {
            if (this.entityId) {
                order.Id = this.entityId;
                await OrderService.Update({ EntityId: this.entityId, Entity: order });
                notifySuccess('Sipariş güncellendi!');
            } else {
                await OrderService.Create({ Entity: order });
                notifySuccess('Sipariş başarıyla oluşturuldu!');
            }
            this.options?.onSave?.();
            this.dialogClose();
        } catch (err: any) {
            notifyError('İşlem sırasında hata oluştu: ' + (err?.message || 'Bilinmeyen hata'));
        }
    }

    private fmt(n: number): string {
        return new Intl.NumberFormat('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
    }

    protected getDialogButtons(): DialogButton[] {
        return [];
    }

    protected getDialogOptions() {
        const opt   = super.getDialogOptions();
        opt.width   = 1200;
        return opt;
    }
}
