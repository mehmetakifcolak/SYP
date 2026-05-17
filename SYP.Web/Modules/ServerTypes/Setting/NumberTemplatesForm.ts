import { BooleanEditor, EnumEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";
import { NumberTemplateType } from "./NumberTemplateType";

export interface NumberTemplatesForm {
    Type: EnumEditor;
    Prefix: StringEditor;
    DateFormat: StringEditor;
    Suffix: StringEditor;
    Length: IntegerEditor;
    Active: BooleanEditor;
}

export class NumberTemplatesForm extends PrefixedContext {
    static readonly formKey = 'Setting.NumberTemplates';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!NumberTemplatesForm.init) {
            NumberTemplatesForm.init = true;

            var w0 = EnumEditor;
            var w1 = StringEditor;
            var w2 = IntegerEditor;
            var w3 = BooleanEditor;

            initFormType(NumberTemplatesForm, [
                'Type', w0,
                'Prefix', w1,
                'DateFormat', w1,
                'Suffix', w1,
                'Length', w2,
                'Active', w3
            ]);
        }
    }
}

[NumberTemplateType]; // referenced types