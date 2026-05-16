import { Decorators, getLookupAsync } from "@serenity-is/corelib";
import { GridEditorDialog } from "@serenity-is/extensions";
import { PriceListItemsForm, PriceListItemsRow } from "../../ServerTypes/Catalog";
import { ProductsRow } from "../../ServerTypes/Catalog";

@Decorators.registerClass("SYP.Catalog.PriceListItemsEditDialog")
export class PriceListItemsEditDialog extends GridEditorDialog<PriceListItemsRow> {
    protected getFormKey() { return PriceListItemsForm.formKey; }
    protected getLocalTextPrefix() { return PriceListItemsRow.localTextPrefix; }

    protected form = new PriceListItemsForm(this.idPrefix);

    constructor(props?: any) {
        super(props);

        // Ürün seçildiğinde ürün bilgilerini doldur
        this.form.ProductId.changeSelect2(async () => {
            const productId = this.form.ProductId.value;
            if (productId) {
                const lookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);
                const product = lookup.itemById[productId];
                if (product) {
                    this.entity.ProductCode = product.Code;
                    this.entity.ProductName = product.Name;

                    // Ürünün varsayılan fiyatını getir
                    if (product.UnitPrice && !this.form.UnitPrice.value) {
                        this.form.UnitPrice.value = product.UnitPrice;
                    }
                }
            }
        });
    }

    protected getDialogTitle(): string {
        return this.isNew() ? "Yeni Ürün Fiyatı Ekle" : "Ürün Fiyatı Düzenle";
    }
}
