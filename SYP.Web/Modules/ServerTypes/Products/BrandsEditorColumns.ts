import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { BrandsRow } from "./BrandsRow";

export interface BrandsEditorColumns {
    Id: Column<BrandsRow>;
    Name: Column<BrandsRow>;
    Description: Column<BrandsRow>;
    Logo: Column<BrandsRow>;
    IsActive: Column<BrandsRow>;
    InsertDate: Column<BrandsRow>;
    InsertUserId: Column<BrandsRow>;
    UpdateDate: Column<BrandsRow>;
    UpdateUserId: Column<BrandsRow>;
}

export class BrandsEditorColumns extends ColumnsBase<BrandsRow> {
    static readonly columnsKey = 'Products.BrandsEditor';
    static readonly Fields = fieldsProxy<BrandsEditorColumns>();
}