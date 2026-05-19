import { Decorators, EntityDialog, getLookupAsync } from '@serenity-is/corelib';
import { OrderForm, OrderRow, OrderService } from '../../ServerTypes/Order';
import { CustomersRow } from '../../ServerTypes/Customer';
import { VendorTypeRow } from '../../ServerTypes/Setting';
import '../OrderDetail/Editor/OrderDetailEditorDialog';
import '../OrderDetail/Editor/OrderDetailGridEditor';

@Decorators.registerClass('SYP.Order.OrderDialog')
export class OrderDialog extends EntityDialog<OrderRow, any> {
    protected getFormKey() { return OrderForm.formKey; }
    protected getRowDefinition() { return OrderRow; }
    protected getService() { return OrderService.baseUrl; }

    protected form = new OrderForm(this.idPrefix);

    constructor(props?: any) {
        super(props);

        // Bayi se�ildi�inde iskonto bilgilerini g�ncelle
        this.form.CustomerId.changeSelect2(async e => {
            await this.updateDiscountPercentage();
        });
    }

    private async updateDiscountPercentage(): Promise<void> {
        const customerId = this.form.CustomerId.value;
        if (!customerId) {
            this.form.DiscountPercentage.value = 0;
            return;
        }

        try {
            // Customer lookup'�n� al
            const customerLookup = await getLookupAsync<CustomersRow>(CustomersRow.lookupKey);
            const customer = customerLookup.itemById[customerId];

            if (customer?.VendorTypeId) {
                // VendorType lookup'�n� al
                const vendorTypeLookup = await getLookupAsync<VendorTypeRow>(VendorTypeRow.lookupKey);
                const vendorType = vendorTypeLookup.itemById[customer.VendorTypeId];

                if (vendorType?.DiscountValue && vendorType.DiscountType === 'Percentage') {
                    this.form.DiscountPercentage.value = vendorType.DiscountValue;
                } else {
                    this.form.DiscountPercentage.value = 0;
                }
            }
        } catch (error) {
            console.error('Error updating discount percentage:', error);
        }
    }

    protected afterLoadEntity(): void {
        super.afterLoadEntity();

        // Yeni kayıt için varsayılan değerleri set et
        if (this.isNew()) {
            // Bugünün tarihi ve saati
            this.entity.OrderDate = new Date().toISOString();
        }

        // Entity y�klendikten sonra iskonto bilgilerini g�ncelle
        if (this.entity.CustomerId) {
            this.updateDiscountPercentage();
        }
    }
}