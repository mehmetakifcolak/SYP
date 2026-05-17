import { Decorators, DialogButton, TemplatedDialog, getLookupAsync, htmlEncode, Lookup, notifyWarning } from "@serenity-is/corelib";
import { ProductsRow } from "../../ServerTypes/Catalog";

export interface SelectedProductWithQuantity {
    product: ProductsRow;
    quantity: number;
}

export interface BulkProductItem {
    ProductId: number;
    ProductCode: string;
    ProductName: string;
    Quantity: number;
    Unit?: string;
    Currency?: string;
    VatRate?: number;
    UnitPrice?: number;
}

export interface BulkProductSelectionDialogOptions {
    onSelect?: (items: BulkProductItem[]) => void;
    excludeProductIds?: number[];
}

@Decorators.registerClass("SYP.Warehouse.BulkProductSelectionDialog")
export class BulkProductSelectionDialog extends TemplatedDialog<BulkProductSelectionDialogOptions> {
    private selectedProducts: Map<number, SelectedProductWithQuantity> = new Map();
    private productListDiv: HTMLElement;
    private searchInput: HTMLInputElement;
    private categorySelect: HTMLSelectElement;
    private brandSelect: HTMLSelectElement;
    private selectedCountSpan: HTMLElement;
    private productLookup: Lookup<ProductsRow>;
    private allProducts: ProductsRow[] = [];

    constructor(opt?: BulkProductSelectionDialogOptions) {
        super(opt);
        this.dialogTitle = "Toplu Ürün Seçimi";
    }

    protected getTemplate(): string {
        return `
            <div class="bulk-product-selection" style="padding: 10px;">
                <div class="filters-row mb-3">
                    <div class="row">
                        <div class="col-md-6">
                            <input type="text" id="~_SearchInput" class="form-control" placeholder="Ürün kodu veya adı ile arayın..." />
                        </div>
                        <div class="col-md-3">
                            <select id="~_CategorySelect" class="form-control">
                                <option value="">Tüm Kategoriler</option>
                            </select>
                        </div>
                        <div class="col-md-3">
                            <select id="~_BrandSelect" class="form-control">
                                <option value="">Tüm Markalar</option>
                            </select>
                        </div>
                    </div>
                </div>
                <div class="selected-count mb-2">
                    <strong><span id="~_SelectedCount">0</span></strong> ürün seçildi
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
        this.categorySelect = this.byId("CategorySelect")?.getNode() as HTMLSelectElement;
        this.brandSelect = this.byId("BrandSelect")?.getNode() as HTMLSelectElement;
        this.productListDiv = this.byId("ProductList")?.getNode() as HTMLElement;
        this.selectedCountSpan = this.byId("SelectedCount")?.getNode() as HTMLElement;

        if (this.searchInput) {
            this.searchInput.addEventListener("input", () => this.filterProducts());
        }

        if (this.categorySelect) {
            this.categorySelect.addEventListener("change", () => this.filterProducts());
        }

        if (this.brandSelect) {
            this.brandSelect.addEventListener("change", () => this.filterProducts());
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
                        // Miktarları kontrol et
                        const invalidItems: string[] = [];
                        this.selectedProducts.forEach((item, id) => {
                            if (item.quantity <= 0) {
                                invalidItems.push(item.product.Code || '');
                            }
                        });

                        if (invalidItems.length > 0) {
                            notifyWarning(`Şu ürünlerin miktarı 0'dan büyük olmalıdır: ${invalidItems.join(', ')}`);
                            return;
                        }

                        const items: BulkProductItem[] = Array.from(this.selectedProducts.values()).map(item => ({
                            ProductId: item.product.Id!,
                            ProductCode: item.product.Code!,
                            ProductName: item.product.Name!,
                            Quantity: item.quantity,
                            Unit: item.product.UnitName,
                            Currency: item.product.CurrencyCode,
                            VatRate: item.product.VatRateId, // VatRateId kullanılıyor, gerekirse lookup'tan oran alınabilir
                            UnitPrice: item.product.UnitPrice
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

            this.allProducts = this.productLookup.items.filter(p =>
                !excludeIds.has(p.Id!)
            );

            // Kategori ve marka listelerini doldur
            this.populateCategoryFilter();
            this.populateBrandFilter();

            this.renderProductList(this.allProducts);
        } catch (e) {
            console.error("Ürünler yüklenirken hata:", e);
            if (this.productListDiv) {
                this.productListDiv.innerHTML = '<div class="text-danger">Ürünler yüklenemedi</div>';
            }
        }
    }

    private populateCategoryFilter(): void {
        if (!this.categorySelect || !this.allProducts) return;

        // Benzersiz kategorileri al
        const categories = new Map<number, string>();
        this.allProducts.forEach(p => {
            if (p.CategoryId && p.CategoryName) {
                categories.set(p.CategoryId, p.CategoryName);
            }
        });

        // Kategorileri sırala ve select'e ekle
        const sortedCategories = Array.from(categories.entries())
            .sort((a, b) => a[1].localeCompare(b[1], 'tr'));

        sortedCategories.forEach(([id, name]) => {
            const option = document.createElement('option');
            option.value = id.toString();
            option.textContent = name;
            this.categorySelect.appendChild(option);
        });
    }

    private populateBrandFilter(): void {
        if (!this.brandSelect || !this.allProducts) return;

        // Benzersiz markaları al
        const brands = new Map<number, string>();
        this.allProducts.forEach(p => {
            if (p.BrandId && p.BrandName) {
                brands.set(p.BrandId, p.BrandName);
            }
        });

        // Markaları sırala ve select'e ekle
        const sortedBrands = Array.from(brands.entries())
            .sort((a, b) => a[1].localeCompare(b[1], 'tr'));

        sortedBrands.forEach(([id, name]) => {
            const option = document.createElement('option');
            option.value = id.toString();
            option.textContent = name;
            this.brandSelect.appendChild(option);
        });
    }

    private filterProducts(): void {
        if (!this.allProducts) return;

        const searchTerm = this.searchInput?.value?.toLowerCase().trim() || '';
        const selectedCategory = this.categorySelect?.value || '';
        const selectedBrand = this.brandSelect?.value || '';

        let products = this.allProducts;

        // Arama filtresi
        if (searchTerm) {
            products = products.filter(p =>
                (p.Code?.toLowerCase().includes(searchTerm)) ||
                (p.Name?.toLowerCase().includes(searchTerm))
            );
        }

        // Kategori filtresi
        if (selectedCategory) {
            const categoryId = parseInt(selectedCategory);
            products = products.filter(p => p.CategoryId === categoryId);
        }

        // Marka filtresi
        if (selectedBrand) {
            const brandId = parseInt(selectedBrand);
            products = products.filter(p => p.BrandId === brandId);
        }

        this.renderProductList(products);
    }

    private renderProductList(products: ProductsRow[]): void {
        if (!this.productListDiv) return;

        if (products.length === 0) {
            this.productListDiv.innerHTML = '<div class="text-muted">Ürün bulunamadı</div>';
            return;
        }

        let html = '<table class="table table-hover table-sm mb-0" style="font-size: 0.9em;">';
        html += '<thead><tr><th style="width: 30px;"><input type="checkbox" class="select-all-checkbox" /></th><th style="width: 100px;">Ürün Kodu</th><th>Ürün Adı</th><th style="width: 120px;">Kategori</th><th style="width: 120px;">Marka</th><th style="width: 90px;">Miktar</th></tr></thead>';
        html += '<tbody>';

        for (const product of products) {
            const selectedItem = this.selectedProducts.get(product.Id!);
            const isChecked = selectedItem != null;
            const quantity = selectedItem?.quantity || 1;

            html += `<tr class="product-row" data-id="${product.Id}" style="cursor: pointer;">
                <td><input type="checkbox" class="product-checkbox" data-id="${product.Id}" ${isChecked ? 'checked' : ''} /></td>
                <td><strong>${htmlEncode(product.Code || '')}</strong></td>
                <td>${htmlEncode(product.Name || '')}</td>
                <td><small class="text-muted">${htmlEncode(product.CategoryName || '-')}</small></td>
                <td><small class="text-muted">${htmlEncode(product.BrandName || '-')}</small></td>
                <td>
                    <input type="number" class="form-control form-control-sm quantity-input"
                           data-id="${product.Id}"
                           value="${quantity}"
                           min="0.0001"
                           step="1"
                           style="width: 75px;"
                           ${!isChecked ? 'disabled' : ''} />
                </td>
            </tr>`;
        }

        html += '</tbody></table>';
        this.productListDiv.innerHTML = html;

        // Event listeners for product checkboxes
        const checkboxes = this.productListDiv.querySelectorAll('.product-checkbox');
        checkboxes.forEach(cb => {
            cb.addEventListener('change', (e) => {
                const target = e.target as HTMLInputElement;
                const productId = parseInt(target.dataset.id!, 10);
                const product = this.productLookup.itemById[productId];
                const row = target.closest('tr');
                const quantityInput = row?.querySelector('.quantity-input') as HTMLInputElement;

                if (target.checked) {
                    const quantity = parseFloat(quantityInput?.value) || 1;
                    this.selectedProducts.set(productId, { product, quantity });
                    if (quantityInput) {
                        quantityInput.disabled = false;
                        quantityInput.focus();
                        quantityInput.select();
                    }
                } else {
                    this.selectedProducts.delete(productId);
                    if (quantityInput) {
                        quantityInput.disabled = true;
                    }
                }

                this.updateSelectedCount();
                this.updateSelectAllCheckbox();
            });
        });

        // Event listeners for quantity inputs
        const quantityInputs = this.productListDiv.querySelectorAll('.quantity-input');
        quantityInputs.forEach(input => {
            input.addEventListener('change', (e) => {
                const target = e.target as HTMLInputElement;
                const productId = parseInt(target.dataset.id!, 10);
                const quantity = parseFloat(target.value) || 1;

                const selectedItem = this.selectedProducts.get(productId);
                if (selectedItem) {
                    selectedItem.quantity = quantity;
                }
            });

            // Miktar girildiğinde otomatik seç
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
                    const quantityInput = row?.querySelector('.quantity-input') as HTMLInputElement;

                    if (target.checked) {
                        const quantity = parseFloat(quantityInput?.value) || 1;
                        this.selectedProducts.set(productId, { product, quantity });
                        if (quantityInput) quantityInput.disabled = false;
                    } else {
                        this.selectedProducts.delete(productId);
                        if (quantityInput) quantityInput.disabled = true;
                    }
                });

                this.updateSelectedCount();
            });
        }

        // Row click to toggle (exclude quantity input)
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
        opt.width = 900;
        return opt;
    }
}
