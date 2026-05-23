import { Decorators, EntityGrid, ToolButton } from '@serenity-is/corelib';
import { ProductsColumns, ProductsRow, ProductsService } from '../../ServerTypes/Catalog';
import { ProductsDialog } from './ProductsDialog';
import { ProductsExcelImportDialog } from './ProductsExcelImportDialog';

@Decorators.registerClass('SYP.Catalog.ProductsGrid')
export class ProductsGrid extends EntityGrid<ProductsRow, any> {
    protected getColumnsKey() { return ProductsColumns.columnsKey; }
    protected getDialogType() { return ProductsDialog; }
    protected getRowDefinition() { return ProductsRow; }
    protected getService() { return ProductsService.baseUrl; }

    constructor(props: any) {
        super(props);
    }

    protected getButtons(): ToolButton[] {
        const buttons = super.getButtons();

        buttons.push({
            title: "Excel ile Toplu Yükle",
            cssClass: "import-excel-button",
            icon: "fa-file-excel",
            onClick: () => this.openExcelImportDialog()
        });

        return buttons;
    }

    private openExcelImportDialog(): void {
        const dlg = new ProductsExcelImportDialog({
            onImportComplete: () => {
                this.refresh();
            }
        });
        dlg.dialogOpen();
    }
}