import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { SmtpSettingsColumns, SmtpSettingsRow, SmtpSettingsService } from '../../ServerTypes/Email';
import { SmtpSettingsDialog } from './SmtpSettingsDialog';

@Decorators.registerClass('SYP.Email.SmtpSettingsGrid')
export class SmtpSettingsGrid extends EntityGrid<SmtpSettingsRow, any> {
    protected getColumnsKey() { return SmtpSettingsColumns.columnsKey; }
    protected getDialogType() { return SmtpSettingsDialog; }
    protected getRowDefinition() { return SmtpSettingsRow; }
    protected getService() { return SmtpSettingsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}
