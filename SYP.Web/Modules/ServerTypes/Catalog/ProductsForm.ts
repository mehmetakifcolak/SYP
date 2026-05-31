import { BooleanEditor, ImageUploadEditor, initFormType, LookupEditor, PrefixedContext, StringEditor, TextAreaEditor } from "@serenity-is/corelib";

export interface ProductsForm {
    Code: StringEditor;
    Barcode: StringEditor;
    CategoryId: LookupEditor;
    BrandId: LookupEditor;
    Name: StringEditor;
    Name2: StringEditor;
    Description: TextAreaEditor;
    UnitId: LookupEditor;
    PackingId: LookupEditor;
    VatRateId: LookupEditor;
    ProductImage: ImageUploadEditor;
    IsActive: BooleanEditor;
}

export class ProductsForm extends PrefixedContext {
    static readonly formKey = 'Catalog.Products';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductsForm.init) {
            ProductsForm.init = true;

            var w0 = StringEditor;
            var w1 = LookupEditor;
            var w2 = TextAreaEditor;
            var w3 = ImageUploadEditor;
            var w4 = BooleanEditor;

            initFormType(ProductsForm, [
                'Code', w0,
                'Barcode', w0,
                'CategoryId', w1,
                'BrandId', w1,
                'Name', w0,
                'Name2', w0,
                'Description', w2,
                'UnitId', w1,
                'PackingId', w1,
                'VatRateId', w1,
                'ProductImage', w3,
                'IsActive', w4
            ]);
        }
    }
}