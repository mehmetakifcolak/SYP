import { BooleanEditor, EmailAddressEditor, initFormType, IntegerEditor, PasswordEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface SmtpSettingsForm {
    Name: StringEditor;
    Host: StringEditor;
    Port: IntegerEditor;
    UseSsl: BooleanEditor;
    Username: StringEditor;
    Password: PasswordEditor;
    FromAddress: EmailAddressEditor;
    FromName: StringEditor;
    IsDefault: BooleanEditor;
    IsActive: BooleanEditor;
    MaxRetryCount: IntegerEditor;
    RetryIntervalMinutes: IntegerEditor;
    DailyLimit: IntegerEditor;
}

export class SmtpSettingsForm extends PrefixedContext {
    static readonly formKey = 'Email.SmtpSettings';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!SmtpSettingsForm.init) {
            SmtpSettingsForm.init = true;

            var w0 = StringEditor;
            var w1 = IntegerEditor;
            var w2 = BooleanEditor;
            var w3 = PasswordEditor;
            var w4 = EmailAddressEditor;

            initFormType(SmtpSettingsForm, [
                'Name', w0,
                'Host', w0,
                'Port', w1,
                'UseSsl', w2,
                'Username', w0,
                'Password', w3,
                'FromAddress', w4,
                'FromName', w0,
                'IsDefault', w2,
                'IsActive', w2,
                'MaxRetryCount', w1,
                'RetryIntervalMinutes', w1,
                'DailyLimit', w1
            ]);
        }
    }
}