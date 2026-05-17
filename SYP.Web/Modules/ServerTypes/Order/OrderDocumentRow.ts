import { fieldsProxy } from "@serenity-is/corelib";

export interface OrderDocumentRow {
    Id?: number;
    OrderId?: number;
    DocumentType?: number;
    FileName?: string;
    FilePath?: string;
    FileSize?: number;
    MimeType?: string;
    UploadedByUserId?: number;
    UploadDate?: string;
    IsActive?: boolean;
    Notes?: string;
    OrderNumber?: string;
    UploadedByUserUsername?: string;
}

export abstract class OrderDocumentRow {
    static readonly idProperty = 'Id';
    static readonly nameProperty = 'FileName';
    static readonly localTextPrefix = 'Order.OrderDocument';
    static readonly deletePermission = 'Order:OrderDocument:Delete';
    static readonly insertPermission = 'Order:OrderDocument:Insert';
    static readonly readPermission = 'Order:OrderDocument:Read';
    static readonly updatePermission = 'Order:OrderDocument:Update';

    static readonly Fields = fieldsProxy<OrderDocumentRow>();
}