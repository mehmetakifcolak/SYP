import { Decorators, ToolButton } from "@serenity-is/corelib";
import { WarehouseStockColumns, WarehouseStockRow, WarehouseStockService } from "../../ServerTypes/Warehouse";
import { GridBase } from "../../_Ext/Bases/GridBase";

@Decorators.registerClass("SYP.Warehouse.WarehouseStockGrid")
export class WarehouseStockGrid extends GridBase<WarehouseStockRow, any> {
    protected getColumnsKey() { return WarehouseStockColumns.columnsKey; }
    protected getRowDefinition() { return WarehouseStockRow; }
    protected getService() { return WarehouseStockService.baseUrl; }

    protected getButtons(): ToolButton[] {
        // Sadece Excel export butonu, yeni ekleme yok
        let buttons = super.getButtons();
        // "Yeni" butonunu kaldır (stok sadece stok girişleri ile güncellenir)
        return buttons.filter(x => x.cssClass !== "add-button");
    }
}
