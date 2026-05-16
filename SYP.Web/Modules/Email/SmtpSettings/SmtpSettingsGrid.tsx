import { Decorators } from '@serenity-is/corelib';
import { SmtpSettingsColumns, SmtpSettingsRow, SmtpSettingsService } from '../../ServerTypes/Email';
import { SmtpSettingsDialog } from './SmtpSettingsDialog';
import { GridBase } from '../../_Ext/Bases/GridBase';

@Decorators.registerClass('SYP.Email.SmtpSettingsGrid')
export class SmtpSettingsGrid extends GridBase<SmtpSettingsRow, any> {
    protected getColumnsKey() { return SmtpSettingsColumns.columnsKey; }
    protected getDialogType() { return SmtpSettingsDialog; }
    protected getRowDefinition() { return SmtpSettingsRow; }
    protected getService() { return SmtpSettingsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}
