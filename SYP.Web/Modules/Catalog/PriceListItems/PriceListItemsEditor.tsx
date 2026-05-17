import { Decorators, getLookupAsync, Lookup } from "@serenity-is/corelib";
import { GridEditorBase } from "@serenity-is/extensions";
import { PriceListItemsColumns, PriceListItemsRow } from "../../ServerTypes/Catalog";
import { ProductsRow } from "../../ServerTypes/Catalog";
import { PriceListItemsEditDialog } from "./PriceListItemsEditDialog";
import { BulkPriceDialog } from "./BulkPriceDialog";
import { ExcelPriceListImportDialog } from "./ExcelPriceListImportDialog";

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

        buttons.push({
            title: "Excel ile Yükle",
            cssClass: "import-excel-button",
            icon: "fa-file-excel",
            onClick: () => this.openExcelImportDialog()
        });

        return buttons;
    }

    private openBulkPriceDialog(): void {
        // Duplicate kontrolü kaldırıldı - kullanıcı aynı ürünü farklı fiyatlarla ekleyebilir

        const dlg = new BulkPriceDialog({
            excludeProductIds: [],
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

    private openExcelImportDialog(): void {
        // Duplicate kontrolü kaldırıldı - kullanıcı aynı ürünü farklı fiyatlarla ekleyebilir

        const dlg = new ExcelPriceListImportDialog({
            excludeProductIds: [],
            onImport: (items) => {
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
