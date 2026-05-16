import { Decorators, getLookupAsync, Lookup } from "@serenity-is/corelib";
import { GridEditorBase } from "@serenity-is/extensions";
import { PriceListItemsColumns, PriceListItemsRow } from "../../ServerTypes/Catalog";
import { ProductsRow } from "../../ServerTypes/Catalog";
import { PriceListItemsEditDialog } from "./PriceListItemsEditDialog";
import { BulkPriceDialog } from "./BulkPriceDialog";

@Decorators.registerEditor("SYP.Catalog.PriceListItemsEditor")
export class PriceListItemsEditor extends GridEditorBase<PriceListItemsRow> {
    protected getColumnsKey() { return PriceListItemsColumns.columnsKey; }
    protected getDialogType() { return PriceListItemsEditDialog; }
    protected getLocalTextPrefix() { return PriceListItemsRow.localTextPrefix; }

    private productLookup: Lookup<ProductsRow>;

    constructor(props: any) {
        super(props);

        getLookupAsync<ProductsRow>(ProductsRow.lookupKey).then(lookup => {
            this.productLookup = lookup;
        });
    }

    protected getAddButtonCaption() {
        return "Ürün Ekle";
    }

    protected getButtons() {
        const buttons = super.getButtons();

        buttons.push({
            title: "Toplu Fiyat Ekle",
            cssClass: "add-bulk-button",
            icon: "fa-list",
            onClick: () => this.openBulkPriceDialog()
        });

        return buttons;
    }

    private openBulkPriceDialog(): void {
        const existingProductIds = this.getItems().map(x => x.ProductId).filter(x => x != null) as number[];

        const dlg = new BulkPriceDialog({
            excludeProductIds: existingProductIds,
            onSelect: (items) => {
                for (const item of items) {
                    const newRow: PriceListItemsRow = {
                        ProductId: item.ProductId,
                        ProductCode: item.ProductCode,
                        ProductName: item.ProductName,
                        UnitPrice: item.UnitPrice,
                        DiscountRate: item.DiscountRate
                    };

                    const id = this.getNextId();
                    newRow[this.getIdProperty()] = id;
                    this.view.addItem(newRow);
                }

                this.view.refresh();
            }
        });
        dlg.dialogOpen();
    }

    protected validateEntity(row: PriceListItemsRow, id: number): boolean {
        if (!super.validateEntity(row, id)) {
            return false;
        }

        // Aynı ürün var mı kontrol et
        const items = this.getItems();
        const existingItem = items.find(x =>
            x.ProductId === row.ProductId &&
            x[this.getIdProperty()] !== id
        );

        if (existingItem) {
            alert("Bu ürün zaten listede mevcut!");
            return false;
        }

        // Ürün bilgilerini lookup'tan al
        if (row.ProductId && this.productLookup) {
            const product = this.productLookup.itemById[row.ProductId];
            if (product) {
                row.ProductCode = product.Code;
                row.ProductName = product.Name;
            }
        }

        return true;
    }
}
