import { DateEditor, DecimalEditor, EnumEditor, initFormType, LookupEditor, PrefixedContext, ServiceLookupEditor, StringEditor, TextAreaEditor } from "@serenity-is/corelib";
import { OrderStatus } from "./OrderStatus";

export interface OrderForm {
    OrderNumber: StringEditor;
    CustomerId: ServiceLookupEditor;
    ManagerUserId: LookupEditor;
    OrderDate: DateEditor;
    Status: EnumEditor;
    CurrencyId: LookupEditor;
    TotalAmount: DecimalEditor;
    DiscountPercentage: DecimalEditor;
    DiscountAmount: DecimalEditor;
    NetAmount: DecimalEditor;
    DetailList: StringEditor;
    Notes: TextAreaEditor;
    RejectReason: TextAreaEditor;
}

export class OrderForm extends PrefixedContext {
    static readonly formKey = 'Order.Order';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!OrderForm.init) {
            OrderForm.init = true;

            var w0 = StringEditor;
            var w1 = ServiceLookupEditor;
            var w2 = LookupEditor;
            var w3 = DateEditor;
            var w4 = EnumEditor;
            var w5 = DecimalEditor;
            var w6 = TextAreaEditor;

            initFormType(OrderForm, [
                'OrderNumber', w0,
                'CustomerId', w1,
                'ManagerUserId', w2,
                'OrderDate', w3,
                'Status', w4,
                'CurrencyId', w2,
                'TotalAmount', w5,
                'DiscountPercentage', w5,
                'DiscountAmount', w5,
                'NetAmount', w5,
                'DetailList', w0,
                'Notes', w6,
                'RejectReason', w6
            ]);
        }
    }
}

[OrderStatus]; // referenced types