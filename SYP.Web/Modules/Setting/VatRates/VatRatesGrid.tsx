import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { VatRatesColumns, VatRatesRow, VatRatesService } from '../../ServerTypes/Setting';
import { VatRatesDialog } from './VatRatesDialog';

@Decorators.registerClass('SYP.Setting.VatRatesGrid')
export class VatRatesGrid extends EntityGrid<VatRatesRow, any> {
    protected getColumnsKey() { return VatRatesColumns.columnsKey; }
    protected getDialogType() { return VatRatesDialog; }
    protected getRowDefinition() { return VatRatesRow; }
    protected getService() { return VatRatesService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}