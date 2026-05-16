import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { CountryRow } from "./CountryRow";

export interface CountryEditorColumns {
    Id: Column<CountryRow>;
    Name: Column<CountryRow>;
    Code: Column<CountryRow>;
    IsoCode3: Column<CountryRow>;
    PhoneCode: Column<CountryRow>;
    CountryNr: Column<CountryRow>;
    InsertUserId: Column<CountryRow>;
    InsertDate: Column<CountryRow>;
    UpdateUserId: Column<CountryRow>;
    UpdateDate: Column<CountryRow>;
}

export class CountryEditorColumns extends ColumnsBase<CountryRow> {
    static readonly columnsKey = 'Setting.CountryEditor';
    static readonly Fields = fieldsProxy<CountryEditorColumns>();
}