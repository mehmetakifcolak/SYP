import { DateEditor, initFormType, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";

export interface AuditLogForm {
    ActionDate: DateEditor;
    ActionType: StringEditor;
    EntityType: StringEditor;
    EntityId: StringEditor;
    EntityName: StringEditor;
    Username: StringEditor;
    IpAddress: StringEditor;
    ChangedFields: StringEditor;
    OldValues: TextAreaEditor;
    NewValues: TextAreaEditor;
}

export class AuditLogForm extends PrefixedContext {
    static readonly formKey = 'Administration.AuditLog';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!AuditLogForm.init) {
            AuditLogForm.init = true;

            var w0 = DateEditor;
            var w1 = StringEditor;
            var w2 = TextAreaEditor;

            initFormType(AuditLogForm, [
                'ActionDate', w0,
                'ActionType', w1,
                'EntityType', w1,
                'EntityId', w1,
                'EntityName', w1,
                'Username', w1,
                'IpAddress', w1,
                'ChangedFields', w1,
                'OldValues', w2,
                'NewValues', w2
            ]);
        }
    }
}