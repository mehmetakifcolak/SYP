import { BooleanEditor, DateEditor, EnumEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";
import { NumberTemplateType } from "./NumberTemplateType";

export interface NumberTemplatesEditorForm {
    Prefix: StringEditor;
    DateFormat: StringEditor;
    Suffix: StringEditor;
    Length: IntegerEditor;
    LengthText: IntegerEditor;
    Active: BooleanEditor;
    Type: EnumEditor;
    DepartmentId: IntegerEditor;
    InsertUserId: IntegerEditor;
    InsertDate: DateEditor;
    UpdateUserId: IntegerEditor;
    UpdateDate: DateEditor;
    TenantId: IntegerEditor;
}

export class NumberTemplatesEditorForm extends PrefixedContext {
    static readonly formKey = 'Setting.NumberTemplatesEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!NumberTemplatesEditorForm.init) {
            NumberTemplatesEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = IntegerEditor;
            var w2 = BooleanEditor;
            var w3 = EnumEditor;
            var w4 = DateEditor;

            initFormType(NumberTemplatesEditorForm, [
                'Prefix', w0,
                'DateFormat', w0,
                'Suffix', w0,
                'Length', w1,
                'LengthText', w1,
                'Active', w2,
                'Type', w3,
                'DepartmentId', w1,
                'InsertUserId', w1,
                'InsertDate', w4,
                'UpdateUserId', w1,
                'UpdateDate', w4,
                'TenantId', w1
            ]);
        }
    }
}

[NumberTemplateType]; // referenced types