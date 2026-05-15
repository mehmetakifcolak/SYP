import { Decorators } from '@serenity-is/corelib';
import { EntityDialog } from '@serenity-is/corelib';
import { AuditLogForm, AuditLogRow, AuditLogService } from '../../ServerTypes/Administration';

@Decorators.registerClass('SYP.Administration.AuditLogDialog')
export class AuditLogDialog extends EntityDialog<AuditLogRow, any> {
    protected getFormKey() { return AuditLogForm.formKey; }
    protected getRowDefinition() { return AuditLogRow; }
    protected getService() { return AuditLogService.baseUrl; }

    protected form = new AuditLogForm(this.idPrefix);

    constructor(opt?: any) {
        super(opt);
    }

    protected getToolbarButtons() {
        // Sadece kapat butonu
        return [];
    }

    protected getSaveState(): string {
        return '';
    }
}
