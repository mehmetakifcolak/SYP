import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { NumberTemplateType } from "../_Ext/NumberTemplateType";
import { NumberTemplatesRow } from "./NumberTemplatesRow";

export interface NumberTemplatesColumns {
    Id: Column<NumberTemplatesRow>;
    Type: Column<NumberTemplatesRow>;
    Prefix: Column<NumberTemplatesRow>;
    DateFormat: Column<NumberTemplatesRow>;
    Suffix: Column<NumberTemplatesRow>;
    Length: Column<NumberTemplatesRow>;
    Active: Column<NumberTemplatesRow>;
}

export class NumberTemplatesColumns extends ColumnsBase<NumberTemplatesRow> {
    static readonly columnsKey = 'Setting.NumberTemplates';
    static readonly Fields = fieldsProxy<NumberTemplatesColumns>();
}

[NumberTemplateType]; // referenced types