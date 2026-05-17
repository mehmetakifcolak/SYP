import { Decorators, EntityGrid, ListRequest } from "@serenity-is/corelib";
import { PriceListItemsRow } from "../../ServerTypes/Catalog";
import { PriceListsDialog } from "../PriceLists/PriceListsDialog";

@Decorators.registerClass("SYP.Catalog.ProductPriceListsGrid")
export class ProductPriceListsGrid extends EntityGrid<PriceListItemsRow, any> {
    protected getColumnsKey() { return "Catalog.ProductPriceListItems"; }
    protected getIdProperty() { return PriceListItemsRow.idProperty; }
    protected getLocalTextPrefix() { return PriceListItemsRow.localTextPrefix; }
    protected getService() { return "Catalog/PriceListItems"; }

    private productId: number;

    constructor(props: { element?: any; productId?: number }) {
        super(props);
        this.productId = props.productId || 0;
    }

    public setProductId(productId: number) {
        this.productId = productId;
        this.refresh();
    }

    protected getButtons() {
        return [{
            title: "Fiyat Listesini Düzenle",
            cssClass: "btn btn-sm btn-primary open-price-list-button",
            onClick: () => {
                const items = this.view.getItems();
                if (items.length === 0) return;

                // İlk satırın PriceListId'sini al
                const priceListId = items[0].PriceListId;

                if (priceListId) {
                    new PriceListsDialog().loadByIdAndOpenDialog(priceListId);
                }
            }
        }];
    }

    protected getInitialTitle() {
        return null;
    }

    protected usePager() {
        return false;
    }

    protected onViewSubmit() {
        if (!super.onViewSubmit()) {
            return false;
        }

        if (!this.productId) {
            return false;
        }

        const request = this.view.params as ListRequest;
        request.EqualityFilter = request.EqualityFilter || {};
        request.EqualityFilter["ProductId"] = this.productId;

        return true;
    }

    protected getColumns() {
        const columns = super.getColumns();

        // İlk sütunu (product code/name) kaldır çünkü zaten ürün detayındayız
        return columns.filter(c =>
            c.field !== "ProductCode" && c.field !== "ProductName"
        );
    }
}
