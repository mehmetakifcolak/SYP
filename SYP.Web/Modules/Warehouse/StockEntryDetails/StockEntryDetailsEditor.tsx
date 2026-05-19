import { Decorators, getLookupAsync, Lookup, notifySuccess, confirmDialog } from "@serenity-is/corelib";
import { GridEditorBase } from "@serenity-is/extensions";
import { StockEntryDetailsColumns, StockEntryDetailsRow } from "../../ServerTypes/Warehouse";
import { StockEntryDetailsEditDialog } from "./StockEntryDetailsEditDialog";
import { ProductsRow } from "../../ServerTypes/Catalog";
import { CurrencyListRow } from "../../ServerTypes/Setting";
import { BulkProductSelectionDialog, BulkProductItem } from "../Common/BulkProductSelectionDialog";
import { ExcelProductImportDialog } from "../Common/ExcelProductImportDialog";

@Decorators.registerEditor("SYP.Warehouse.StockEntryDetailsEditor")
export class StockEntryDetailsEditor extends GridEditorBase<StockEntryDetailsRow> {
    protected getColumnsKey() { return StockEntryDetailsColumns.columnsKey; }
    protected getDialogType() { return StockEntryDetailsEditDialog; }
    protected getLocalTextPrefix() { return StockEntryDetailsRow.localTextPrefix; }

    protected createSlickGrid(): Slick.Grid {
        const grid = super.createSlickGrid();

        // Currency kolonunu düzenlenebilir yap
        const columns = grid.getColumns();
        const currencyCol = columns.find((c: any) => c.field === 'Currency');
        if (currencyCol) {
            (currencyCol as any).editor = 'LookupEditor';
            (currencyCol as any).editorOptions = {
                lookupKey: CurrencyListRow.lookupKey,
                idField: 'Code',
                textField: 'Code'
            };
        }

        // Actions kolonu ekle
        columns.push({
            field: 'Actions',
            name: '',
            title: 'İşlemler',
            width: 100,
            minWidth: 100,
            maxWidth: 100,
            formatter: (ctx: any) => {
                const container = document.createElement('div');
                container.style.display = 'flex';
                container.style.gap = '8px';
                container.style.justifyContent = 'center';

                // Düzenle butonu
                const editBtn = document.createElement('a');
                editBtn.className = 'inline-action edit';
                editBtn.title = 'Düzenle';
                editBtn.style.cursor = 'pointer';
                editBtn.innerHTML = '<i class="fa fa-pencil text-blue"></i>';
                container.appendChild(editBtn);

                // Sil butonu
                const deleteBtn = document.createElement('a');
                deleteBtn.className = 'inline-action delete';
                deleteBtn.title = 'Sil';
                deleteBtn.style.cursor = 'pointer';
                deleteBtn.innerHTML = '<i class="fa fa-trash text-red"></i>';
                container.appendChild(deleteBtn);

                return container;
            }
        } as any);

        grid.setColumns(columns);
        return grid;
    }

    protected onClick(e: Event, row: number, cell: number): void {
        super.onClick(e, row, cell);

        const target = e.target as HTMLElement;
        if (target.closest('.inline-action.edit')) {
            e.preventDefault();
            const item = this.view.getItem(row);
            this.editItem(item);
        }
        else if (target.closest('.inline-action.delete')) {
            e.preventDefault();
            const item = this.view.getItem(row);
            confirmDialog('Bu satırı silmek istediğinizden emin misiniz?', () => {
                this.deleteEntity(item[this.getIdProperty()]);
                notifySuccess('Satır silindi.');
            });
        }
    }

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
                    const newRow: StockEntryDetailsRow = {
                        ProductId: item.ProductId,
                        ProductCode: item.ProductCode,
                        ProductName: item.ProductName,
                        Quantity: item.Quantity,
                        Unit: item.Unit,
                        Currency: item.Currency || '',
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
                            unit = product.UnitName;
                            currency = product.CurrencyCode;
                            vatRate = product.VatRate;
                            unitPrice = product.UnitPrice;
                        }
                    }

                    const newRow: StockEntryDetailsRow = {
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

    protected validateEntity(row: StockEntryDetailsRow, id: number): boolean {
        if (!super.validateEntity(row, id)) {
            return false;
        }

        // Ürün bilgilerini lookup'tan al
        if (row.ProductId && this.productLookup) {
            const product = this.productLookup.itemById[row.ProductId];
            if (product) {
                row.ProductCode = product.Code;
                row.ProductName = product.Name;
                row.Unit = product.UnitName;
                row.Currency = product.CurrencyCode;
                row.VatRate = product.VatRate;
                row.UnitPrice = product.UnitPrice;
            }
        }

        return true;
    }
}
