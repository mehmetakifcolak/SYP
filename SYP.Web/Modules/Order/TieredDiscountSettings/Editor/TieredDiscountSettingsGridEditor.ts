import { GridEditorBase } from '@/_Ext/Editors/GridEditorBase';
import { Decorators } from '@serenity-is/corelib';
import { TieredDiscountSettingsEditorColumns, TieredDiscountSettingsRow } from '../../ServerTypes/Order';
import { TieredDiscountSettingsEditorDialog } from './TieredDiscountSettingsEditorDialog';

@Decorators.registerEditor('SYP.Order.TieredDiscountSettingsGridEditor')
export class TieredDiscountSettingsGridEditor extends GridEditorBase<TieredDiscountSettingsRow> {
    protected getColumnsKey() { return TieredDiscountSettingsEditorColumns.columnsKey; }
    protected getDialogType() { return TieredDiscountSettingsEditorDialog; }
    protected getRowDefinition() { return TieredDiscountSettingsRow; }

    constructor(props: any) {
        super(props);
    }
}