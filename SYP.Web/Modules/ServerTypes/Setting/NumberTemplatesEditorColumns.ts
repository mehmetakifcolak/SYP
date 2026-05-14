import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { NumberTemplateType } from "../_Ext/NumberTemplateType";
import { NumberTemplatesRow } from "./NumberTemplatesRow";

export interface NumberTemplatesEditorColumns {
    Id: Column<NumberTemplatesRow>;
    Prefix: Column<NumberTemplatesRow>;
    DateFormat: Column<NumberTemplatesRow>;
    Suffix: Column<NumberTemplatesRow>;
    Length: Column<NumberTemplatesRow>;
    LengthText: Column<NumberTemplatesRow>;
    Active: Column<NumberTemplatesRow>;
    Type: Column<NumberTemplatesRow>;
    DepartmentId: Column<NumberTemplatesRow>;
    InsertUserId: Column<NumberTemplatesRow>;
    InsertDate: Column<NumberTemplatesRow>;
    UpdateUserId: Column<NumberTemplatesRow>;
    UpdateDate: Column<NumberTemplatesRow>;
    TenantId: Column<NumberTemplatesRow>;
}

export class NumberTemplatesEditorColumns extends ColumnsBase<NumberTemplatesRow> {
    static readonly columnsKey = 'Setting.NumberTemplatesEditor';
    static readonly Fields = fieldsProxy<NumberTemplatesEditorColumns>();
}

[NumberTemplateType]; // referenced types