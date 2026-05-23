import { Decorators, DialogButton, TemplatedDialog, getLookupAsync, notifyError, notifySuccess, notifyWarning, Lookup, serviceCall } from "@serenity-is/corelib";
import { ProductsRow, ProductsService, ProductCategoryRow, BrandsRow } from "../../ServerTypes/Catalog";
import { UnitsRow, CurrencyListRow, VatRatesRow } from "../../ServerTypes/Setting";
import { ProductPackingRow } from "../../ServerTypes/Catalog";
import * as XLSX from 'xlsx';

export interface ProductsExcelImportDialogOptions {
    onImportComplete?: () => void;
}

export interface ExcelRowData {
    code: string;
    name: string;
    name2?: string;
    description?: string;
    barcode?: string;
    unitPrice?: number;
    categoryName?: string;
    brandName?: string;
    unitName?: string;
    currencyCode?: string;
    vatRateName?: string;
    packingName?: string;
    rowIndex: number;
}

export interface ParsedProduct {
    rowIndex: number;
    code: string;
    name: string;
    name2?: string;
    description?: string;
    barcode?: string;
    unitPrice?: number;
    categoryId?: number;
    brandId?: number;
    unitId?: number;
    currencyId?: number;
    vatRateId?: number;
    packingId?: number;
    isValid: boolean;
    errors: string[];
}

@Decorators.registerClass("SYP.Catalog.ProductsExcelImportDialog")
export class ProductsExcelImportDialog extends TemplatedDialog<ProductsExcelImportDialogOptions> {
    private fileInput: HTMLInputElement;
    private previewDiv: HTMLElement;

    // Lookups
    private categoryLookup: Lookup<ProductCategoryRow>;
    private brandLookup: Lookup<BrandsRow>;
    private unitLookup: Lookup<UnitsRow>;
    private currencyLookup: Lookup<CurrencyListRow>;
    private vatRateLookup: Lookup<VatRatesRow>;
    private packingLookup: Lookup<ProductPackingRow>;

    private parsedData: ExcelRowData[] = [];
    private validProducts: ParsedProduct[] = [];
    private invalidProducts: ParsedProduct[] = [];

    constructor(opt?: ProductsExcelImportDialogOptions) {
        super(opt);
        this.dialogTitle = "Excel ile Ürün Toplu İçe Aktarma";
    }

    protected getTemplate(): string {
        return `
            <div class="excel-import" style="padding: 15px;">
                <div class="alert alert-info mb-3">
                    <strong>Excel Formatı (Sütun Sırası):</strong><br/>
                    1. <b>Ürün Kodu</b> (boş bırakılırsa otomatik oluşturulur)<br/>
                    2. <b>Ürün Adı</b> (zorunlu)<br/>
                    3. <b>İkinci İsim</b> (opsiyonel)<br/>
                    4. <b>Açıklama</b> (opsiyonel)<br/>
                    5. <b>Barkod</b> (opsiyonel)<br/>
                    6. <b>Birim Fiyat</b> (opsiyonel)<br/>
                    7. <b>Kategori</b> (kategori adı)<br/>
                    8. <b>Marka</b> (marka adı)<br/>
                    9. <b>Birim</b> (birim adı)<br/>
                    10. <b>Para Birimi</b> (kod: TRY, USD, vb.)<br/>
                    11. <b>KDV Oranı</b> (oran adı)<br/>
                    12. <b>Ambalaj</b> (ambalaj adı, opsiyonel)<br/>
                    <br/>
                    <small>İlk satır başlık satırı olarak kabul edilir ve atlanır.</small>
                </div>

                <div class="mb-3">
                    <label class="form-label">Excel Dosyası Seçin (.xlsx, .xls)</label>
                    <input type="file" id="~_FileInput" class="form-control" accept=".xlsx,.xls" />
                </div>

                <div class="mb-3">
                    <a href="javascript:;" id="~_DownloadTemplate" class="btn btn-sm btn-outline-secondary">
                        <i class="fa fa-download"></i> Örnek Şablon İndir
                    </a>
                </div>

                <div id="~_Preview" class="preview-area" style="max-height: 400px; overflow-y: auto; border: 1px solid #ddd; padding: 10px; display: none;">
                </div>
            </div>
        `;
    }

    protected async onDialogOpen(): Promise<void> {
        super.onDialogOpen();

        this.fileInput = this.byId("FileInput")?.getNode() as HTMLInputElement;
        this.previewDiv = this.byId("Preview")?.getNode() as HTMLElement;

        // Load all lookups
        this.categoryLookup = await getLookupAsync<ProductCategoryRow>("Catalog.ProductCategory");
        this.brandLookup = await getLookupAsync<BrandsRow>("Catalog.Brands");
        this.unitLookup = await getLookupAsync<UnitsRow>("Setting.Units");
        this.currencyLookup = await getLookupAsync<CurrencyListRow>("Setting.CurrencyList");
        this.vatRateLookup = await getLookupAsync<VatRatesRow>("Setting.VatRates");
        this.packingLookup = await getLookupAsync<ProductPackingRow>("Catalog.ProductPacking");

        // File selection handler
        if (this.fileInput) {
            this.fileInput.addEventListener("change", () => this.handleFileSelect());
        }

        // Template download
        const downloadBtn = this.byId("DownloadTemplate")?.getNode();
        if (downloadBtn) {
            downloadBtn.addEventListener("click", () => this.downloadTemplate());
        }
    }

    protected getDialogButtons(): DialogButton[] {
        return [
            {
                text: "İçe Aktar",
                cssClass: "btn btn-primary",
                click: () => this.doImport()
            },
            {
                text: "İptal",
                cssClass: "btn btn-default",
                click: () => this.dialogClose()
            }
        ];
    }

    private async handleFileSelect(): Promise<void> {
        const file = this.fileInput.files?.[0];
        if (!file) return;

        try {
            const data = await this.readFile(file);
            const workbook = XLSX.read(data, { type: 'array' });
            const firstSheet = workbook.Sheets[workbook.SheetNames[0]];
            const jsonData = XLSX.utils.sheet_to_json<any>(firstSheet, { header: 1 });

            this.parseExcelData(jsonData);
            this.showPreview();
        } catch (e) {
            notifyError("Excel dosyası okunamadı: " + (e as Error).message);
        }
    }

    private readFile(file: File): Promise<ArrayBuffer> {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onload = (e) => resolve(e.target?.result as ArrayBuffer);
            reader.onerror = (e) => reject(e);
            reader.readAsArrayBuffer(file);
        });
    }

    private parseExcelData(data: any[][]): void {
        this.parsedData = [];
        this.validProducts = [];
        this.invalidProducts = [];

        // Skip header row (row 0)
        for (let i = 1; i < data.length; i++) {
            const row = data[i];
            if (!row || row.length === 0) continue;

            const excelRow: ExcelRowData = {
                code: this.getCellValue(row[0]),
                name: this.getCellValue(row[1]),
                name2: this.getCellValue(row[2]),
                description: this.getCellValue(row[3]),
                barcode: this.getCellValue(row[4]),
                unitPrice: this.parseDecimal(row[5]),
                categoryName: this.getCellValue(row[6]),
                brandName: this.getCellValue(row[7]),
                unitName: this.getCellValue(row[8]),
                currencyCode: this.getCellValue(row[9]),
                vatRateName: this.getCellValue(row[10]),
                packingName: this.getCellValue(row[11]),
                rowIndex: i + 1
            };

            // Must have at least a name
            if (!excelRow.name) continue;

            this.parsedData.push(excelRow);
        }

        // Validate and map to products
        for (const excelRow of this.parsedData) {
            const product = this.validateAndMapProduct(excelRow);

            if (product.isValid) {
                this.validProducts.push(product);
            } else {
                this.invalidProducts.push(product);
            }
        }
    }

    private getCellValue(cell: any): string {
        if (cell === null || cell === undefined) return '';
        return String(cell).trim();
    }

    private parseDecimal(value: any): number | undefined {
        if (value === null || value === undefined || value === '') return undefined;
        const num = parseFloat(value);
        return isNaN(num) ? undefined : num;
    }

    private validateAndMapProduct(excelRow: ExcelRowData): ParsedProduct {
        const product: ParsedProduct = {
            rowIndex: excelRow.rowIndex,
            code: excelRow.code,
            name: excelRow.name,
            name2: excelRow.name2,
            description: excelRow.description,
            barcode: excelRow.barcode,
            unitPrice: excelRow.unitPrice,
            isValid: true,
            errors: []
        };

        // Category lookup
        if (excelRow.categoryName) {
            const category = this.categoryLookup.items.find(c =>
                c.Name?.toLowerCase() === excelRow.categoryName.toLowerCase()
            );
            if (category) {
                product.categoryId = category.Id;
            } else {
                product.errors.push(`Kategori bulunamadı: ${excelRow.categoryName}`);
            }
        }

        // Brand lookup
        if (excelRow.brandName) {
            const brand = this.brandLookup.items.find(b =>
                b.Name?.toLowerCase() === excelRow.brandName.toLowerCase()
            );
            if (brand) {
                product.brandId = brand.Id;
            } else {
                product.errors.push(`Marka bulunamadı: ${excelRow.brandName}`);
            }
        }

        // Unit lookup
        if (excelRow.unitName) {
            const unit = this.unitLookup.items.find(u =>
                u.Name?.toLowerCase() === excelRow.unitName.toLowerCase()
            );
            if (unit) {
                product.unitId = unit.Id;
            } else {
                product.errors.push(`Birim bulunamadı: ${excelRow.unitName}`);
            }
        }

        // Currency lookup
        if (excelRow.currencyCode) {
            const currency = this.currencyLookup.items.find(c =>
                c.Code?.toLowerCase() === excelRow.currencyCode.toLowerCase()
            );
            if (currency) {
                product.currencyId = currency.Id;
            } else {
                product.errors.push(`Para birimi bulunamadı: ${excelRow.currencyCode}`);
            }
        }

        // VAT Rate lookup
        if (excelRow.vatRateName) {
            const vatRate = this.vatRateLookup.items.find(v =>
                v.Name?.toLowerCase() === excelRow.vatRateName.toLowerCase()
            );
            if (vatRate) {
                product.vatRateId = vatRate.Id;
            } else {
                product.errors.push(`KDV oranı bulunamadı: ${excelRow.vatRateName}`);
            }
        }

        // Packing lookup (optional)
        if (excelRow.packingName) {
            const packing = this.packingLookup.items.find(p =>
                p.Name?.toLowerCase() === excelRow.packingName.toLowerCase()
            );
            if (packing) {
                product.packingId = packing.Id;
            } else {
                product.errors.push(`Ambalaj bulunamadı: ${excelRow.packingName}`);
            }
        }

        product.isValid = product.errors.length === 0;
        return product;
    }

    private showPreview(): void {
        if (!this.previewDiv) return;

        this.previewDiv.style.display = 'block';

        if (this.parsedData.length === 0) {
            this.previewDiv.innerHTML = '<div class="text-warning">Excel dosyasında geçerli veri bulunamadı.</div>';
            return;
        }

        let html = `<div class="mb-2">
            <strong>Toplam ${this.parsedData.length} satır okundu.</strong><br/>
            <span class="text-success">${this.validProducts.length} ürün geçerli</span>,
            <span class="text-danger">${this.invalidProducts.length} ürün hatalı</span>
        </div>`;

        html += '<table class="table table-sm table-bordered mb-0">';
        html += '<thead><tr><th>Satır</th><th>Kod</th><th>Ürün Adı</th><th>Fiyat</th><th>Durum</th></tr></thead>';
        html += '<tbody>';

        const allProducts = [...this.validProducts, ...this.invalidProducts]
            .sort((a, b) => a.rowIndex - b.rowIndex);

        for (const product of allProducts) {
            const statusClass = product.isValid ? 'text-success' : 'text-danger';
            const statusText = product.isValid
                ? '✓ Geçerli'
                : '✗ ' + product.errors.join(', ');
            const codeDisplay = product.code || '&lt;otomatik&gt;';
            const priceDisplay = product.unitPrice ? product.unitPrice.toFixed(2) : '-';

            html += `<tr class="${product.isValid ? '' : 'table-danger'}">
                <td>${product.rowIndex}</td>
                <td>${codeDisplay}</td>
                <td>${product.name}</td>
                <td class="text-right">${priceDisplay}</td>
                <td class="${statusClass}">${statusText}</td>
            </tr>`;
        }

        html += '</tbody></table>';

        if (this.validProducts.length === 0) {
            html += '<div class="alert alert-warning mt-2 mb-0">İçe aktarılabilir geçerli ürün bulunamadı.</div>';
        }

        this.previewDiv.innerHTML = html;
    }

    private async doImport(): Promise<void> {
        if (this.validProducts.length === 0) {
            notifyWarning("İçe aktarılacak geçerli ürün bulunamadı.");
            return;
        }

        try {
            const request = {
                Products: this.validProducts.map(p => ({
                    RowIndex: p.rowIndex,
                    Code: p.code,
                    Name: p.name,
                    Name2: p.name2,
                    Description: p.description,
                    Barcode: p.barcode,
                    UnitPrice: p.unitPrice,
                    CategoryId: p.categoryId,
                    BrandId: p.brandId,
                    UnitId: p.unitId,
                    CurrencyId: p.currencyId,
                    VatRateId: p.vatRateId,
                    PackingId: p.packingId
                }))
            };

            const response = await serviceCall({
                service: ProductsService.baseUrl + '/BulkImportProducts',
                request: request
            }) as any;

            if (response.ErrorCount > 0) {
                let errorHtml = `<div class="mb-2">${response.SuccessCount} ürün başarıyla içe aktarıldı.</div>`;
                errorHtml += `<div class="mb-2 text-danger">${response.ErrorCount} ürün eklenirken hata oluştu:</div>`;
                errorHtml += '<ul>';
                for (const error of response.Errors) {
                    errorHtml += `<li>Satır ${error.RowIndex} (${error.ProductCode}): ${error.ErrorMessage}</li>`;
                }
                errorHtml += '</ul>';
                notifyWarning(errorHtml, '', { timeOut: 0, extendedTimeOut: 0 });
            } else {
                notifySuccess(`${response.SuccessCount} ürün başarıyla içe aktarıldı.`);
            }

            this.options?.onImportComplete?.();
            this.dialogClose();
        } catch (e) {
            notifyError("İçe aktarma sırasında hata oluştu: " + (e as Error).message);
        }
    }

    private downloadTemplate(): void {
        const templateData = [
            [
                'Ürün Kodu',
                'Ürün Adı',
                'İkinci İsim',
                'Açıklama',
                'Barkod',
                'Birim Fiyat',
                'Kategori',
                'Marka',
                'Birim',
                'Para Birimi',
                'KDV Oranı',
                'Ambalaj'
            ],
            [
                '',  // Boş bırakılırsa otomatik
                'Örnek Ürün 1',
                'Sample Product 1',
                'Ürün açıklaması',
                '1234567890123',
                150.50,
                'Kategori Adı',
                'Marka Adı',
                'Adet',
                'TRY',
                'KDV 20',
                ''
            ],
            [
                'URUN002',
                'Örnek Ürün 2',
                '',
                '',
                '',
                280.00,
                'Kategori Adı',
                'Marka Adı',
                'Kg',
                'USD',
                'KDV 20',
                'Ambalaj Adı'
            ]
        ];

        const ws = XLSX.utils.aoa_to_sheet(templateData);
        const wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'Ürünler');

        // Column widths
        ws['!cols'] = [
            { wch: 15 },  // Code
            { wch: 25 },  // Name
            { wch: 25 },  // Name2
            { wch: 30 },  // Description
            { wch: 18 },  // Barcode
            { wch: 12 },  // UnitPrice
            { wch: 20 },  // Category
            { wch: 20 },  // Brand
            { wch: 12 },  // Unit
            { wch: 12 },  // Currency
            { wch: 12 },  // VatRate
            { wch: 15 }   // Packing
        ];

        XLSX.writeFile(wb, 'urun_toplu_yukle_sablonu.xlsx');
    }

    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 900;
        return opt;
    }
}
