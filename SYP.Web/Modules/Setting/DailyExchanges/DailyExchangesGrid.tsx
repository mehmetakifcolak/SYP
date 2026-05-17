import { Decorators, ToolButton, notifySuccess, notifyError, notifyWarning, EntityGrid} from '@serenity-is/corelib';
import { DailyExchangesColumns, DailyExchangesRow, DailyExchangesService } from '../../ServerTypes/Setting';
import { DailyExchangesDialog } from './DailyExchangesDialog';

@Decorators.registerClass('SYP.Setting.DailyExchangesGrid')
export class DailyExchangesGrid extends EntityGrid<DailyExchangesRow, any> {
    protected getColumnsKey() { return DailyExchangesColumns.columnsKey; }
    protected getDialogType() { return DailyExchangesDialog; }
    protected getRowDefinition() { return DailyExchangesRow; }
    protected getService() { return DailyExchangesService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getButtons(): ToolButton[] {
        var buttons = super.getButtons();

        buttons.push({
            title: 'Gunluk Kur Cek',
            cssClass: 'export-xlsx-button',
            icon: 'fa-refresh',
            onClick: () => this.fetchTodayRates()
        });

        buttons.push({
            title: 'Gecmis Kur Cek',
            cssClass: 'export-xlsx-button',
            icon: 'fa-calendar',
            onClick: () => this.showDateRangePicker()
        });

        return buttons;
    }

    private fetchTodayRates() {
        DailyExchangesService.FetchTodayRates({}, () => {
            notifySuccess("Gunluk kur verileri basariyla cekildi.");
            this.refresh();
        }, {
            onError: (response: any) => {
                notifyError("Hata: " + (response?.Error?.Message || "Bilinmeyen hata"));
            }
        } as any);
    }

    private showDateRangePicker() {
        var dialog = document.createElement('div');
        dialog.style.cssText = 'position:fixed;top:0;left:0;width:100%;height:100%;background:rgba(0,0,0,0.5);z-index:10000;display:flex;align-items:center;justify-content:center;';

        var card = document.createElement('div');
        card.style.cssText = 'background:#fff;border-radius:8px;padding:24px;min-width:350px;box-shadow:0 4px 20px rgba(0,0,0,0.3);';

        card.innerHTML = `
            <h3 style="margin:0 0 16px;font-size:16px;">TCMB Gecmis Kur Verileri</h3>
            <div style="margin-bottom:12px;">
                <label style="display:block;margin-bottom:4px;font-weight:600;">Baslangic Tarihi</label>
                <input type="date" id="importStartDate" style="width:100%;padding:8px;border:1px solid #ccc;border-radius:4px;" />
            </div>
            <div style="margin-bottom:16px;">
                <label style="display:block;margin-bottom:4px;font-weight:600;">Bitis Tarihi</label>
                <input type="date" id="importEndDate" style="width:100%;padding:8px;border:1px solid #ccc;border-radius:4px;" />
            </div>
            <div style="display:flex;gap:8px;justify-content:flex-end;">
                <button id="importCancelBtn" style="padding:8px 16px;border:1px solid #ccc;border-radius:4px;background:#f5f5f5;cursor:pointer;">Iptal</button>
                <button id="importSubmitBtn" style="padding:8px 16px;border:none;border-radius:4px;background:#1976d2;color:#fff;cursor:pointer;">Verileri Cek</button>
            </div>
        `;

        dialog.appendChild(card);
        document.body.appendChild(dialog);

        dialog.addEventListener('click', (e) => {
            if (e.target === dialog) {
                document.body.removeChild(dialog);
            }
        });

        card.querySelector('#importCancelBtn')!.addEventListener('click', () => {
            document.body.removeChild(dialog);
        });

        card.querySelector('#importSubmitBtn')!.addEventListener('click', () => {
            var startInput = card.querySelector('#importStartDate') as HTMLInputElement;
            var endInput = card.querySelector('#importEndDate') as HTMLInputElement;

            if (!startInput.value || !endInput.value) {
                notifyWarning("Lutfen baslangic ve bitis tarihlerini seciniz.");
                return;
            }

            var submitBtn = card.querySelector('#importSubmitBtn') as HTMLButtonElement;
            submitBtn.disabled = true;
            submitBtn.textContent = 'Cekiliyor...';

            DailyExchangesService.ImportDateRange({
                StartDate: startInput.value,
                EndDate: endInput.value
            } as any, () => {
                document.body.removeChild(dialog);
                notifySuccess("Kur verileri basariyla ice aktarildi.");
                this.refresh();
            }, {
                onError: (response: any) => {
                    submitBtn.disabled = false;
                    submitBtn.textContent = 'Verileri Cek';
                    notifyError("Hata: " + (response?.Error?.Message || "Bilinmeyen hata"));
                }
            } as any);
        });
    }
}
