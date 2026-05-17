import { registerEnum } from "@serenity-is/corelib";

export enum DocumentType {
    Dekont = 1,
    Fatura = 2,
    Irsaliye = 3,
    Diger = 4
}
registerEnum(DocumentType, 'SYP.Order.DocumentType', 'Order.DocumentType');