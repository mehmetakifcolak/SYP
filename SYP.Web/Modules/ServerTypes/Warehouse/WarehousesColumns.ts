import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { WarehousesRow } from "./WarehousesRow";

export interface WarehousesColumns {
    Id: Column<WarehousesRow>;
    Code: Column<WarehousesRow>;
    Name: Column<WarehousesRow>;
    City: Column<WarehousesRow>;
    Phone: Column<WarehousesRow>;
    ManagerName: Column<WarehousesRow>;
    IsActive: Column<WarehousesRow>;
}

export class WarehousesColumns extends ColumnsBase<WarehousesRow> {
    static readonly columnsKey = 'Warehouse.Warehouses';
    static readonly Fields = fieldsProxy<WarehousesColumns>();
}