import { BooleanEditor, DateEditor, initFormType, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface EmailAttachmentsForm {
    EmailQueueId: StringEditor;
    FileName: StringEditor;
    ContentType: StringEditor;
    FilePath: StringEditor;
    FileSize: StringEditor;
    IsInline: BooleanEditor;
    ContentId: StringEditor;
    InsertDate: DateEditor;
}

export class EmailAttachmentsForm extends PrefixedContext {
    static readonly formKey = 'Email.EmailAttachments';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!EmailAttachmentsForm.init) {
            EmailAttachmentsForm.init = true;

            var w0 = StringEditor;
            var w1 = BooleanEditor;
            var w2 = DateEditor;

            initFormType(EmailAttachmentsForm, [
                'EmailQueueId', w0,
                'FileName', w0,
                'ContentType', w0,
                'FilePath', w0,
                'FileSize', w0,
                'IsInline', w1,
                'ContentId', w0,
                'InsertDate', w2
            ]);
        }
    }
}