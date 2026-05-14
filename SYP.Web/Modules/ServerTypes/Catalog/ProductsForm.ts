import { initFormType, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface ProductsForm {
    Code: StringEditor;
    Name: StringEditor;
    Name2: StringEditor;
    Description: StringEditor;
    Barcode: StringEditor;
}

export class ProductsForm extends PrefixedContext {
    static readonly formKey = 'Catalog.Products';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductsForm.init) {
            ProductsForm.init = true;

            var w0 = StringEditor;

            initFormType(ProductsForm, [
                'Code', w0,
                'Name', w0,
                'Name2', w0,
                'Description', w0,
                'Barcode', w0
            ]);
        }
    }
}