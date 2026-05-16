import { Decorators, getLookup } from "@serenity-is/corelib";
import { GridEditorBase } from "@serenity-is/extensions";
import { StockExitDetailsColumns, StockExitDetailsRow } from "../../ServerTypes/Warehouse";
import { StockExitDetailsEditDialog } from "./StockExitDetailsEditDialog";
import { ProductsRow } from "../../ServerTypes/Catalog";
import { BulkProductSelectionDialog } from "../Common/BulkProductSelectionDialog";

@Decorators.registerEditor("SYP.Warehouse.StockExitDetailsEditor")
export class StockExitDetailsEditor extends GridEditorBase<StockExitDetailsRow> {
    protected getColumnsKey() { return StockExitDetailsColumns.columnsKey; }
    protected getDialogType() { return StockExitDetailsEditDialog; }
    protected getLocalTextPrefix() { return StockExitDetailsRow.localTextPrefix; }

    constructor(props: any) {
        super(props);
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

        return buttons;
    }

    private openBulkProductDialog(): void {
        // Mevcut ürün ID'lerini al
        const existingProductIds = this.getItems().map(x => x.ProductId).filter(x => x != null) as number[];

        new BulkProductSelectionDialog({
            excludeProductIds: existingProductIds,
            onSelect: (products) => {
                for (const product of products) {
                    const newRow: StockExitDetailsRow = {
                        ProductId: product.Id,
                        ProductCode: product.Code,
                        ProductName: product.Name,
                        Quantity: 1
                    };

                    const id = this.getNextId();
                    newRow[this.getIdProperty()] = id;
                    this.view.addItem(newRow);
                }

                this.view.refresh();
            }
        }).dialogOpen();
    }

    protected validateEntity(row: StockExitDetailsRow, id: number): boolean {
        if (!super.validateEntity(row, id)) {
            return false;
        }

        // Ürün bilgilerini lookup'tan al
        if (row.ProductId) {
            const product = getLookup<ProductsRow>(ProductsRow.lookupKey).itemById[row.ProductId];
            if (product) {
                row.ProductCode = product.Code;
                row.ProductName = product.Name;
            }
        }

        return true;
    }
}
