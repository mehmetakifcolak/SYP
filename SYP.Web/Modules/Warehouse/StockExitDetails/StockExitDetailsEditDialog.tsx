import { Decorators, getLookupAsync, Lookup } from "@serenity-is/corelib";
import { GridEditorDialog } from "@serenity-is/extensions";
import { StockExitDetailsForm, StockExitDetailsRow } from "../../ServerTypes/Warehouse";
import { ProductsRow } from "../../ServerTypes/Catalog";

@Decorators.registerClass("SYP.Warehouse.StockExitDetailsEditDialog")
export class StockExitDetailsEditDialog extends GridEditorDialog<StockExitDetailsRow> {
    protected getFormKey() { return StockExitDetailsForm.formKey; }
    protected getLocalTextPrefix() { return StockExitDetailsRow.localTextPrefix; }

    protected form = new StockExitDetailsForm(this.idPrefix);
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
