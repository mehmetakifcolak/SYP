import { Decorators, EntityDialog, serviceRequest, getLookupAsync } from "@serenity-is/corelib";
import { StockEntriesForm, StockEntriesRow, StockEntriesService, WarehousesRow } from "../../ServerTypes/Warehouse";
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

            // Varsayılan depoyu seç
            if (!this.form.WarehouseId.value) {
                this.setDefaultWarehouse();
            }
        }
    }

    private async setDefaultWarehouse() {
        const lookup = await getLookupAsync<WarehousesRow>(WarehousesRow.lookupKey);
        const defaultWarehouse = lookup.items.find(w => w.IsDefault && w.IsActive);
        if (defaultWarehouse) {
            this.form.WarehouseId.value = defaultWarehouse.Id;
        }
    }
}
