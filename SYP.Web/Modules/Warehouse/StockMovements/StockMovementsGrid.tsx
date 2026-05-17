import { Decorators, ToolButton, EntityGrid} from "@serenity-is/corelib";
import { StockMovementsColumns, StockMovementsRow, StockMovementsService } from "../../ServerTypes/Warehouse";

@Decorators.registerClass("SYP.Warehouse.StockMovementsGrid")
export class StockMovementsGrid extends EntityGrid<StockMovementsRow, any> {
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

        var movementTypeCol = columns.find(x => x.field === 'MovementType');
        if (movementTypeCol) {
            movementTypeCol.format = ctx => {
                const value = ctx.value as string;
                if (value === 'Entry' || value === 'Exit') {
                    const span = document.createElement('span');
                    span.className = value === 'Entry' ? 'text-success' : 'text-danger';
                    span.innerHTML = value === 'Entry'
                        ? '<i class="fa fa-arrow-down"></i> Giriş'
                        : '<i class="fa fa-arrow-up"></i> Çıkış';
                    return span;
                }
                return value ?? '';
            };
        }

        var quantityCol = columns.find(x => x.field === 'Quantity');
        if (quantityCol) {
            quantityCol.format = ctx => {
                const value = ctx.value as number;
                if (value == null) return '';
                const formatted = value.toFixed(4);
                if (value === 0) return formatted;
                const span = document.createElement('span');
                span.className = value > 0 ? 'text-success' : 'text-danger';
                span.textContent = value > 0 ? '+' + formatted : formatted;
                return span;
            };
        }

        var statusCol = columns.find(x => x.field === 'Status');
        if (statusCol) {
            statusCol.format = ctx => {
                const value = ctx.value as number;
                const cfg: Record<number, [string, string]> = {
                    0: ['bg-secondary', 'Taslak'],
                    1: ['bg-success', 'Onaylandı'],
                    2: ['bg-danger', 'İptal'],
                };
                const c = cfg[value];
                if (!c) return value?.toString() ?? '';
                const badge = document.createElement('span');
                badge.className = 'badge ' + c[0];
                badge.textContent = c[1];
                return badge;
            };
        }

        return columns;
    }
}
