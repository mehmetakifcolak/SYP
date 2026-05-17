import { BooleanEditor, DateEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, ServiceLookupEditor, StringEditor } from "@serenity-is/corelib";

export interface OrderDocumentForm {
    OrderId: ServiceLookupEditor;
    DocumentType: IntegerEditor;
    FileName: StringEditor;
    FilePath: StringEditor;
    FileSize: StringEditor;
    MimeType: StringEditor;
    UploadedByUserId: LookupEditor;
    UploadDate: DateEditor;
    IsActive: BooleanEditor;
    Notes: StringEditor;
}

export class OrderDocumentForm extends PrefixedContext {
    static readonly formKey = 'Order.OrderDocument';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!OrderDocumentForm.init) {
            OrderDocumentForm.init = true;

            var w0 = ServiceLookupEditor;
            var w1 = IntegerEditor;
            var w2 = StringEditor;
            var w3 = LookupEditor;
            var w4 = DateEditor;
            var w5 = BooleanEditor;

            initFormType(OrderDocumentForm, [
                'OrderId', w0,
                'DocumentType', w1,
                'FileName', w2,
                'FilePath', w2,
                'FileSize', w2,
                'MimeType', w2,
                'UploadedByUserId', w3,
                'UploadDate', w4,
                'IsActive', w5,
                'Notes', w2
            ]);
        }
    }
}