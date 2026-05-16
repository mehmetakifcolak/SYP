import { Decorators, EntityDialog, serviceRequest } from "@serenity-is/corelib";
import { StockEntriesForm, StockEntriesRow, StockEntriesService } from "../../ServerTypes/Warehouse";
import { GetNextNumberResponse } from "../../ServerTypes/GetNextNumberResponse";

@Decorators.registerClass("SYP.Warehouse.StockEntriesDialog")
export class StockEntriesDialog extends EntityDialog<StockEntriesRow, any> {
    protected getFormKey() { return StockEntriesForm.formKey; }
    protected getRowDefinition() { return StockEntriesRow; }
    protected getService() { return StockEntriesService.baseUrl; }

    protected form = new StockEntriesForm(this.idPrefix);

    protected afterLoadEntity() {
        super.afterLoadEntity();

        // Yeni kayıt ise otomatik fiş numarası al
        if (this.isNew()) {
            serviceRequest<GetNextNumberResponse>(StockEntriesService.baseUrl + '/GetNextEntryNo', {}, response => {
                if (response && response.Serial) {
                    this.form.EntryNo.value = response.Serial;
                }
            });

            // Varsayılan tarih
            if (!this.form.EntryDate.value) {
                this.form.EntryDate.valueAsDate = new Date();
            }
        }
    }
}
