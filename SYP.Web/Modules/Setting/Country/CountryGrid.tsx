import { Decorators, EntityGrid} from '@serenity-is/corelib';
import { CountryColumns, CountryRow, CountryService } from '../../ServerTypes/Setting';
import { CountryDialog } from './CountryDialog';

@Decorators.registerClass('SYP.Setting.CountryGrid')
export class CountryGrid extends EntityGrid<CountryRow, any> {
    protected getColumnsKey() { return CountryColumns.columnsKey; }
    protected getDialogType() { return CountryDialog; }
    protected getRowDefinition() { return CountryRow; }
    protected getService() { return CountryService.baseUrl; }

    constructor(props: any) {
        super(props);
    }
}