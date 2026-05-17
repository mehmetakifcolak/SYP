import { DialogBase } from '@/_Ext/Bases/DialogBase';
import { Decorators, notifyError } from '@serenity-is/corelib';
import { PriceListsForm, PriceListsRow, PriceListsService } from '../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Catalog.PriceListsDialog')
@Decorators.maximizable()
export class PriceListsDialog extends DialogBase<PriceListsRow, any> {
    protected getFormKey() { return PriceListsForm.formKey; }
    protected getRowDefinition() { return PriceListsRow; }
    protected getService() { return PriceListsService.baseUrl; }

    protected form = new PriceListsForm(this.idPrefix);

    constructor(props?: any) {
        super(props);

        // Tarih değişikliklerinde validasyon
        this.form.ValidTo.change(() => {
            this.validateDateRange();
        });

        this.form.ValidFrom.change(() => {
            this.validateDateRange();
        });
    }

    protected afterLoadEntity(): void {
        super.afterLoadEntity();

        // Yeni kayıt için default değerler
        if (this.isNew()) {
            const today = new Date();
            this.form.ValidFrom.valueAsDate = today;

            // 1 yıl sonrası
            const nextYear = new Date(today);
            nextYear.setFullYear(nextYear.getFullYear() + 1);
            this.form.ValidTo.valueAsDate = nextYear;

            // Default aktif
            this.form.IsActive.value = 1;
        }
    }

    private validateDateRange(): boolean {
        const validFrom = this.form.ValidFrom.valueAsDate;
        const validTo = this.form.ValidTo.valueAsDate;

        if (validFrom && validTo && validTo < validFrom) {
            notifyError("Geçerlilik bitiş tarihi, başlangıç tarihinden küçük olamaz!");
            return false;
        }

        return true;
    }

    protected validateBeforeSave(): boolean {
        if (!super.validateBeforeSave()) {
            return false;
        }

        return this.validateDateRange();
    }

    protected getDialogOptions() {
        const opt = super.getDialogOptions();
        opt.width = 900;
        return opt;
    }
}