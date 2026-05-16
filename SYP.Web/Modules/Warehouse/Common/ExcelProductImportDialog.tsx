import { Decorators, DialogButton, TemplatedDialog, getLookupAsync, notifyError, notifySuccess, notifyWarning, Lookup } from "@serenity-is/corelib";
import { ProductsRow } from "../../ServerTypes/Catalog";
import * as XLSX from 'xlsx';

export interface ExcelProductImportDialogOptions {
    onImport?: (items: { ProductId: number; ProductCode: string; ProductName: string; Quantity: number }[]) => void;
    excludeProductIds?: number[];
}

export interface ExcelRowData {
    productCode: string;
    quantity: number;
    rowIndex: number;
}

@Decorators.registerClass("SYP.Warehouse.ExcelProductImportDialog")
export class ExcelProductImportDialog extends TemplatedDialog<ExcelProductImportDialogOptions> {
    private fileInput: HTMLInputElement;
    private previewDiv: HTMLElement;
    private productLookup: Lookup<ProductsRow>;
    private parsedData: ExcelRowData[] = [];
    private validItems: { ProductId: number; ProductCode: string; ProductName: string; Quantity: number }[] = [];

    constructor(opt?: ExcelProductImportDialogOptions) {
        super(opt);
        this.dialogTitle = "Excel ile Ürün Yükle";
    }

    protected getTemplate(): string {
        return `
            <div class="excel-import" style="padding: 15px;">
                <div class="alert alert-info mb-3">
                    <strong>Excel Formatı:</strong> İlk sütun <b>Ürün Kodu</b>, ikinci sütun <b>Miktar</b> olmalıdır.
                    <br/>İlk satır başlık satırı olarak kabul edilir ve atlanır.
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

                <div id="~_Preview" class="preview-area" style="max-height: 300px; overflow-y: auto; border: 1px solid #ddd; padding: 10px; display: none;">
                </div>
            </div>
        `;
    }

    protected async onDialogOpen(): Promise<void> {
        super.onDialogOpen();

        this.fileInput = this.byId("FileInput")?.getNode() as HTMLInputElement;
        this.previewDiv = this.byId("Preview")?.getNode() as HTMLElement;

        // Lookup'ı yükle
        this.productLookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);

        // Dosya seçildiğinde
        if (this.fileInput) {
            this.fileInput.addEventListener("change", () => this.handleFileSelect());
        }

        // Şablon indirme
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
        this.validItems = [];

        const excludeIds = new Set(this.options?.excludeProductIds || []);

        // İlk satırı atla (başlık)
        for (let i = 1; i < data.length; i++) {
            const row = data[i];
            if (!row || row.length < 2) continue;

            const productCode = String(row[0] || '').trim();
            const quantity = parseFloat(row[1]) || 0;

            if (!productCode || quantity <= 0) continue;

            this.parsedData.push({
                productCode,
                quantity,
                rowIndex: i + 1
            });
        }

        // Ürün kodlarını lookup'tan bul
        for (const item of this.parsedData) {
            const product = this.productLookup.items.find(p =>
                p.Code?.toLowerCase() === item.productCode.toLowerCase()
            );

            if (product && !excludeIds.has(product.Id!)) {
                this.validItems.push({
                    ProductId: product.Id!,
                    ProductCode: product.Code!,
                    ProductName: product.Name!,
                    Quantity: item.quantity
                });
            }
        }
    }

    private showPreview(): void {
        if (!this.previewDiv) return;

        this.previewDiv.style.display = 'block';

        if (this.parsedData.length === 0) {
            this.previewDiv.innerHTML = '<div class="text-warning">Excel dosyasında geçerli veri bulunamadı.</div>';
            return;
        }

        let html = `<div class="mb-2"><strong>Toplam ${this.parsedData.length} satır okundu, ${this.validItems.length} ürün eşleşti.</strong></div>`;

        html += '<table class="table table-sm table-bordered mb-0">';
        html += '<thead><tr><th>Satır</th><th>Ürün Kodu</th><th>Miktar</th><th>Durum</th></tr></thead>';
        html += '<tbody>';

        for (const item of this.parsedData) {
            const product = this.productLookup.items.find(p =>
                p.Code?.toLowerCase() === item.productCode.toLowerCase()
            );

            const isValid = product != null;
            const statusClass = isValid ? 'text-success' : 'text-danger';
            const statusText = isValid ? `✓ ${product.Name}` : '✗ Ürün bulunamadı';

            html += `<tr class="${isValid ? '' : 'table-danger'}">
                <td>${item.rowIndex}</td>
                <td>${item.productCode}</td>
                <td>${item.quantity}</td>
                <td class="${statusClass}">${statusText}</td>
            </tr>`;
        }

        html += '</tbody></table>';

        if (this.validItems.length === 0) {
            html += '<div class="alert alert-warning mt-2 mb-0">Eşleşen ürün bulunamadı. Lütfen ürün kodlarını kontrol edin.</div>';
        }

        this.previewDiv.innerHTML = html;
    }

    private doImport(): void {
        if (this.validItems.length === 0) {
            notifyWarning("İçe aktarılacak geçerli ürün bulunamadı.");
            return;
        }

        this.options?.onImport?.(this.validItems);
        notifySuccess(`${this.validItems.length} ürün başarıyla eklendi.`);
        this.dialogClose();
    }

    private downloadTemplate(): void {
        const templateData = [
            ['Ürün Kodu', 'Miktar'],
            ['URUN001', 10],
            ['URUN002', 5],
            ['URUN003', 20]
        ];

        const ws = XLSX.utils.aoa_to_sheet(templateData);
        const wb = XLSX.utils.book_new();
        XLSX.utils.book_append_sheet(wb, ws, 'Ürünler');

        // Sütun genişlikleri
        ws['!cols'] = [{ wch: 15 }, { wch: 10 }];

        XLSX.writeFile(wb, 'urun_yukleme_sablonu.xlsx');
    }

    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 700;
        return opt;
    }
}
