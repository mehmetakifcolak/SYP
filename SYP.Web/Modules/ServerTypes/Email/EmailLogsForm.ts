import { DateEditor, EnumEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";
import { EmailLogStatus } from "./EmailLogStatus";

export interface EmailLogsForm {
    EmailQueueId: StringEditor;
    EmailSubject: StringEditor;
    ToAddress: StringEditor;
    Status: EnumEditor;
    StatusMessage: TextAreaEditor;
    SmtpResponse: TextAreaEditor;
    ProcessStartTime: DateEditor;
    ProcessEndTime: DateEditor;
    Duration: IntegerEditor;
    InsertDate: DateEditor;
}

export class EmailLogsForm extends PrefixedContext {
    static readonly formKey = 'Email.EmailLogs';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!EmailLogsForm.init) {
            EmailLogsForm.init = true;

            var w0 = StringEditor;
            var w1 = EnumEditor;
            var w2 = TextAreaEditor;
            var w3 = DateEditor;
            var w4 = IntegerEditor;

            initFormType(EmailLogsForm, [
                'EmailQueueId', w0,
                'EmailSubject', w0,
                'ToAddress', w0,
                'Status', w1,
                'StatusMessage', w2,
                'SmtpResponse', w2,
                'ProcessStartTime', w3,
                'ProcessEndTime', w3,
                'Duration', w4,
                'InsertDate', w3
            ]);
        }
    }
}

[EmailLogStatus]; // referenced types