import { Decorators, EntityDialog, serviceRequest, getLookupAsync } from "@serenity-is/corelib";
import { StockExitsForm, StockExitsRow, StockExitsService, WarehousesRow } from "../../ServerTypes/Warehouse";
import { GetNextNumberResponse } from "../../ServerTypes/GetNextNumberResponse";

@Decorators.registerClass("SYP.Warehouse.StockExitsDialog")
export class StockExitsDialog extends EntityDialog<StockExitsRow, any> {
    protected getFormKey() { return StockExitsForm.formKey; }
    protected getRowDefinition() { return StockExitsRow; }
    protected getService() { return StockExitsService.baseUrl; }

    protected form = new StockExitsForm(this.idPrefix);

    protected afterLoadEntity() {
        super.afterLoadEntity();

        // Yeni kayıt ise otomatik fiş numarası al
        if (this.isNew()) {
            serviceRequest<GetNextNumberResponse>(StockExitsService.baseUrl + '/GetNextExitNo', {}, response => {
                if (response && response.Serial) {
                    this.form.ExitNo.value = response.Serial;
                }
            });

            // Varsayılan tarih
            if (!this.form.ExitDate.value) {
                this.form.ExitDate.valueAsDate = new Date();
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
