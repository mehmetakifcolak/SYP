import { Decorators, DialogButton, TemplatedDialog, getLookupAsync, htmlEncode, Lookup, notifyWarning } from "@serenity-is/corelib";
import { ProductsRow } from "../../ServerTypes/Catalog";

export interface BulkPriceItem {
    ProductId: number;
    ProductCode: string;
    ProductName: string;
    UnitPrice: number;
    DiscountRate?: number;
}

interface SelectedProductWithPrice {
    product: ProductsRow;
    unitPrice: number;
    discountRate?: number;
}

export interface BulkPriceDialogOptions {
    onSelect?: (items: BulkPriceItem[]) => void;
    excludeProductIds?: number[];
}

@Decorators.registerClass("SYP.Catalog.BulkPriceDialog")
export class BulkPriceDialog extends TemplatedDialog<BulkPriceDialogOptions> {
    private selectedProducts: Map<number, SelectedProductWithPrice> = new Map();
    private productListDiv: HTMLElement;
    private searchInput: HTMLInputElement;
    private selectedCountSpan: HTMLElement;
    private productLookup: Lookup<ProductsRow>;

    constructor(opt?: BulkPriceDialogOptions) {
        super(opt);
        this.dialogTitle = "Toplu Fiyat Tanımlama";
    }

    protected getTemplate(): string {
        return `
            <div class="bulk-price-selection" style="padding: 10px;">
                <div class="search-box mb-3">
                    <input type="text" id="~_SearchInput" class="form-control" placeholder="Ürün kodu veya adı ile arayın..." />
                </div>
                <div class="selected-count mb-2">
                    <span id="~_SelectedCount">0</span> ürün seçildi
                </div>
                <div id="~_ProductList" class="product-list" style="max-height: 450px; overflow-y: auto; border: 1px solid #ddd; padding: 10px;">
                    <div class="text-muted">Ürünler yükleniyor...</div>
                </div>
            </div>
        `;
    }

    protected onDialogOpen(): void {
        super.onDialogOpen();

        this.searchInput = this.byId("SearchInput")?.getNode() as HTMLInputElement;
        this.productListDiv = this.byId("ProductList")?.getNode() as HTMLElement;
        this.selectedCountSpan = this.byId("SelectedCount")?.getNode() as HTMLElement;

        if (this.searchInput) {
            this.searchInput.addEventListener("input", () => this.filterProducts());
        }

        this.loadProducts();
    }

    protected getDialogButtons(): DialogButton[] {
        return [
            {
                text: "Seçilenleri Ekle",
                cssClass: "btn btn-primary",
                click: () => {
                    if (this.selectedProducts.size > 0) {
                        const invalidItems: string[] = [];
                        this.selectedProducts.forEach((item) => {
                            if (!item.unitPrice || item.unitPrice <= 0) {
                                invalidItems.push(item.product.Code || '');
                            }
                        });

                        if (invalidItems.length > 0) {
                            notifyWarning(`Şu ürünlerin fiyatı 0'dan büyük olmalıdır: ${invalidItems.join(', ')}`);
                            return;
                        }

                        const items: BulkPriceItem[] = Array.from(this.selectedProducts.values()).map(item => ({
                            ProductId: item.product.Id!,
                            ProductCode: item.product.Code!,
                            ProductName: item.product.Name!,
                            UnitPrice: item.unitPrice,
                            DiscountRate: item.discountRate
                        }));
                        this.options.onSelect?.(items);
                        this.dialogClose();
                    }
                }
            },
            {
                text: "İptal",
                cssClass: "btn btn-default",
                click: () => this.dialogClose()
            }
        ];
    }

    private async loadProducts(): Promise<void> {
        try {
            this.productLookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);
            const excludeIds = new Set(this.options?.excludeProductIds || []);

            const products = this.productLookup.items.filter(p =>
                !excludeIds.has(p.Id!) && p.IsActive === 1
            );

            this.renderProductList(products);
        } catch (e) {
            console.error("Ürünler yüklenirken hata:", e);
            if (this.productListDiv) {
                this.productListDiv.innerHTML = '<div class="text-danger">Ürünler yüklenemedi</div>';
            }
        }
    }

    private filterProducts(): void {
        if (!this.productLookup) return;

        const searchTerm = this.searchInput?.value?.toLowerCase().trim() || '';
        const excludeIds = new Set(this.options?.excludeProductIds || []);

        let products = this.productLookup.items.filter(p =>
            !excludeIds.has(p.Id!) && p.IsActive === 1
        );

        if (searchTerm) {
            products = products.filter(p =>
                (p.Code?.toLowerCase().includes(searchTerm)) ||
                (p.Name?.toLowerCase().includes(searchTerm))
            );
        }

        this.renderProductList(products);
    }

    private renderProductList(products: ProductsRow[]): void {
        if (!this.productListDiv) return;

        if (products.length === 0) {
            this.productListDiv.innerHTML = '<div class="text-muted">Ürün bulunamadı</div>';
            return;
        }

        let html = '<table class="table table-hover table-sm mb-0">';
        html += `<thead><tr>
            <th style="width: 40px;"><input type="checkbox" class="select-all-checkbox" /></th>
            <th>Ürün Kodu</th>
            <th>Ürün Adı</th>
            <th style="width: 120px;">Mevcut Fiyat</th>
            <th style="width: 120px;">Yeni Fiyat</th>
        </tr></thead>`;
        html += '<tbody>';

        for (const product of products) {
            const selectedItem = this.selectedProducts.get(product.Id!);
            const isChecked = selectedItem != null;
            const unitPrice = selectedItem?.unitPrice || product.UnitPrice || 0;

            html += `<tr class="product-row" data-id="${product.Id}">
                <td><input type="checkbox" class="product-checkbox" data-id="${product.Id}" ${isChecked ? 'checked' : ''} /></td>
                <td>${htmlEncode(product.Code || '')}</td>
                <td>${htmlEncode(product.Name || '')}</td>
                <td class="text-right">${product.UnitPrice?.toFixed(4) || '-'}</td>
                <td>
                    <input type="number" class="form-control form-control-sm price-input"
                           data-id="${product.Id}"
                           value="${unitPrice}"
                           min="0"
                           step="0.0001"
                           style="width: 100px;"
                           ${!isChecked ? 'disabled' : ''} />
                </td>
            </tr>`;
        }

        html += '</tbody></table>';
        this.productListDiv.innerHTML = html;

        // Event listeners
        const checkboxes = this.productListDiv.querySelectorAll('.product-checkbox');
        checkboxes.forEach(cb => {
            cb.addEventListener('change', (e) => {
                const target = e.target as HTMLInputElement;
                const productId = parseInt(target.dataset.id!, 10);
                const product = this.productLookup.itemById[productId];
                const row = target.closest('tr');
                const priceInput = row?.querySelector('.price-input') as HTMLInputElement;

                if (target.checked) {
                    const unitPrice = parseFloat(priceInput?.value) || product.UnitPrice || 0;
                    this.selectedProducts.set(productId, { product, unitPrice });
                    if (priceInput) {
                        priceInput.disabled = false;
                        priceInput.focus();
                        priceInput.select();
                    }
                } else {
                    this.selectedProducts.delete(productId);
                    if (priceInput) {
                        priceInput.disabled = true;
                    }
                }

                this.updateSelectedCount();
                this.updateSelectAllCheckbox();
            });
        });

        // Price input listeners
        const priceInputs = this.productListDiv.querySelectorAll('.price-input');
        priceInputs.forEach(input => {
            input.addEventListener('change', (e) => {
                const target = e.target as HTMLInputElement;
                const productId = parseInt(target.dataset.id!, 10);
                const unitPrice = parseFloat(target.value) || 0;

                const selectedItem = this.selectedProducts.get(productId);
                if (selectedItem) {
                    selectedItem.unitPrice = unitPrice;
                }
            });

            input.addEventListener('focus', (e) => {
                const target = e.target as HTMLInputElement;
                const productId = parseInt(target.dataset.id!, 10);
                const row = target.closest('tr');
                const checkbox = row?.querySelector('.product-checkbox') as HTMLInputElement;

                if (checkbox && !checkbox.checked) {
                    checkbox.checked = true;
                    checkbox.dispatchEvent(new Event('change'));
                }
            });
        });

        // Select all checkbox
        const selectAll = this.productListDiv.querySelector('.select-all-checkbox') as HTMLInputElement;
        if (selectAll) {
            selectAll.addEventListener('change', (e) => {
                const target = e.target as HTMLInputElement;

                checkboxes.forEach(cb => {
                    const checkbox = cb as HTMLInputElement;
                    checkbox.checked = target.checked;
                    const productId = parseInt(checkbox.dataset.id!, 10);
                    const product = this.productLookup.itemById[productId];
                    const row = checkbox.closest('tr');
                    const priceInput = row?.querySelector('.price-input') as HTMLInputElement;

                    if (target.checked) {
                        const unitPrice = parseFloat(priceInput?.value) || product.UnitPrice || 0;
                        this.selectedProducts.set(productId, { product, unitPrice });
                        if (priceInput) priceInput.disabled = false;
                    } else {
                        this.selectedProducts.delete(productId);
                        if (priceInput) priceInput.disabled = true;
                    }
                });

                this.updateSelectedCount();
            });
        }

        // Row click
        const rows = this.productListDiv.querySelectorAll('.product-row');
        rows.forEach(row => {
            row.addEventListener('click', (e) => {
                const target = e.target as HTMLElement;
                if (target.tagName === 'INPUT') return;

                const checkbox = row.querySelector('.product-checkbox') as HTMLInputElement;
                checkbox.checked = !checkbox.checked;
                checkbox.dispatchEvent(new Event('change'));
            });
        });

        this.updateSelectAllCheckbox();
    }

    private updateSelectedCount(): void {
        if (this.selectedCountSpan) {
            this.selectedCountSpan.textContent = this.selectedProducts.size.toString();
        }
    }

    private updateSelectAllCheckbox(): void {
        const selectAll = this.productListDiv?.querySelector('.select-all-checkbox') as HTMLInputElement;
        const checkboxes = this.productListDiv?.querySelectorAll('.product-checkbox');

        if (selectAll && checkboxes && checkboxes.length > 0) {
            const allChecked = Array.from(checkboxes).every((cb: Element) => (cb as HTMLInputElement).checked);
            const someChecked = Array.from(checkboxes).some((cb: Element) => (cb as HTMLInputElement).checked);
            selectAll.checked = allChecked;
            selectAll.indeterminate = someChecked && !allChecked;
        }
    }

    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 800;
        return opt;
    }
}
