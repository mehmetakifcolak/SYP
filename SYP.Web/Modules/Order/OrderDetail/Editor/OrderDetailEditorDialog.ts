import { Decorators, getLookupAsync } from '@serenity-is/corelib';
import { OrderDetailEditorForm, OrderDetailRow } from '../../../ServerTypes/Order';
import { GridEditorDialog } from '@serenity-is/extensions';
import { ProductsRow } from '../../../ServerTypes/Catalog';

@Decorators.registerClass('SYP.Order.OrderDetailEditorDialog')
export class OrderDetailEditorDialog extends GridEditorDialog<OrderDetailRow> {
    protected getFormKey() { return OrderDetailEditorForm.formKey; }
    protected getRowDefinition() { return OrderDetailRow; }

    protected form = new OrderDetailEditorForm(this.idPrefix);

    constructor(props?: any) {
        super(props);

        // Ürün seçildiğinde birim fiyat ve diğer bilgileri doldur
        this.form.ProductId.changeSelect2(async e => {
            const productId = this.form.ProductId.value;
            if (productId) {
                const lookup = await getLookupAsync<ProductsRow>(ProductsRow.lookupKey);
                const product = lookup.itemById[productId];
                if (product) {
                    // Birim fiyatı güncel fiyattan al
                    const unitPrice = product.CurrentValidPrice || product.UnitPrice || 0;
                    this.form.UnitPrice.value = unitPrice;

                    // Unit ID'yi al
                    if (product.UnitId) {
                        this.form.UnitId.value = product.UnitId;
                    }

                    // VAT bilgilerini al
                    if (product.VatRateId) {
                        this.form.VatRateId.value = product.VatRateId;
                    }
                    if (product.VatRate != null) {
                        this.form.VatRate.value = product.VatRate;
                    }

                    // İskonto değerini bayi tipinden al
                    await this.updateDiscountFromCustomer();

                    // Line Total hesapla
                    this.calculateLineTotal();
                }
            }
        });

        // Miktar veya birim fiyat değiştiğinde toplam hesapla
        this.form.Quantity.change(e => {
            this.calculateLineTotal();
        });

        this.form.UnitPrice.change(e => {
            this.calculateLineTotal();
        });

        this.form.Discount.change(e => {
            this.calculateLineTotal();
        });
    }

    private async updateDiscountFromCustomer(): Promise<void> {
        try {
            // Parent form'dan customer ID'yi al
            let customerId: number | null = null;

            // Grid editor içindeyse parent form'u bul
            const parentForm = this.element?.closest('.s-DialogContent')?.parent()?.find('[name="CustomerId"]');
            if (parentForm && parentForm.length > 0) {
                const customerIdVal = parentForm.val();
                if (customerIdVal) {
                    customerId = parseInt(customerIdVal as string);
                }
            }

            if (!customerId || isNaN(customerId)) {
                return;
            }

            // Customer lookup'ını al
            const customerLookup = await getLookupAsync<CustomersRow>(CustomersRow.lookupKey);
            const customer = customerLookup.itemById[customerId];

            if (customer?.VendorTypeId) {
                // VendorType lookup'ını al
                const vendorTypeLookup = await getLookupAsync<VendorTypeRow>(VendorTypeRow.lookupKey);
                const vendorType = vendorTypeLookup.itemById[customer.VendorTypeId];

                if (vendorType?.DiscountValue) {
                    // İskonto değerini set et (percentage veya fixed amount olabilir)
                    if (vendorType.DiscountType === 'Percentage' || vendorType.DiscountType === '%') {
                        // Yüzde ise, birim fiyat * miktar * iskonto oranı
                        const quantity = this.form.Quantity.value || 0;
                        const unitPrice = this.form.UnitPrice.value || 0;
                        const discountAmount = (quantity * unitPrice * vendorType.DiscountValue) / 100;
                        this.form.Discount.value = discountAmount;
                    } else {
                        // Sabit tutar ise
                        this.form.Discount.value = vendorType.DiscountValue;
                    }
                }
            }
        } catch (error) {
            console.error('Error updating discount from customer:', error);
        }
    }

    private calculateLineTotal(): void {
        const quantity = this.form.Quantity.value || 0;
        const unitPrice = this.form.UnitPrice.value || 0;
        const discount = this.form.Discount.value || 0;

        const lineTotal = (quantity * unitPrice) - discount;
        this.form.LineTotal.value = lineTotal;
    }
}