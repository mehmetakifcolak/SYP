import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators, TabsExtensions } from '@serenity-is/corelib';
import { ProductsForm, ProductsRow, ProductsService } from '../../ServerTypes/Catalog';
import { ProductPriceListsGrid } from './ProductPriceListsGrid';

@Decorators.registerClass('SYP.Catalog.ProductsDialog')
export class ProductsDialog extends DialogBase<ProductsRow, any> {
    protected getFormKey() { return ProductsForm.formKey; }
    protected getRowDefinition() { return ProductsRow; }
    protected getService() { return ProductsService.baseUrl; }

    protected form = new ProductsForm(this.idPrefix);
    private priceListsGrid: ProductPriceListsGrid;

    protected afterLoadEntity() {
        super.afterLoadEntity();

        // Fiyat listeleri grid'ini güncelle
        if (this.priceListsGrid && this.entityId) {
            this.priceListsGrid.setProductId(this.entityId as number);
        }
    }

    protected arrange(): void {
        super.arrange();

        // Fiyat Listeleri tab'ını ekle (sadece düzenleme modunda)
        if (this.isEditMode() && !this.priceListsGrid) {
            const tabs = this.findById("PropertyGrid")?.closest(".s-PropertyGrid")?.querySelector(".category-links");
            if (tabs) {
                // Tab oluştur
                const tabContent = document.createElement("div");
                tabContent.id = this.idPrefix + "TabPriceLists";
                tabContent.className = "tab-pane s-property-items";
                tabContent.innerHTML = '<div id="' + this.idPrefix + 'PriceListsGrid"></div>';

                const tabPane = this.findById("PropertyGrid")?.closest(".s-PropertyGrid")?.querySelector(".tab-content");
                tabPane?.appendChild(tabContent);

                // Tab link oluştur
                const tabLink = document.createElement("li");
                tabLink.className = "nav-item";
                tabLink.innerHTML = '<a class="nav-link" href="#' + this.idPrefix + 'TabPriceLists" data-bs-toggle="tab">Fiyat Listeleri</a>';
                tabs.appendChild(tabLink);

                // Grid'i oluştur
                this.priceListsGrid = new ProductPriceListsGrid({
                    element: "#" + this.idPrefix + "PriceListsGrid",
                    productId: this.entityId as number
                });
            }
        }
    }
}