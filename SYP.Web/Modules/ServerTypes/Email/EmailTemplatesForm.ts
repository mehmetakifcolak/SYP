import { BooleanEditor, HtmlContentEditor, initFormType, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";

export interface EmailTemplatesForm {
    TemplateKey: StringEditor;
    Name: StringEditor;
    Subject: StringEditor;
    Category: StringEditor;
    LanguageId: StringEditor;
    Description: TextAreaEditor;
    IsActive: BooleanEditor;
    Body: HtmlContentEditor;
    BodyText: TextAreaEditor;
}

export class EmailTemplatesForm extends PrefixedContext {
    static readonly formKey = 'Email.EmailTemplates';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!EmailTemplatesForm.init) {
            EmailTemplatesForm.init = true;

            var w0 = StringEditor;
            var w1 = TextAreaEditor;
            var w2 = BooleanEditor;
            var w3 = HtmlContentEditor;

            initFormType(EmailTemplatesForm, [
                'TemplateKey', w0,
                'Name', w0,
                'Subject', w0,
                'Category', w0,
                'LanguageId', w0,
                'Description', w1,
                'IsActive', w2,
                'Body', w3,
                'BodyText', w1
            ]);
        }
    }
}