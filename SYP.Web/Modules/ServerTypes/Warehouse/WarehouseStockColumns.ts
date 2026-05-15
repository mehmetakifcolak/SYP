import { ColumnsBase, fieldsProxy } from "@serenity-is/corelib";
import { Column } from "@serenity-is/sleekgrid";
import { WarehouseStockRow } from "./WarehouseStockRow";

export interface WarehouseStockColumns {
    WarehouseCode: Column<WarehouseStockRow>;
    WarehouseName: Column<WarehouseStockRow>;
    ProductCode: Column<WarehouseStockRow>;
    ProductName: Column<WarehouseStockRow>;
    Quantity: Column<WarehouseStockRow>;
    LastUpdateDate: Column<WarehouseStockRow>;
}

export class WarehouseStockColumns extends ColumnsBase<WarehouseStockRow> {
    static readonly columnsKey = 'Warehouse.WarehouseStock';
    static readonly Fields = fieldsProxy<WarehouseStockColumns>();
}