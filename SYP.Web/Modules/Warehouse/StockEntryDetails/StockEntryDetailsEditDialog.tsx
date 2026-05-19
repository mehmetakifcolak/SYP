import { Decorators, getLookupAsync, Lookup } from "@serenity-is/corelib";
import { GridEditorDialog } from "@serenity-is/extensions";
import { StockEntryDetailsForm, StockEntryDetailsRow } from "../../ServerTypes/Warehouse";
import { ProductsRow } from "../../ServerTypes/Catalog";
import { CurrencyListRow } from "../../ServerTypes/Setting";

@Decorators.registerClass("SYP.Warehouse.StockEntryDetailsEditDialog")
export class StockEntryDetailsEditDialog extends GridEditorDialog<StockEntryDetailsRow> {
    protected getFormKey() { return StockEntryDetailsForm.formKey; }
    protected getLocalTextPrefix() { return StockEntryDetailsRow.localTextPrefix; }

    protected form = new StockEntryDetailsForm(this.idPrefix);
    private productLookup: Lookup<ProductsRow>;
    private currencyLookup: Lookup<CurrencyListRow>;

    constructor(props?: any) {
        super(props);

        // Product Lookup'ı önceden yükle
        getLookupAsync<ProductsRow>(ProductsRow.lookupKey).then(lookup => {
            this.productLookup = lookup;
        });

        // Currency Lookup'ı önceden yükle ve select2 oluştur
        getLookupAsync<CurrencyListRow>(CurrencyListRow.lookupKey).then(lookup => {
            this.currencyLookup = lookup;
            this.setupCurrencySelect();
        });

        // Ürün seçildiğinde kod ve adı doldur
        this.form.ProductId.changeSelect2(async e => {
            const productId = this.form.ProductId.value;
            if (productId) {
                const lookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);
                const product = lookup.itemById[productId];
                if (product) {
                    (this.entity as any).ProductCode = product.Code;
                    (this.entity as any).ProductName = product.Name;

                    // Para birimi doldur
                    if (product.CurrencyCode) {
                        (window as any).$(this.form.Currency.domNode).val(product.CurrencyCode).trigger('change');
                    }

                    // Alış fiyat listesinden fiyat çek
                    await this.loadPurchasePrice(productId);
                }
            }
        });
    }

    private async loadPurchasePrice(productId: number): Promise<void> {
        try {
            // Alış fiyat listesi (Type=2, IsActive=true, IsDefault=true) için fiyat çek
            const response = await (window as any).serviceCall({
                service: 'Catalog/PriceListItems/List',
                request: {
                    Criteria: [
                        ['ProductId', '=', productId],
                        ['PriceListType', '=', 2], // Purchase
                        ['PriceListIsActive', '=', true],
                        ['PriceListIsDefault', '=', true]
                    ],
                    Take: 1
                }
            });

            if (response.Entities && response.Entities.length > 0) {
                const priceItem = response.Entities[0];
                this.form.UnitPrice.value = priceItem.UnitPrice;
            } else {
                // Fiyat bulunamadı - boş bırak
                this.form.UnitPrice.value = null;
            }
        } catch (error) {
            console.error('Alış fiyatı yüklenirken hata:', error);
            this.form.UnitPrice.value = null;
        }
    }

    private setupCurrencySelect(): void {
        if (!this.currencyLookup) return;

        const currencyData = this.currencyLookup.items.map(item => ({
            id: item.Code,
            text: item.Code
        }));

        // Select2 oluştur
        const $currency = (window as any).$(this.form.Currency.domNode);
        $currency.select2({
            data: currencyData,
            placeholder: 'Para Birimi Seçin',
            allowClear: true
        });

        // Value değiştiğinde entity'yi güncelle
        $currency.on('change', (e: any) => {
            this.form.Currency.value = $currency.val() as string;
        });
    }

    protected getDialogTitle(): string {
        return this.isNew() ? "Yeni Ürün Ekle" : "Ürün Düzenle";
    }

    protected async updateInterface(): Promise<void> {
        super.updateInterface();

        // Ürün seçiliyse ve kod/ad boşsa doldur
        const productId = this.form.ProductId.value;
        if (productId && (!(this.entity as any).ProductCode || !(this.entity as any).ProductName)) {
            const lookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);
            const product = lookup.itemById[productId];
            if (product) {
                (this.entity as any).ProductCode = product.Code;
                (this.entity as any).ProductName = product.Name;

                // Para birimi ve fiyat bilgilerini doldur
                if (product.Currency && !this.form.Currency.value) {
                    this.form.Currency.value = product.Currency;
                }
                if (product.UnitPrice != null && !this.form.UnitPrice.value) {
                    this.form.UnitPrice.value = product.UnitPrice;
                }
            }
        }
    }

    protected beforeSave(): boolean {
        if (!super.beforeSave()) {
            return false;
        }

        // Birim fiyat kontrolü
        const unitPrice = this.form.UnitPrice.value;
        if (!unitPrice || unitPrice <= 0) {
            alert('Birim fiyat zorunludur! Seçtiğiniz ürün için alış fiyat listesinde fiyat tanımlı değil. Lütfen önce fiyat listesine ekleyiniz.');
            return false;
        }

        // Ürün bilgilerini ekle
        const productId = this.form.ProductId.value;
        if (productId && this.productLookup) {
            const product = this.productLookup.itemById[productId];
            if (product) {
                this.entity.ProductCode = product.Code;
                this.entity.ProductName = product.Name;

                // Para birimi ve fiyat form'dan alındığı için entity'ye yazalım
                this.entity.Currency = this.form.Currency.value;
                this.entity.UnitPrice = this.form.UnitPrice.value;
            }
        }

        return true;
    }
}
