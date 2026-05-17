import { DecimalEditor, initFormType, LookupEditor, PrefixedContext, ServiceLookupEditor, StringEditor } from "@serenity-is/corelib";

export interface OrderDetailForm {
    OrderId: ServiceLookupEditor;
    ProductId: LookupEditor;
    Quantity: DecimalEditor;
    UnitId: LookupEditor;
    UnitPrice: DecimalEditor;
    VatRateId: LookupEditor;
    VatRate: DecimalEditor;
    Discount: DecimalEditor;
    LineTotal: DecimalEditor;
    Notes: StringEditor;
}

export class OrderDetailForm extends PrefixedContext {
    static readonly formKey = 'Order.OrderDetail';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!OrderDetailForm.init) {
            OrderDetailForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = LookupEditor;
            var w2 = DecimalEditor;
            var w3 = StringEditor;

            initFormType(OrderDetailForm, [
                'OrderId', w0,
                'ProductId', w1,
                'Quantity', w2,
                'UnitId', w1,
                'UnitPrice', w2,
                'VatRateId', w1,
                'VatRate', w2,
                'Discount', w2,
                'LineTotal', w2,
                'Notes', w3
            ]);
        }
    }
}