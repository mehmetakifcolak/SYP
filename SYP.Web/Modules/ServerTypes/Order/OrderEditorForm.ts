import { DateEditor, DecimalEditor, EnumEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, ServiceLookupEditor, StringEditor } from "@serenity-is/corelib";
import { OrderStatus } from "./OrderStatus";

export interface OrderEditorForm {
    OrderNumber: StringEditor;
    CustomerId: ServiceLookupEditor;
    ManagerUserId: LookupEditor;
    Status: EnumEditor;
    OrderDate: DateEditor;
    TotalAmount: DecimalEditor;
    DiscountPercentage: DecimalEditor;
    DiscountAmount: DecimalEditor;
    NetAmount: DecimalEditor;
    CurrencyId: LookupEditor;
    Notes: StringEditor;
    RejectReason: StringEditor;
    InsertDate: DateEditor;
    InsertUserId: IntegerEditor;
    UpdateDate: DateEditor;
    UpdateUserId: IntegerEditor;
}

export class OrderEditorForm extends PrefixedContext {
    static readonly formKey = 'Order.OrderEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!OrderEditorForm.init) {
            OrderEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = ServiceLookupEditor;
            var w2 = LookupEditor;
            var w3 = EnumEditor;
            var w4 = DateEditor;
            var w5 = DecimalEditor;
            var w6 = IntegerEditor;

            initFormType(OrderEditorForm, [
                'OrderNumber', w0,
                'CustomerId', w1,
                'ManagerUserId', w2,
                'Status', w3,
                'OrderDate', w4,
                'TotalAmount', w5,
                'DiscountPercentage', w5,
                'DiscountAmount', w5,
                'NetAmount', w5,
                'CurrencyId', w2,
                'Notes', w0,
                'RejectReason', w0,
                'InsertDate', w4,
                'InsertUserId', w6,
                'UpdateDate', w4,
                'UpdateUserId', w6
            ]);
        }
    }
}

[OrderStatus]; // referenced types