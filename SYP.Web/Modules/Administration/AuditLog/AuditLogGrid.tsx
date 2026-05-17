import { Decorators, ToolButton, EntityGrid} from '@serenity-is/corelib';
import { AuditLogColumns, AuditLogRow, AuditLogService } from '../../ServerTypes/Administration';
import { AuditLogDialog } from './AuditLogDialog';

@Decorators.registerClass('SYP.Administration.AuditLogGrid')
export class AuditLogGrid extends EntityGrid<AuditLogRow, any> {
    protected getColumnsKey() { return AuditLogColumns.columnsKey; }
    protected getDialogType() { return AuditLogDialog; }
    protected getRowDefinition() { return AuditLogRow; }
    protected getService() { return AuditLogService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getAddButtonCaption() {
        return null; // Log kayıtları manuel eklenemez
    }

    protected getButtons(): ToolButton[] {
        let buttons = super.getButtons();
        // Yeni ekle butonunu kaldır
        buttons = buttons.filter(x => x.cssClass !== 'add-button');
        return buttons;
    }

    protected getColumns() {
        let columns = super.getColumns();

        // ActionType sütununa renk formatlayıcı ekle
        let actionTypeCol = columns.find(x => x.field === 'ActionType');
        if (actionTypeCol) {
            actionTypeCol.format = ctx => {
                const map: Record<string, [string, string]> = {
                    'Insert': ['success', 'Ekleme'],
                    'Update': ['warning', 'Güncelleme'],
                    'Delete': ['danger', 'Silme'],
                };
                const value = ctx.value as string;
                const cfg = map[value] ?? ['secondary', value];
                const badge = document.createElement('span');
                badge.className = 'badge bg-' + cfg[0];
                badge.textContent = cfg[1];
                return badge;
            };
        }

        return columns;
    }
}
