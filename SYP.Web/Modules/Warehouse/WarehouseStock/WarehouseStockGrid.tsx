import { Decorators, EntityGrid, ToolButton } from "@serenity-is/corelib";
import { WarehouseStockColumns, WarehouseStockRow, WarehouseStockService } from "../../ServerTypes/Warehouse";

@Decorators.registerClass("SYP.Warehouse.WarehouseStockGrid")
export class WarehouseStockGrid extends EntityGrid<WarehouseStockRow> {
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
