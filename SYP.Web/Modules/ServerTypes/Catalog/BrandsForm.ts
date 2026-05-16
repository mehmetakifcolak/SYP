import { BooleanEditor, ImageUploadEditor, initFormType, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface BrandsForm {
    Name: StringEditor;
    Description: StringEditor;
    Logo: ImageUploadEditor;
    IsActive: BooleanEditor;
}

export class BrandsForm extends PrefixedContext {
    static readonly formKey = 'Catalog.Brands';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!BrandsForm.init) {
            BrandsForm.init = true;

            var w0 = StringEditor;
            var w1 = ImageUploadEditor;
            var w2 = BooleanEditor;

            initFormType(BrandsForm, [
                'Name', w0,
                'Description', w0,
                'Logo', w1,
                'IsActive', w2
            ]);
        }
    }
}