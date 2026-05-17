import { DateEditor, EmailAddressEditor, EnumEditor, HtmlContentEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";
import { EmailPriority } from "./EmailPriority";
import { EmailQueueStatus } from "./EmailQueueStatus";

export interface EmailQueueForm {
    ToAddresses: TextAreaEditor;
    CcAddresses: TextAreaEditor;
    BccAddresses: TextAreaEditor;
    SmtpSettingsId: LookupEditor;
    TemplateId: LookupEditor;
    Subject: StringEditor;
    Body: HtmlContentEditor;
    BodyText: TextAreaEditor;
    FromAddress: EmailAddressEditor;
    FromName: StringEditor;
    ReplyToAddress: EmailAddressEditor;
    Priority: EnumEditor;
    ScheduledAt: DateEditor;
    Status: EnumEditor;
    RetryCount: IntegerEditor;
    ProcessedAt: DateEditor;
    SentAt: DateEditor;
    NextRetryAt: DateEditor;
    ErrorMessage: TextAreaEditor;
    ReferenceType: StringEditor;
    ReferenceId: StringEditor;
    TemplateData: TextAreaEditor;
}

export class EmailQueueForm extends PrefixedContext {
    static readonly formKey = 'Email.EmailQueue';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!EmailQueueForm.init) {
            EmailQueueForm.init = true;

            var w0 = TextAreaEditor;
            var w1 = LookupEditor;
            var w2 = StringEditor;
            var w3 = HtmlContentEditor;
            var w4 = EmailAddressEditor;
            var w5 = EnumEditor;
            var w6 = DateEditor;
            var w7 = IntegerEditor;

            initFormType(EmailQueueForm, [
                'ToAddresses', w0,
                'CcAddresses', w0,
                'BccAddresses', w0,
                'SmtpSettingsId', w1,
                'TemplateId', w1,
                'Subject', w2,
                'Body', w3,
                'BodyText', w0,
                'FromAddress', w4,
                'FromName', w2,
                'ReplyToAddress', w4,
                'Priority', w5,
                'ScheduledAt', w6,
                'Status', w5,
                'RetryCount', w7,
                'ProcessedAt', w6,
                'SentAt', w6,
                'NextRetryAt', w6,
                'ErrorMessage', w0,
                'ReferenceType', w2,
                'ReferenceId', w2,
                'TemplateData', w0
            ]);
        }
    }
}

[EmailPriority, EmailQueueStatus]; // referenced types