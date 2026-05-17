import { BooleanEditor, initFormType, IntegerEditor, LookupEditor, PrefixedContext, StringEditor } from "@serenity-is/corelib";

export interface ProductCategoryForm {
    ParentId: LookupEditor;
    Name: StringEditor;
    SortOrder: IntegerEditor;
    IsActive: BooleanEditor;
}

export class ProductCategoryForm extends PrefixedContext {
    static readonly formKey = 'Catalog.ProductCategory';
    private static init: boolean;

    constructor(prefix: string) {
        super(prefix);

        if (!ProductCategoryForm.init) {
            ProductCategoryForm.init = true;

            var w0 = LookupEditor;
            var w1 = StringEditor;
            var w2 = IntegerEditor;
            var w3 = BooleanEditor;

            initFormType(ProductCategoryForm, [
                'ParentId', w0,
                'Name', w1,
                'SortOrder', w2,
                'IsActive', w3
            ]);
        }
    }
}