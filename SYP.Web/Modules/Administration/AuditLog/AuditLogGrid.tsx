import { Decorators, ToolButton } from '@serenity-is/corelib';
import { AuditLogColumns, AuditLogRow, AuditLogService } from '../../ServerTypes/Administration';
import { AuditLogDialog } from './AuditLogDialog';
import { GridBase } from '../../_Ext/Bases/GridBase';

@Decorators.registerClass('SYP.Administration.AuditLogGrid')
export class AuditLogGrid extends GridBase<AuditLogRow, any> {
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
                let value = ctx.value as string;
                let badgeClass = 'secondary';
                let label = value;

                switch (value) {
                    case 'Insert':
                        badgeClass = 'success';
                        label = 'Ekleme';
                        break;
                    case 'Update':
                        badgeClass = 'warning';
                        label = 'Güncelleme';
                        break;
                    case 'Delete':
                        badgeClass = 'danger';
                        label = 'Silme';
                        break;
                }

                return `<span class="badge bg-${badgeClass}">${label}</span>`;
            };
        }

        return columns;
    }
}
