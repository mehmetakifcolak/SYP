import { Decorators, getLookupAsync, Lookup } from "@serenity-is/corelib";
import { GridEditorDialog } from "@serenity-is/extensions";
import { StockEntryDetailsForm, StockEntryDetailsRow } from "../../ServerTypes/Warehouse";
import { ProductsRow } from "../../ServerTypes/Catalog";

@Decorators.registerClass("SYP.Warehouse.StockEntryDetailsEditDialog")
export class StockEntryDetailsEditDialog extends GridEditorDialog<StockEntryDetailsRow> {
    protected getFormKey() { return StockEntryDetailsForm.formKey; }
    protected getLocalTextPrefix() { return StockEntryDetailsRow.localTextPrefix; }

    protected form = new StockEntryDetailsForm(this.idPrefix);
    private productLookup: Lookup<ProductsRow>;

    constructor(props?: any) {
        super(props);

        // Lookup'ı önceden yükle
        getLookupAsync<ProductsRow>(ProductsRow.lookupKey).then(lookup => {
            this.productLookup = lookup;
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
                }
            }
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
            }
        }

        return true;
    }
}
