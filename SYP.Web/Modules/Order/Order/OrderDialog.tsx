import {
    Decorators, DialogButton, Lookup, TemplatedDialog,
    getLookupAsync, htmlEncode, localText, notifyError, notifySuccess, notifyWarning
} from '@serenity-is/corelib';
import { BrandsRow, PriceListItemsRow, PriceListItemsService, PriceListsRow, ProductCategoryRow, ProductsRow } from '../../ServerTypes/Catalog';
import { CustomersRow } from '../../ServerTypes/Customer';
import { OrderDetailRow, OrderDetailService, OrderRow, OrderService } from '../../ServerTypes/Order';
import { CurrencyListRow, VendorTypeRow } from '../../ServerTypes/Setting';
import { PermissionKeys } from '../../ServerTypes/Administration';
import { hasPermission } from '../../Administration/User/Authentication/Authorization';

export interface OrderDialogOptions {
    entityId?: number | null;
    onSave?: () => void;
    /** Picker modu: kaydetmek yerine seçilen ürünleri geri döndürür */
    onProductsSelected?: (items: OrderDetailRow[]) => void;
    /** Picker modunda müşteri fiyat listesi için müşteri ID'si */
    preSelectedCustomerId?: number;
}

interface CartItem {
    productId: number;
    productCode: string;
    productName: string;
    quantity: number;    // koli sayısı
    packingQty: number;  // koli içi adet
    unitId?: number;
    unitName?: string;
    unitPrice: number;
    vatRateId?: number;
    vatRate?: number;
    discount: number;
    lineTotal: number;   // koli × packingQty × unitPrice - discount
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
    private brandLookup!: Lookup<BrandsRow>;
    private selectedCategoryId: number | null = null;
    private expandedCategories = new Set<number>();
    private selectedBrandIds = new Set<number>();
    private searchTerm = '';
    private customerId: number | null = null;
    private priceListItems = new Map<number, PriceListItemsRow>();
    private activeCurrencyCode = '₺';
    private activeCurrencyId: number | undefined = undefined;
    private cartVisible = false;
    /** Bayi olmayan kullanıcılar için cari seçimi zorunlu */
    private requireCustomerSelection = false;

    private imgTooltipEl!: HTMLElement;
    private customerWrapEl!: HTMLElement;
    private customerSelectEl!: HTMLSelectElement;
    private catListEl!: HTMLElement;
    private brandSectionEl!: HTMLElement;
    private brandListEl!: HTMLElement;
    private productGridEl!: HTMLElement;
    private searchEl!: HTMLInputElement;
    private cartPanelEl!: HTMLElement;
    private cartItemsEl!: HTMLElement;
    private cartTotalEl!: HTMLElement;
    private cartBadgeEl!: HTMLElement;

    constructor(props?: OrderDialogOptions) {
        super(props);
        this.entityId = props?.entityId ?? null;
        this.dialogTitle = this.entityId
            ? localText('Site.OrderDialog.EditTitle', 'Sipariş Düzenle')
            : localText('Site.OrderDialog.CreateTitle', 'Sipariş Oluştur');
    }

    protected getTemplate(): string {
        return `
<div class="opd-root">
    <div class="opd-topbar">
        <div id="~_CustomerWrap" class="opd-customer-wrap" style="display:none">
            <i class="fa fa-building opd-customer-icon"></i>
            <select id="~_CustomerSelect" class="form-select form-select-sm opd-customer-select">
                <option value="">${localText('Site.OrderDialog.SelectCustomer', 'Cari Seçiniz...')}</option>
            </select>
        </div>
        <div class="opd-search-wrap">
            <i class="fa fa-search opd-search-icon"></i>
            <input id="~_SearchInput" type="text" class="form-control form-control-sm"
                   placeholder="${localText('Site.OrderDialog.SearchProductPlaceholder', 'Ürün adı veya kodu ile ara...')}" />
        </div>
        <button id="~_CartToggle" class="btn opd-cart-btn">
            <i class="fa fa-shopping-cart"></i>
            <span>${localText('Site.OrderDialog.Cart', 'Sepet')}</span>
            <span class="opd-cart-badge" id="~_CartBadge">0</span>
        </button>
    </div>
    <div class="opd-body">
        <aside class="opd-sidebar">
            <div class="opd-sidebar-head">${localText('Site.OrderDialog.Categories', 'Kategoriler')}</div>
            <div id="~_CategoryList" class="opd-cat-list"></div>
            <div id="~_BrandSection" class="opd-brand-section" style="display:none">
                <div class="opd-sidebar-head opd-brand-head">${localText('Site.OrderDialog.Brands', 'Markalar')}</div>
                <div id="~_BrandList" class="opd-brand-list"></div>
            </div>
        </aside>
        <main class="opd-main">
            <div id="~_ProductGrid" class="opd-product-grid">
                <div class="opd-loading">
                    <i class="fa fa-spinner fa-spin"></i>&nbsp;${localText('Site.OrderDialog.LoadingProducts', 'Ürünler yükleniyor...')}
                </div>
            </div>
        </main>
        <aside id="~_CartPanel" class="opd-cart-panel opd-cart-closed">
            <div class="opd-cart-head">
                <span><i class="fa fa-shopping-cart"></i>&nbsp;${localText('Site.OrderDialog.MyCart', 'Sepetim')}</span>
                <button id="~_CloseCart" class="btn btn-link opd-close-cart">
                    <i class="fa fa-times"></i>
                </button>
            </div>
            <div id="~_CartItems" class="opd-cart-items">
                <div class="opd-cart-empty">${localText('Site.OrderDialog.CartEmpty', 'Sepet boş')}</div>
            </div>
            <div class="opd-cart-foot">
                <div class="opd-cart-total-row">
                    <span>${localText('Site.OrderDialog.Total', 'Toplam')}</span>
                    <strong id="~_CartTotal">0,00 ₺</strong>
                </div>
                <button id="~_CompleteOrder" class="btn opd-complete-btn">
                    <i class="fa fa-check"></i>&nbsp;
                    ${this.entityId ? localText('Site.OrderDialog.UpdateOrder', 'Siparişi Güncelle') : localText('Site.OrderDialog.CompleteOrder', 'Siparişi Tamamla')}
                </button>
            </div>
        </aside>
    </div>
</div>`;
    }

    protected onDialogOpen(): void {
        super.onDialogOpen();
        this.bindElements();
        if (this.options?.onProductsSelected) {
            this.dialogTitle = localText('Site.OrderDialog.SelectProductTitle', 'Ürün Seç');
            const btn = this.byId('CompleteOrder')?.getNode() as HTMLButtonElement;
            if (btn) btn.innerHTML = `<i class="fa fa-check"></i>&nbsp;${localText('Site.OrderDialog.ConfirmSelection', 'Seçimi Onayla')}`;
        }
        this.loadData();
    }

    private bindElements(): void {
        const n = (id: string) => this.byId(id)?.getNode() as HTMLElement;

        this.imgTooltipEl = document.createElement('div');
        this.imgTooltipEl.style.cssText =
            'display:none;position:fixed;z-index:99999;pointer-events:none;' +
            'background:#fff;border:1px solid #ddd;border-radius:6px;' +
            'box-shadow:0 4px 16px rgba(0,0,0,.18);padding:4px;';
        document.body.appendChild(this.imgTooltipEl);

        this.customerWrapEl   = n('CustomerWrap');
        this.customerSelectEl = n('CustomerSelect') as HTMLSelectElement;
        this.catListEl      = n('CategoryList');
        this.brandSectionEl = n('BrandSection');
        this.brandListEl    = n('BrandList');
        this.productGridEl  = n('ProductGrid');
        this.searchEl       = n('SearchInput') as HTMLInputElement;
        this.cartPanelEl    = n('CartPanel');
        this.cartItemsEl    = n('CartItems');
        this.cartTotalEl    = n('CartTotal');
        this.cartBadgeEl    = n('CartBadge');

        n('CartToggle').addEventListener('click', () => this.toggleCart());
        n('CloseCart').addEventListener('click', () => this.toggleCart(false));
        n('CompleteOrder').addEventListener('click', () => this.completeOrder());

        this.customerSelectEl.addEventListener('change', () => {
            const val = parseInt(this.customerSelectEl.value, 10);
            this.onCustomerChanged(Number.isNaN(val) ? null : val);
        });

        this.searchEl.addEventListener('input', () => {
            this.searchTerm = this.searchEl.value.toLowerCase().trim();
            this.renderProducts();
        });
    }

    private async loadData(): Promise<void> {
        const [pl, cl, vt, cat, br, currLookup] = await Promise.all([
            getLookupAsync<ProductsRow>(ProductsRow.lookupKey),
            getLookupAsync<CustomersRow>(CustomersRow.lookupKey),
            getLookupAsync<VendorTypeRow>(VendorTypeRow.lookupKey),
            getLookupAsync<ProductCategoryRow>('Catalog.ProductCategory'),
            getLookupAsync<BrandsRow>('Catalog.Brands'),
            getLookupAsync<CurrencyListRow>(CurrencyListRow.lookupKey)
        ]);
        this.productLookup    = pl;
        this.customerLookup   = cl;
        this.vendorTypeLookup = vt;
        this.categoryLookup   = cat;
        this.brandLookup      = br;

        const eur = currLookup.items.find(c => c.Code === 'EUR');
        if (eur) {
            this.activeCurrencyCode = eur.Symbol || eur.Code || '€';
            this.activeCurrencyId   = eur.Id;
        }

        this.allProducts = this.productLookup.items.filter(p => p.IsActive !== 0);
        this.renderCategories();
        this.renderBrands();
        this.renderProducts();

        const isBayi = hasPermission(PermissionKeys.Bayii)
            && !hasPermission(PermissionKeys.Security)
            && !hasPermission(PermissionKeys.Temsilci);

        if (this.options?.preSelectedCustomerId) {
            this.customerId = this.options.preSelectedCustomerId;
            await this.loadPriceListItems(this.customerId);
        } else if (isBayi) {
            this.tryAutoSelectCustomer();
        } else {
            // Bayi olmayan kullanıcı (yönetici/temsilci): cariyi kendisi seçmeli
            this.requireCustomerSelection = true;
            this.setupCustomerSelector();
        }

        if (this.entityId) {
            await this.loadExistingOrder(this.entityId);
        }
    }

    /** Bayi olmayan kullanıcılar için cari seçim kutusunu doldurur ve gösterir. */
    private setupCustomerSelector(): void {
        if (!this.customerSelectEl || !this.customerWrapEl) return;

        const customers = (this.customerLookup?.items ?? [])
            .filter(c => c.IsActive !== false)
            .sort((a, b) => (a.Name ?? '').localeCompare(b.Name ?? '', 'tr'));

        const opts = [`<option value="">${localText('Site.OrderDialog.SelectCustomer', 'Cari Seçiniz...')}</option>`];
        for (const c of customers) {
            const label = c.Code ? `${c.Code} - ${c.Name ?? ''}` : (c.Name ?? '');
            opts.push(`<option value="${c.Id}">${htmlEncode(label)}</option>`);
        }
        this.customerSelectEl.innerHTML = opts.join('');
        if (this.customerId) this.customerSelectEl.value = String(this.customerId);
        this.customerWrapEl.style.display = '';
    }

    /** Cari değiştiğinde fiyat listesini yeniden yükler ve sepeti yeni fiyatlara göre günceller. */
    private async onCustomerChanged(customerId: number | null): Promise<void> {
        this.customerId = customerId;
        this.priceListItems.clear();
        if (customerId) {
            await this.loadPriceListItems(customerId);
        }
        this.recalcCartPrices();
        this.renderProducts();
        this.updateCartUI();
    }

    /** Sepetteki kalemlerin birim fiyat/indirim/satır toplamını aktif cariye göre yeniden hesaplar. */
    private recalcCartPrices(): void {
        this.cart.forEach((item, productId) => {
            const p = this.productLookup?.itemById[productId];
            if (!p) return;
            const price     = this.getProductPrice(p);
            const actualQty = item.quantity * item.packingQty;
            item.unitPrice  = price;
            item.discount   = this.calcDiscount(price, actualQty);
            item.lineTotal  = price * actualQty - item.discount;
        });
    }

    private tryAutoSelectCustomer(): void {
        if (this.entityId || this.customerId) return;

        OrderService.GetCurrentBayiiCustomerId({}).then(
            resp => {
                if (resp?.CustomerId) {
                    this.customerId = resp.CustomerId;
                    this.loadPriceListItems(resp.CustomerId);
                }
            },
            () => {}
        );
    }

    private async loadPriceListItems(customerId: number): Promise<void> {
        const customer = this.customerLookup?.itemById[customerId];
        const priceListId = (customer as any)?.PriceListId as number | undefined;
        if (!priceListId) return;

        try {
            const [priceListLookup, resp] = await Promise.all([
                getLookupAsync<PriceListsRow>(PriceListsRow.lookupKey),
                PriceListItemsService.List({ EqualityFilter: { PriceListId: String(priceListId) } })
            ]);

            const priceList  = priceListLookup.itemById[priceListId];
            const currLookup = await getLookupAsync<CurrencyListRow>(CurrencyListRow.lookupKey);
            const cur = priceList?.CurrencyId ? currLookup.itemById[priceList.CurrencyId] : null;
            if (cur) {
                this.activeCurrencyCode = cur.Symbol || cur.Code || '₺';
                this.activeCurrencyId   = cur.Id;
            }

            this.priceListItems.clear();
            for (const item of resp?.Entities ?? []) {
                if (item.ProductId != null) this.priceListItems.set(item.ProductId, item);
            }
            this.renderProducts();
            this.updateCartUI();
        } catch { /* fiyat listesi yoksa varsayılan fiyat kullanılır */ }
    }

    private getProductPrice(product: ProductsRow): number {
        const item = this.priceListItems.get(product.Id!);
        if (item?.UnitPrice != null) return item.UnitPrice;
        return 0;
    }

    private getPackingQty(product: ProductsRow): number {
        return (product as any).PackingQuantity as number ?? 1;
    }

    private async loadExistingOrder(id: number): Promise<void> {
        try {
            const orderResp = await OrderService.Retrieve({ EntityId: id });
            const order = orderResp.Entity;
            if (!order) return;

            if (order.CustomerId) {
                this.customerId = order.CustomerId;
                if (this.requireCustomerSelection && this.customerSelectEl)
                    this.customerSelectEl.value = String(order.CustomerId);
                await this.loadPriceListItems(order.CustomerId);
            }

            let rows = order.DetailList ?? [];

            if (rows.length === 0) {
                const detailResp = await OrderDetailService.List({
                    EqualityFilter: { OrderId: String(id) }
                });
                rows = detailResp?.Entities ?? [];
            }

            for (const d of rows) {
                if (!d.ProductId) continue;
                const p          = this.productLookup?.itemById[d.ProductId];
                const packingQty = (p as any)?.PackingQuantity as number ?? 1;
                const actualQty  = d.Quantity ?? 1;
                const koliCount  = Math.max(1, Math.round(actualQty / packingQty));
                this.cart.set(d.ProductId, {
                    productId:   d.ProductId,
                    productCode: p?.Code     ?? '',
                    productName: p?.Name     ?? (d.ProductCodeName ?? ''),
                    quantity:    koliCount,
                    packingQty,
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
            notifyError(localText('Site.OrderDialog.OrderLoadFailed', 'Sipariş yüklenemedi: ') + (err?.message || ''));
        }
    }

    // Sadece brand filtresi uygulanmış ürünler (kategori filtresi yok)
    private getBrandFilteredProducts(): ProductsRow[] {
        if (this.selectedBrandIds.size === 0) return this.allProducts;
        return this.allProducts.filter(p => {
            const bid = (p as any).BrandId as number | undefined;
            return bid != null && this.selectedBrandIds.has(bid);
        });
    }

    // Sadece kategori filtresi uygulanmış ürünler (brand filtresi yok)
    private getCategoryFilteredProducts(): ProductsRow[] {
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
        return products;
    }

    private renderCategories(): void {
        if (!this.catListEl) return;

        // Kategori listesi brand filtresine göre de daralır
        const baseProducts = this.getBrandFilteredProducts();
        const usedIds = new Set(baseProducts.map(p => p.CategoryId).filter(Boolean) as number[]);

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

        // Seçili kategori artık görünür değilse sıfırla
        if (this.selectedCategoryId !== null && !visibleCats.has(this.selectedCategoryId)) {
            this.selectedCategoryId = null;
        }

        // parent → children map
        const childrenOf = new Map<number | null, ProductCategoryRow[]>();
        for (const [, cat] of visibleCats) {
            const key = (cat.ParentId && visibleCats.has(cat.ParentId)) ? cat.ParentId : null;
            if (!childrenOf.has(key)) childrenOf.set(key, []);
            childrenOf.get(key)!.push(cat);
        }
        childrenOf.forEach(arr =>
            arr.sort((a, b) => (a.FullPath ?? a.Name ?? '').localeCompare(b.FullPath ?? b.Name ?? '', 'tr'))
        );

        this.catListEl.replaceChildren();

        // "Tüm Ürünler" satırı
        const allItem = document.createElement('div');
        allItem.className = 'opd-cat-item' + (this.selectedCategoryId === null ? ' active' : '');
        allItem.dataset.id = '';
        const allIcon = document.createElement('i');
        allIcon.className = 'fa fa-th-large';
        allItem.append(allIcon, ' ' + localText('Site.OrderDialog.AllProducts', 'Tüm Ürünler'));
        allItem.addEventListener('click', () => {
            this.selectedCategoryId = null;
            this.catListEl.querySelectorAll('.opd-cat-item').forEach(x => x.classList.remove('active'));
            allItem.classList.add('active');
            this.renderBrands();
            this.renderProducts();
        });
        this.catListEl.appendChild(allItem);

        const renderNode = (cat: ProductCategoryRow, depth: number) => {
            const children = childrenOf.get(cat.Id!) ?? [];
            const hasChildren = children.length > 0;
            const isExpanded = this.expandedCategories.has(cat.Id!);

            const el = document.createElement('div');
            el.className = 'opd-cat-item' + (this.selectedCategoryId === cat.Id ? ' active' : '');
            el.dataset.id = String(cat.Id);
            el.style.paddingLeft = (12 + depth * 14) + 'px';

            const icon = document.createElement('i');
            icon.className = hasChildren
                ? 'fa ' + (isExpanded ? 'fa-caret-down' : 'fa-caret-right') + ' opd-cat-toggle'
                : 'fa fa-tag';
            el.appendChild(icon);

            const nameSpan = document.createElement('span');
            nameSpan.textContent = ' ' + (cat.Name ?? '');
            el.appendChild(nameSpan);

            el.addEventListener('click', () => {
                this.selectedCategoryId = cat.Id!;
                if (hasChildren) {
                    if (this.expandedCategories.has(cat.Id!)) {
                        this.expandedCategories.delete(cat.Id!);
                    } else {
                        this.expandedCategories.add(cat.Id!);
                    }
                    this.renderCategories();
                } else {
                    this.catListEl.querySelectorAll('.opd-cat-item').forEach(x => x.classList.remove('active'));
                    el.classList.add('active');
                }
                this.renderBrands();
                this.renderProducts();
            });

            this.catListEl.appendChild(el);

            if (hasChildren && isExpanded) {
                for (const child of children) {
                    renderNode(child, depth + 1);
                }
            }
        };

        for (const root of (childrenOf.get(null) ?? [])) {
            renderNode(root, 0);
        }
    }

    private renderBrands(): void {
        if (!this.brandListEl || !this.brandSectionEl) return;

        const categoryProducts = this.getCategoryFilteredProducts();
        const brandIds = new Set(
            categoryProducts.map(p => (p as any).BrandId as number | undefined).filter(Boolean) as number[]
        );

        // Yeni kategoride olmayan seçili markaları temizle
        for (const id of this.selectedBrandIds) {
            if (!brandIds.has(id)) this.selectedBrandIds.delete(id);
        }

        if (brandIds.size === 0) {
            this.brandSectionEl.style.display = 'none';
            this.brandListEl.replaceChildren();
            return;
        }

        const brands = Array.from(brandIds)
            .map(id => this.brandLookup?.itemById[id])
            .filter(Boolean)
            .sort((a, b) => (a!.Name ?? '').localeCompare(b!.Name ?? '', 'tr')) as BrandsRow[];

        this.brandSectionEl.style.display = '';
        this.brandListEl.replaceChildren();

        brands.forEach(brand => {
            const label = document.createElement('label');
            label.className = 'opd-brand-item';

            const cb = document.createElement('input');
            cb.type = 'checkbox';
            cb.checked = this.selectedBrandIds.has(brand.Id!);
            cb.addEventListener('change', () => {
                if (cb.checked) {
                    this.selectedBrandIds.add(brand.Id!);
                } else {
                    this.selectedBrandIds.delete(brand.Id!);
                }
                this.renderCategories();
                this.renderProducts();
            });

            const name = document.createElement('span');
            name.textContent = brand.Name ?? '';

            label.append(cb, name);
            this.brandListEl.appendChild(label);
        });
    }

    private renderProducts(): void {
        if (!this.productGridEl) return;

        let products = this.getCategoryFilteredProducts();

        if (this.selectedBrandIds.size > 0) {
            products = products.filter(p => {
                const bid = (p as any).BrandId as number | undefined;
                return bid != null && this.selectedBrandIds.has(bid);
            });
        }

        if (this.searchTerm) {
            products = products.filter(p =>
                p.Code?.toLowerCase().includes(this.searchTerm) ||
                p.Name?.toLowerCase().includes(this.searchTerm)
            );
        }

        if (products.length === 0) {
            this.productGridEl.innerHTML = `<div class="opd-no-results"><i class="fa fa-search"></i><br>${localText('Site.OrderDialog.NoProductsFound', 'Ürün bulunamadı')}</div>`;
            return;
        }

        let html = '';
        products.forEach(product => {
            const inCart   = this.cart.has(product.Id!);
            const price    = this.getProductPrice(product);
            const currency = htmlEncode(this.activeCurrencyCode);

            html += `
<div class="opd-card${inCart ? ' opd-card--in-cart' : ''}" data-id="${product.Id}">
    <div class="opd-card-body">
        <div class="opd-card-code">${htmlEncode(product.Code || '')}</div>
        <div class="opd-card-name" title="${htmlEncode(product.Name || '')}">${htmlEncode(product.Name || '')}</div>
        <div class="opd-card-cat">${htmlEncode(product.CategoryName || '')}</div>
        <div class="opd-card-price">${this.fmt(price)}&nbsp;${currency}</div>
        ${product.PackingQuantity && product.PackingQuantity > 1
            ? `<div class="opd-card-unit">${htmlEncode(product.PackingName || localText('Site.OrderDialog.Box', 'Koli'))}: ${product.PackingQuantity} ${htmlEncode(product.UnitName || localText('Site.OrderDialog.Piece', 'adet'))}</div>`
            : product.UnitName ? `<div class="opd-card-unit">${htmlEncode(product.UnitName)}</div>` : ''
        }
    </div>
    <div class="opd-card-footer">
        <div class="opd-qty-wrap">
            <button class="opd-qty-btn opd-qty-minus" data-id="${product.Id}">−</button>
            <input type="number" class="opd-qty-input" data-id="${product.Id}"
                   value="1" min="0.001" step="1" />
            <button class="opd-qty-btn opd-qty-plus" data-id="${product.Id}">+</button>
        </div>
        <button class="btn opd-add-btn" data-id="${product.Id}">
            <i class="fa fa-plus"></i>&nbsp;${localText('Site.OrderDialog.AddToCart', 'Sepete Ekle')}
        </button>
    </div>
</div>`;
        });

        this.productGridEl.innerHTML = html;
        this.bindProductEvents();
    }

    private getThumbUrl(imagePath: string | undefined): string | null {
        if (!imagePath) return null;
        const dot = imagePath.lastIndexOf('.');
        const thumb = dot > -1 ? imagePath.slice(0, dot) + '_t.jpg' : imagePath + '_t.jpg';
        return '/upload/' + thumb;
    }

    private positionImgTooltip(e: MouseEvent): void {
        const gap = 12, tw = 188, th = 188;
        let left = e.clientX + gap;
        let top  = e.clientY + gap;
        if (left + tw > window.innerWidth)  left = e.clientX - tw - gap;
        if (top  + th > window.innerHeight) top  = e.clientY - th - gap;
        this.imgTooltipEl.style.left = left + 'px';
        this.imgTooltipEl.style.top  = top  + 'px';
    }

    private bindProductEvents(): void {
        const g = this.productGridEl;

        g.querySelectorAll<HTMLDivElement>('.opd-card').forEach(card => {
            card.addEventListener('mouseenter', (e: MouseEvent) => {
                const id = parseInt(card.dataset.id!);
                const product = this.productLookup?.itemById[id];
                const thumbUrl = this.getThumbUrl((product as any)?.ProductImage);
                if (!thumbUrl) return;
                this.imgTooltipEl.innerHTML =
                    `<img src="${thumbUrl}" alt="" style="max-width:180px;max-height:180px;display:block;"
                          onerror="this.parentElement.style.display='none'" />`;
                this.imgTooltipEl.style.display = 'block';
                this.positionImgTooltip(e);
            });
            card.addEventListener('mousemove', (e: MouseEvent) => this.positionImgTooltip(e));
            card.addEventListener('mouseleave', () => { this.imgTooltipEl.style.display = 'none'; });
        });

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
        if (this.requireCustomerSelection && !this.customerId) {
            notifyWarning(localText('Site.OrderDialog.SelectCustomerFirst', 'Lütfen önce bir cari (müşteri) seçiniz.'));
            this.customerSelectEl?.focus();
            return;
        }

        const p = this.productLookup.itemById[productId];
        if (!p) return;

        const price      = this.getProductPrice(p);
        const packingQty = this.getPackingQty(p);
        const existing   = this.cart.get(productId);
        const koliCount  = (existing?.quantity ?? 0) + addQty;
        const actualQty  = koliCount * packingQty;
        const discount   = this.calcDiscount(price, actualQty);

        this.cart.set(productId, {
            productId,
            productCode: p.Code    || '',
            productName: p.Name    || '',
            quantity:    koliCount,
            packingQty,
            unitId:      p.UnitId,
            unitName:    p.UnitName,
            unitPrice:   price,
            vatRateId:   p.VatRateId,
            vatRate:     p.VatRate,
            discount,
            lineTotal:   price * actualQty - discount
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
        const type = (vt.DiscountType ?? '').trim().toLowerCase();
        if (type === 'percentage' || type === '%' || type === 'yüzde') {
            return (price * quantity * vt.DiscountValue) / 100;
        }
        return 0;
    }

    private updateCartUI(): void {
        const count = this.cart.size;
        if (this.cartBadgeEl) this.cartBadgeEl.textContent = String(count);

        if (count === 0) {
            if (this.cartItemsEl) this.cartItemsEl.innerHTML = `<div class="opd-cart-empty">${localText('Site.OrderDialog.CartEmpty', 'Sepet boş')}</div>`;
            if (this.cartTotalEl) this.cartTotalEl.textContent = '0,00 ' + this.activeCurrencyCode;
            return;
        }

        let total = 0;
        let html  = '';

        this.cart.forEach(item => {
            total += item.lineTotal;
            const koliLabel = item.packingQty > 1
                ? `${item.quantity} ${localText('Site.OrderDialog.BoxLower', 'koli')} × ${item.packingQty} ${localText('Site.OrderDialog.Piece', 'adet')}`
                : `${item.quantity} ${localText('Site.OrderDialog.Piece', 'adet')}`;
            html  += `
<div class="opd-ci">
    <div class="opd-ci-info">
        <div class="opd-ci-name">${htmlEncode(item.productName)}</div>
        <div class="opd-ci-meta">${htmlEncode(item.productCode)}${item.unitName ? ' · ' + htmlEncode(item.unitName) : ''}</div>
    </div>
    <input type="number" class="opd-cart-qty" data-id="${item.productId}"
           value="${item.quantity}" min="1" step="1" title="${localText('Site.OrderDialog.BoxCount', 'Koli sayısı')}" />
    <div class="opd-ci-price">
        <div>${this.fmt(item.lineTotal)}&nbsp;${htmlEncode(this.activeCurrencyCode)}</div>
        <small>${koliLabel}</small>
    </div>
    <button class="opd-ci-del" data-id="${item.productId}" title="${localText('Site.OrderDialog.Remove', 'Kaldır')}">
        <i class="fa fa-trash-o"></i>
    </button>
</div>`;
        });

        if (this.cartItemsEl) {
            this.cartItemsEl.innerHTML = html;

            this.cartItemsEl.querySelectorAll<HTMLInputElement>('.opd-cart-qty').forEach(inp => {
                inp.addEventListener('change', () => {
                    const id   = parseInt(inp.dataset.id!);
                    const koli = Math.max(1, parseInt(inp.value, 10) || 1);
                    const item = this.cart.get(id);
                    if (item) {
                        const actualQty  = koli * item.packingQty;
                        item.quantity    = koli;
                        item.discount    = this.calcDiscount(item.unitPrice, actualQty);
                        item.lineTotal   = item.unitPrice * actualQty - item.discount;
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

        if (this.cartTotalEl) this.cartTotalEl.textContent = this.fmt(total) + ' ' + this.activeCurrencyCode;
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
            notifyWarning(localText('Site.OrderDialog.CartEmptyAddProduct', 'Sepet boş! Lütfen en az bir ürün ekleyin.'));
            return;
        }

        if (this.requireCustomerSelection && !this.customerId) {
            notifyWarning(localText('Site.OrderDialog.SelectCustomerFirst', 'Lütfen önce bir cari (müşteri) seçiniz.'));
            this.customerSelectEl?.focus();
            return;
        }

        const detailList: OrderDetailRow[] = Array.from(this.cart.values()).map(item => ({
            ProductId: item.productId,
            Quantity:  item.quantity * item.packingQty,
            UnitId:    item.unitId,
            UnitPrice: item.unitPrice,
            VatRateId: item.vatRateId,
            VatRate:   item.vatRate,
            Discount:  item.discount,
            LineTotal: item.lineTotal
        }));

        // Picker modu: karton miktarıyla geri döndür (adet değil)
        if (this.options?.onProductsSelected) {
            const pickerList: OrderDetailRow[] = Array.from(this.cart.values()).map(item => ({
                ProductId: item.productId,
                Quantity:  item.quantity,
                UnitId:    item.unitId,
                UnitPrice: item.unitPrice,
                VatRateId: item.vatRateId,
                VatRate:   item.vatRate,
                Discount:  item.discount,
                LineTotal: item.lineTotal
            }));
            this.options.onProductsSelected(pickerList);
            this.dialogClose();
            return;
        }

        const totalAmount = detailList.reduce((s, d) => s + (d.LineTotal || 0), 0);

        const order: OrderRow = {
            CustomerId:  this.customerId ?? undefined,
            CurrencyId:  this.activeCurrencyId,
            OrderDate:   new Date().toISOString(),
            Status:      14,
            TotalAmount: totalAmount,
            NetAmount:   totalAmount,
            DetailList:  detailList
        };

        try {
            if (this.entityId) {
                order.Id = this.entityId;
                await OrderService.Update({ EntityId: this.entityId, Entity: order });
                notifySuccess(localText('Site.OrderDialog.OrderUpdated', 'Sipariş güncellendi!'));
            } else {
                await OrderService.Create({ Entity: order });
                notifySuccess(localText('Site.OrderDialog.OrderCreated', 'Sipariş başarıyla oluşturuldu!'));
            }
            this.options?.onSave?.();
            this.dialogClose();
        } catch (err: any) {
            notifyError(localText('Site.OrderDialog.OperationError', 'İşlem sırasında hata oluştu: ') + (err?.message || localText('Site.OrderDialog.UnknownError', 'Bilinmeyen hata')));
        }
    }

    private fmt(n: number): string {
        return new Intl.NumberFormat('tr-TR', { minimumFractionDigits: 2, maximumFractionDigits: 2 }).format(n);
    }

    public destroy(): void {
        if (this.imgTooltipEl?.parentNode)
            this.imgTooltipEl.parentNode.removeChild(this.imgTooltipEl);
        super.destroy();
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
