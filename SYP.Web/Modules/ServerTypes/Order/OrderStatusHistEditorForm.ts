import { DateEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, ServiceLookupEditor, StringEditor } from "@serenity-is/corelib";

export interface OrderStatusHistEditorForm {
    OrderId: ServiceLookupEditor;
    OldStatus: IntegerEditor;
    NewStatus: IntegerEditor;
    ChangedByUserId: LookupEditor;
    ChangedByUserRole: StringEditor;
    ChangeReason: StringEditor;
    ChangeDate: DateEditor;
}

export class OrderStatusHistEditorForm extends PrefixedContext {
    static readonly formKey = 'Order.OrderStatusHistEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!OrderStatusHistEditorForm.init) {
            OrderStatusHistEditorForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = IntegerEditor;
            var w2 = LookupEditor;
            var w3 = StringEditor;
            var w4 = DateEditor;

            initFormType(OrderStatusHistEditorForm, [
                'OrderId', w0,
                'OldStatus', w1,
                'NewStatus', w1,
                'ChangedByUserId', w2,
                'ChangedByUserRole', w3,
                'ChangeReason', w3,
                'ChangeDate', w4
            ]);
        }
    }
}