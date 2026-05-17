import { GridEditorBase } from '@/Common/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { OrderDetailEditorColumns, OrderDetailRow } from '../../ServerTypes/Order';
import { OrderDetailEditorDialog } from './OrderDetailEditorDialog';
import { ProductsRow } from '../../../ServerTypes/Catalog';
import { CustomersRow } from '../../../ServerTypes/Customer';
import { VendorTypeRow } from '../../../ServerTypes/Setting';
import { BulkProductSelectionDialog, BulkProductItem } from '../../../Warehouse/Common/BulkProductSelectionDialog';
import { ExcelProductImportDialog } from '../../../Warehouse/Common/ExcelProductImportDialog';
declare var $: any;

@Decorators.registerEditor('SYP.Order.OrderDetailGridEditor')
export class OrderDetailGridEditor extends GridEditorBase<OrderDetailRow> {
    protected getColumnsKey() { return OrderDetailEditorColumns.columnsKey; }
    protected getDialogType() { return OrderDetailEditorDialog; }
    protected getRowDefinition() { return OrderDetailRow; }

    private productLookup: Lookup<ProductsRow>;
    private customerLookup: Lookup<CustomersRow>;
    private vendorTypeLookup: Lookup<VendorTypeRow>;

    constructor(props: any) {
        super(props);

        // Lookup'ları önceden yükle
        getLookupAsync<ProductsRow>(ProductsRow.lookupKey).then(lookup => {
            this.productLookup = lookup;
        });
        getLookupAsync<CustomersRow>(CustomersRow.lookupKey).then(lookup => {
            this.customerLookup = lookup;
        });
        getLookupAsync<VendorTypeRow>(VendorTypeRow.lookupKey).then(lookup => {
            this.vendorTypeLookup = lookup;
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
            onSelect: async (items: BulkProductItem[]) => {
                for (const item of items) {
                    const newRow: OrderDetailRow = {
                        ProductId: item.ProductId,
                        Quantity: item.Quantity,
                        UnitId: undefined,
                        UnitPrice: item.UnitPrice || 0,
                        VatRateId: item.VatRate,
                        VatRate: 0,
                        Discount: 0,
                        LineTotal: 0,
                        Notes: ''
                    };

                    // Ürün bilgilerini ve iskonto hesaplamasını yap
                    await this.fillOrderDetailFromProduct(newRow);

                    // Benzersiz ID oluştur (negatif sayı kullanarak)
                    const items = this.getItems();
                    const id = items.length > 0 ? -(Date.now() + Math.random()) : -1;
                    (newRow as any).__id = id;
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
            onImport: async (items) => {
                for (const item of items) {
                    const newRow: OrderDetailRow = {
                        ProductId: item.ProductId,
                        Quantity: item.Quantity,
                        UnitId: undefined,
                        UnitPrice: 0,
                        VatRateId: undefined,
                        VatRate: 0,
                        Discount: 0,
                        LineTotal: 0,
                        Notes: ''
                    };

                    // Ürün bilgilerini ve iskonto hesaplamasını yap
                    await this.fillOrderDetailFromProduct(newRow);

                    // Benzersiz ID oluştur (negatif sayı kullanarak)
                    const currentItems = this.getItems();
                    const id = currentItems.length > 0 ? -(Date.now() + Math.random()) : -1;
                    (newRow as any).__id = id;
                    this.view.addItem(newRow);
                }

                this.view.refresh();
            }
        });
        dlg.dialogOpen();
    }

    private async fillOrderDetailFromProduct(row: OrderDetailRow): Promise<void> {
        if (!row.ProductId) return;

        // Lookup'ları yükle (henüz yüklenmediyse)
        if (!this.productLookup) {
            this.productLookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);
        }
        if (!this.customerLookup) {
            this.customerLookup = await getLookupAsync<CustomersRow>(CustomersRow.lookupKey);
        }
        if (!this.vendorTypeLookup) {
            this.vendorTypeLookup = await getLookupAsync<VendorTypeRow>(VendorTypeRow.lookupKey);
        }

        const product = this.productLookup.itemById[row.ProductId];
        if (!product) return;

        // Birim fiyatı güncel fiyattan al
        row.UnitPrice = product.CurrentValidPrice || product.UnitPrice || 0;

        // Unit ID'yi al
        if (product.UnitId) {
            row.UnitId = product.UnitId;
        }

        // VAT bilgilerini al
        if (product.VatRateId) {
            row.VatRateId = product.VatRateId;
        }
        if (product.VatRate != null) {
            row.VatRate = product.VatRate;
        }

        // İskonto hesapla
        await this.calculateDiscount(row);

        // Line Total hesapla
        this.calculateLineTotal(row);
    }

    protected async validateEntity(row: OrderDetailRow, id: number): Promise<boolean> {
        if (!await super.validateEntity(row, id)) {
            return false;
        }

        // Ürün bilgilerini lookup'tan al
        if (row.ProductId && this.productLookup) {
            const product = this.productLookup.itemById[row.ProductId];
            if (product) {
                // Birim fiyatı güncel fiyattan al (eğer henüz set edilmemişse)
                if (!row.UnitPrice || row.UnitPrice === 0) {
                    row.UnitPrice = product.CurrentValidPrice || product.UnitPrice || 0;
                }

                // Unit ID'yi al (eğer henüz set edilmemişse)
                if (!row.UnitId && product.UnitId) {
                    row.UnitId = product.UnitId;
                }

                // VAT bilgilerini al (eğer henüz set edilmemişse)
                if (!row.VatRateId && product.VatRateId) {
                    row.VatRateId = product.VatRateId;
                }
                if (!row.VatRate && product.VatRate != null) {
                    row.VatRate = product.VatRate;
                }

                // İskonto hesapla (eğer henüz set edilmemişse)
                if (!row.Discount || row.Discount === 0) {
                    await this.calculateDiscount(row);
                }

                // Line Total hesapla
                this.calculateLineTotal(row);
            }
        }

        return true;
    }

    private async calculateDiscount(row: OrderDetailRow): Promise<void> {
        try {
            if (!this.customerLookup || !this.vendorTypeLookup) {
                return;
            }

            // Parent entity'den customer ID'yi al
            let customerId: number | null = null;

            // Form elementini bul (jQuery ile)
            const element = (this as any).element;
            if (element) {
                const $form = $(element).closest('.s-Form, form');
                if ($form.length > 0) {
                    const $customerInput = $form.find('[name="CustomerId"]');
                    if ($customerInput.length > 0) {
                        const val = $customerInput.val();
                        if (val) {
                            customerId = parseInt(val);
                        }
                    }
                }
            }

            if (!customerId || isNaN(customerId)) {
                return;
            }

            const customer = this.customerLookup.itemById[customerId];
            if (customer?.VendorTypeId) {
                const vendorType = this.vendorTypeLookup.itemById[customer.VendorTypeId];

                if (vendorType?.DiscountValue) {
                    // İskonto değerini hesapla
                    if (vendorType.DiscountType === 'Percentage' || vendorType.DiscountType === '%') {
                        // Yüzde ise, birim fiyat * miktar * iskonto oranı
                        const quantity = row.Quantity || 0;
                        const unitPrice = row.UnitPrice || 0;
                        row.Discount = (quantity * unitPrice * vendorType.DiscountValue) / 100;
                    } else {
                        // Sabit tutar ise
                        row.Discount = vendorType.DiscountValue;
                    }
                }
            }
        } catch (error) {
            console.error('Error calculating discount:', error);
            // Hata olursa iskonto 0 kalır
        }
    }

    private calculateLineTotal(row: OrderDetailRow): void {
        const quantity = row.Quantity || 0;
        const unitPrice = row.UnitPrice || 0;
        const discount = row.Discount || 0;

        row.LineTotal = (quantity * unitPrice) - discount;
    }
}