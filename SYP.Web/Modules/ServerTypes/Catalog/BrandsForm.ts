import { ImageUploadEditor, initFormType, IntegerEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";

export interface BrandsForm {
    Name: StringEditor;
    Description: TextAreaEditor;
    Logo: ImageUploadEditor;
    IsActive: IntegerEditor;
}

export class BrandsForm extends PrefixedContext {
    static readonly formKey = 'Catalog.Brands';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!BrandsForm.init) {
            BrandsForm.init = true;

            var w0 = StringEditor;
            var w1 = TextAreaEditor;
            var w2 = ImageUploadEditor;
            var w3 = IntegerEditor;

            initFormType(BrandsForm, [
                'Name', w0,
                'Description', w1,
                'Logo', w2,
                'IsActive', w3
            ]);
        }
    }
}