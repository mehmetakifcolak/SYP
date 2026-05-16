import { Decorators, getLookupAsync, Lookup } from "@serenity-is/corelib";
import { GridEditorBase } from "@serenity-is/extensions";
import { StockExitDetailsColumns, StockExitDetailsRow } from "../../ServerTypes/Warehouse";
import { StockExitDetailsEditDialog } from "./StockExitDetailsEditDialog";
import { ProductsRow } from "../../ServerTypes/Catalog";
import { BulkProductSelectionDialog, BulkProductItem } from "../Common/BulkProductSelectionDialog";
import { ExcelProductImportDialog } from "../Common/ExcelProductImportDialog";

@Decorators.registerEditor("SYP.Warehouse.StockExitDetailsEditor")
export class StockExitDetailsEditor extends GridEditorBase<StockExitDetailsRow> {
    protected getColumnsKey() { return StockExitDetailsColumns.columnsKey; }
    protected getDialogType() { return StockExitDetailsEditDialog; }
    protected getLocalTextPrefix() { return StockExitDetailsRow.localTextPrefix; }

    private productLookup: Lookup<ProductsRow>;

    constructor(props: any) {
        super(props);

        // Lookup'ı önceden yükle
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
            title: "Toplu Ürün Ekle",
            cssClass: "add-bulk-button",
            icon: "fa-list",
            onClick: () => this.openBulkProductDialog()
        });

        buttons.push({
            title: "Excel ile Yükle",
            cssClass: "excel-import-button",
            icon: "fa-file-excel-o",
            onClick: () => this.openExcelImportDialog()
        });

        return buttons;
    }

    private openBulkProductDialog(): void {
        // Mevcut ürün ID'lerini al
        const existingProductIds = this.getItems().map(x => x.ProductId).filter(x => x != null) as number[];

        const dlg = new BulkProductSelectionDialog({
            excludeProductIds: existingProductIds,
            onSelect: (items: BulkProductItem[]) => {
                for (const item of items) {
                    const newRow: StockExitDetailsRow = {
                        ProductId: item.ProductId,
                        ProductCode: item.ProductCode,
                        ProductName: item.ProductName,
                        Quantity: item.Quantity,
                        Unit: item.Unit,
                        Currency: item.Currency,
                        VatRate: item.VatRate,
                        UnitPrice: item.UnitPrice
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
        const existingProductIds = this.getItems().map(x => x.ProductId).filter(x => x != null) as number[];

        const dlg = new ExcelProductImportDialog({
            excludeProductIds: existingProductIds,
            onImport: (items) => {
                for (const item of items) {
                    // Lookup'tan ürün bilgilerini al
                    let productCode = item.ProductCode;
                    let productName = item.ProductName;
                    let unit: string | undefined;
                    let currency: string | undefined;
                    let vatRate: number | undefined;
                    let unitPrice: number | undefined;

                    if (this.productLookup && item.ProductId) {
                        const product = this.productLookup.itemById[item.ProductId];
                        if (product) {
                            productCode = product.Code || productCode;
                            productName = product.Name || productName;
                            unit = product.Unit;
                            currency = product.Currency;
                            vatRate = product.VatRate;
                            unitPrice = product.UnitPrice;
                        }
                    }

                    const newRow: StockExitDetailsRow = {
                        ProductId: item.ProductId,
                        ProductCode: productCode,
                        ProductName: productName,
                        Quantity: item.Quantity,
                        Unit: unit,
                        Currency: currency,
                        VatRate: vatRate,
                        UnitPrice: unitPrice
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

    protected validateEntity(row: StockExitDetailsRow, id: number): boolean {
        if (!super.validateEntity(row, id)) {
            return false;
        }

        // Ürün bilgilerini lookup'tan al
        if (row.ProductId && this.productLookup) {
            const product = this.productLookup.itemById[row.ProductId];
            if (product) {
                row.ProductCode = product.Code;
                row.ProductName = product.Name;
                row.Unit = product.Unit;
                row.Currency = product.Currency;
                row.VatRate = product.VatRate;
                row.UnitPrice = product.UnitPrice;
            }
        }

        return true;
    }
}
