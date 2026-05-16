import { Decorators, getLookup } from "@serenity-is/corelib";
import { GridEditorDialog } from "@serenity-is/extensions";
import { StockEntryDetailsForm, StockEntryDetailsRow } from "../../ServerTypes/Warehouse";
import { ProductsRow } from "../../ServerTypes/Catalog";

@Decorators.registerClass("SYP.Warehouse.StockEntryDetailsEditDialog")
export class StockEntryDetailsEditDialog extends GridEditorDialog<StockEntryDetailsRow> {
    protected getFormKey() { return StockEntryDetailsForm.formKey; }
    protected getLocalTextPrefix() { return StockEntryDetailsRow.localTextPrefix; }

    protected form = new StockEntryDetailsForm(this.idPrefix);

    constructor(props?: any) {
        super(props);

        // Ürün seçildiğinde kod ve adı doldur
        this.form.ProductId.changeSelect2(e => {
            const productId = this.form.ProductId.value;
            if (productId) {
                const product = getLookup<ProductsRow>(ProductsRow.lookupKey).itemById[productId];
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

    protected updateInterface(): void {
        super.updateInterface();

        // Ürün seçiliyse ve kod/ad boşsa doldur
        const productId = this.form.ProductId.value;
        if (productId && (!(this.entity as any).ProductCode || !(this.entity as any).ProductName)) {
            const product = getLookup<ProductsRow>(ProductsRow.lookupKey).itemById[productId];
            if (product) {
                (this.entity as any).ProductCode = product.Code;
                (this.entity as any).ProductName = product.Name;
            }
        }
    }
}
