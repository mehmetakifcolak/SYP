import { Decorators, getLookupAsync, Lookup } from "@serenity-is/corelib";
import { GridEditorDialog } from "@serenity-is/extensions";
import { StockExitDetailsForm, StockExitDetailsRow } from "../../ServerTypes/Warehouse";
import { ProductsRow } from "../../ServerTypes/Catalog";
import { CurrencyListRow } from "../../ServerTypes/Setting";

@Decorators.registerClass("SYP.Warehouse.StockExitDetailsEditDialog")
export class StockExitDetailsEditDialog extends GridEditorDialog<StockExitDetailsRow> {
    protected getFormKey() { return StockExitDetailsForm.formKey; }
    protected getLocalTextPrefix() { return StockExitDetailsRow.localTextPrefix; }

    protected form = new StockExitDetailsForm(this.idPrefix);
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

                    // Para birimi ve fiyat bilgilerini doldur
                    if (product.CurrencyCode) {
                        // Select2'ye değer set et
                        (window as any).$(this.form.Currency.domNode).val(product.CurrencyCode).trigger('change');
                    }
                    if (product.UnitPrice != null) {
                        this.form.UnitPrice.value = product.UnitPrice;
                    }
                }
            }
        });
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
                if (product.CurrencyCode && !this.form.Currency.value) {
                    this.form.Currency.value = product.CurrencyCode;
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
