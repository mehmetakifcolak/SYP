import { Decorators, ToolButton } from "@serenity-is/corelib";
import { StockMovementsColumns, StockMovementsRow, StockMovementsService } from "../../ServerTypes/Warehouse";
import { GridBase } from "../../_Ext/Bases/GridBase";

@Decorators.registerClass("SYP.Warehouse.StockMovementsGrid")
export class StockMovementsGrid extends GridBase<StockMovementsRow, any> {
    protected getColumnsKey() { return StockMovementsColumns.columnsKey; }
    protected getRowDefinition() { return StockMovementsRow; }
    protected getService() { return StockMovementsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getButtons(): ToolButton[] {
        // Sadece Excel export butonu, ekleme/silme yok (read-only view)
        var buttons = super.getButtons();
        // Yeni ekle butonunu kaldır
        buttons = buttons.filter(x => x.cssClass !== 'add-button');
        return buttons;
    }

    protected getColumns() {
        var columns = super.getColumns();

        // Hareket Tipi kolonunu formatla
        var movementTypeCol = columns.find(x => x.field === 'MovementType');
        if (movementTypeCol) {
            movementTypeCol.format = ctx => {
                var value = ctx.item.MovementType;
                if (value === 'Entry') {
                    return '<span class="text-success"><i class="fa fa-arrow-down"></i> Giriş</span>';
                } else if (value === 'Exit') {
                    return '<span class="text-danger"><i class="fa fa-arrow-up"></i> Çıkış</span>';
                }
                return value;
            };
        }

        // Miktar kolonunu formatla (pozitif yeşil, negatif kırmızı)
        var quantityCol = columns.find(x => x.field === 'Quantity');
        if (quantityCol) {
            quantityCol.format = ctx => {
                var value = ctx.item.Quantity;
                if (value == null) return '';
                var formatted = value.toFixed(4);
                if (value > 0) {
                    return '<span class="text-success">+' + formatted + '</span>';
                } else if (value < 0) {
                    return '<span class="text-danger">' + formatted + '</span>';
                }
                return formatted;
            };
        }

        // Durum kolonunu formatla
        var statusCol = columns.find(x => x.field === 'Status');
        if (statusCol) {
            statusCol.format = ctx => {
                var value = ctx.item.Status;
                switch (value) {
                    case 0: return '<span class="badge bg-secondary">Taslak</span>';
                    case 1: return '<span class="badge bg-success">Onaylandı</span>';
                    case 2: return '<span class="badge bg-danger">İptal</span>';
                    default: return value?.toString() ?? '';
                }
            };
        }

        return columns;
    }
}
