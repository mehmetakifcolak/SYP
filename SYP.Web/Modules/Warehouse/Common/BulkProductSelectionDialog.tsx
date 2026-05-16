import { Decorators, DialogButton, BaseDialog, getLookupAsync, WidgetProps, htmlEncode, Lookup } from "@serenity-is/corelib";
import { ProductsRow } from "../../ServerTypes/Catalog";

export interface BulkProductSelectionDialogOptions {
    onSelect?: (products: ProductsRow[]) => void;
    excludeProductIds?: number[];
}

@Decorators.registerClass("SYP.Warehouse.BulkProductSelectionDialog")
export class BulkProductSelectionDialog extends BaseDialog<BulkProductSelectionDialogOptions> {
    private selectedProducts: Map<number, ProductsRow> = new Map();
    private productListDiv: HTMLElement;
    private searchInput: HTMLInputElement;
    private selectedCountSpan: HTMLElement;
    private productLookup: Lookup<ProductsRow>;

    constructor(props: WidgetProps<BulkProductSelectionDialogOptions>) {
        super(props);
        this.dialogTitle = "Toplu Ürün Seçimi";
    }

    protected renderContents(): void {
        const content = this.findById("Content");
        if (!content) return;

        content.innerHTML = `
            <div class="bulk-product-selection" style="padding: 10px;">
                <div class="search-box mb-3">
                    <input type="text" class="form-control" placeholder="Ürün kodu veya adı ile arayın..." />
                </div>
                <div class="selected-count mb-2">
                    <span class="selected-count-number">0</span> ürün seçildi
                </div>
                <div class="product-list" style="max-height: 400px; overflow-y: auto; border: 1px solid #ddd; padding: 10px;">
                    <div class="text-muted">Ürünler yükleniyor...</div>
                </div>
            </div>
        `;

        this.searchInput = content.querySelector('.search-box input') as HTMLInputElement;
        this.productListDiv = content.querySelector('.product-list') as HTMLElement;
        this.selectedCountSpan = content.querySelector('.selected-count-number') as HTMLElement;

        if (this.searchInput) {
            this.searchInput.addEventListener("input", () => this.filterProducts());
        }

        this.loadProducts();
    }

    protected onDialogOpen(): void {
        super.onDialogOpen();
        this.renderContents();
    }

    protected getDialogButtons(): DialogButton[] {
        return [
            {
                text: "Seçilenleri Ekle",
                cssClass: "btn btn-primary",
                click: () => {
                    if (this.selectedProducts.size > 0) {
                        this.options.onSelect?.(Array.from(this.selectedProducts.values()));
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
        this.productLookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);
        const excludeIds = new Set(this.options.excludeProductIds || []);

        const products = this.productLookup.items.filter(p =>
            p.IsActive === 1 && !excludeIds.has(p.Id!)
        );

        this.renderProductList(products);
    }

    private filterProducts(): void {
        if (!this.productLookup) return;

        const searchTerm = this.searchInput?.value?.toLowerCase().trim() || '';
        const excludeIds = new Set(this.options.excludeProductIds || []);

        let products = this.productLookup.items.filter(p =>
            p.IsActive === 1 && !excludeIds.has(p.Id!)
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
        html += '<thead><tr><th style="width: 40px;"><input type="checkbox" class="select-all-checkbox" /></th><th>Ürün Kodu</th><th>Ürün Adı</th></tr></thead>';
        html += '<tbody>';

        for (const product of products) {
            const isChecked = this.selectedProducts.has(product.Id!);
            html += `<tr class="product-row" data-id="${product.Id}" style="cursor: pointer;">
                <td><input type="checkbox" class="product-checkbox" data-id="${product.Id}" ${isChecked ? 'checked' : ''} /></td>
                <td>${htmlEncode(product.Code || '')}</td>
                <td>${htmlEncode(product.Name || '')}</td>
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

                if (target.checked) {
                    this.selectedProducts.set(productId, product);
                } else {
                    this.selectedProducts.delete(productId);
                }

                this.updateSelectedCount();
                this.updateSelectAllCheckbox();
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

                    if (target.checked) {
                        this.selectedProducts.set(productId, product);
                    } else {
                        this.selectedProducts.delete(productId);
                    }
                });

                this.updateSelectedCount();
            });
        }

        // Row click to toggle
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
        opt.width = 600;
        return opt;
    }

    protected getTemplate(): string {
        return `<div id="~_Content"></div>`;
    }
}
