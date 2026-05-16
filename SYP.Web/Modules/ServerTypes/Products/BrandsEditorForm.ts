import { BooleanEditor, DateEditor, ImageUploadEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface BrandsEditorForm {
    Name: StringEditor;
    Description: StringEditor;
    Logo: ImageUploadEditor;
    IsActive: BooleanEditor;
    InsertDate: DateEditor;
    InsertUserId: IntegerEditor;
    UpdateDate: DateEditor;
    UpdateUserId: IntegerEditor;
}

export class BrandsEditorForm extends PrefixedContext {
    static readonly formKey = 'Products.BrandsEditor';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!BrandsEditorForm.init) {
            BrandsEditorForm.init = true;

            var w0 = StringEditor;
            var w1 = ImageUploadEditor;
            var w2 = BooleanEditor;
            var w3 = DateEditor;
            var w4 = IntegerEditor;

            initFormType(BrandsEditorForm, [
                'Name', w0,
                'Description', w0,
                'Logo', w1,
                'IsActive', w2,
                'InsertDate', w3,
                'InsertUserId', w4,
                'UpdateDate', w3,
                'UpdateUserId', w4
            ]);
        }
    }
}