import { proxyTexts } from "@serenity-is/corelib";

namespace texts {
    export declare namespace Db {
        export function asKey(): typeof Db;
        export function asTry(): typeof Db;
        namespace Administration {
            export function asKey(): typeof Administration;
            export function asTry(): typeof Administration;
            namespace AuditLog {
                export function asKey(): typeof AuditLog;
                export function asTry(): typeof AuditLog;
                export const ActionDate: string;
                export const ActionType: string;
                export const ChangedFields: string;
                export const EntityId: string;
                export const EntityName: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const EntityType: string;
                export const Id: string;
                export const IpAddress: string;
                export const NewValues: string;
                export const OldValues: string;
                export const UserDisplayName: string;
                export const UserId: string;
                export const Username: string;
            }
            namespace Language {
                export function asKey(): typeof Language;
                export function asTry(): typeof Language;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const LanguageId: string;
                export const LanguageName: string;
            }
            namespace Role {
                export function asKey(): typeof Role;
                export function asTry(): typeof Role;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const RoleId: string;
                export const RoleName: string;
            }
            namespace RolePermission {
                export function asKey(): typeof RolePermission;
                export function asTry(): typeof RolePermission;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const PermissionKey: string;
                export const RoleId: string;
                export const RoleName: string;
                export const RolePermissionId: string;
            }
            namespace User {
                export function asKey(): typeof User;
                export function asTry(): typeof User;
                export const DisplayName: string;
                export const Email: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const LastDirectoryUpdate: string;
                export const Password: string;
                export const PasswordConfirm: string;
                export const PasswordHash: string;
                export const PasswordSalt: string;
                export const Roles: string;
                export const Source: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const UserId: string;
                export const UserImage: string;
                export const Username: string;
            }
            namespace UserPermission {
                export function asKey(): typeof UserPermission;
                export function asTry(): typeof UserPermission;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Granted: string;
                export const PermissionKey: string;
                export const User: string;
                export const UserId: string;
                export const UserPermissionId: string;
                export const Username: string;
            }
            namespace UserRole {
                export function asKey(): typeof UserRole;
                export function asTry(): typeof UserRole;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const RoleId: string;
                export const RoleName: string;
                export const User: string;
                export const UserId: string;
                export const UserRoleId: string;
                export const Username: string;
            }
        }
        namespace Catalog {
            export function asKey(): typeof Catalog;
            export function asTry(): typeof Catalog;
            namespace Brands {
                export function asKey(): typeof Brands;
                export function asTry(): typeof Brands;
                export const Description: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const Logo: string;
                export const Name: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
            namespace PriceListItems {
                export function asKey(): typeof PriceListItems;
                export function asTry(): typeof PriceListItems;
                export const DiscountRate: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const Notes: string;
                export const PriceListCode: string;
                export const PriceListId: string;
                export const PriceListName: string;
                export const ProductCode: string;
                export const ProductId: string;
                export const ProductName: string;
                export const UnitPrice: string;
            }
            namespace PriceLists {
                export function asKey(): typeof PriceLists;
                export function asTry(): typeof PriceLists;
                export const Code: string;
                export const CurrencyCode: string;
                export const CurrencyId: string;
                export const CurrencyName: string;
                export const Description: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const IsDefault: string;
                export const ItemList: string;
                export const Name: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const ValidFrom: string;
                export const ValidTo: string;
            }
            namespace Products {
                export function asKey(): typeof Products;
                export function asTry(): typeof Products;
                export const Barcode: string;
                export const BrandId: string;
                export const BrandName: string;
                export const CategoryId: string;
                export const CategoryName: string;
                export const Code: string;
                export const CodeName: string;
                export const CurrencyCode: string;
                export const CurrencyId: string;
                export const Description: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const Name: string;
                export const Name2: string;
                export const ProductImage: string;
                export const UnitId: string;
                export const UnitName: string;
                export const UnitPrice: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const VatRateId: string;
                export const VatRateName: string;
            }
        }
        namespace Customer {
            export function asKey(): typeof Customer;
            export function asTry(): typeof Customer;
            namespace Customers {
                export function asKey(): typeof Customers;
                export function asTry(): typeof Customers;
                export const Address: string;
                export const City: string;
                export const Code: string;
                export const CountryId: string;
                export const CountryName: string;
                export const District: string;
                export const Email: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const FirstName: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const LastName: string;
                export const Name: string;
                export const Password: string;
                export const PasswordConfirm: string;
                export const Phone: string;
                export const Phone2: string;
                export const TaxNumber: string;
                export const TaxOffice: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const UserId: string;
                export const UserIsActive: string;
                export const Username: string;
                export const VendorTypeId: string;
                export const VendorTypeTitle: string;
            }
        }
        namespace Email {
            export function asKey(): typeof Email;
            export function asTry(): typeof Email;
            namespace EmailAttachments {
                export function asKey(): typeof EmailAttachments;
                export function asTry(): typeof EmailAttachments;
                export const ContentId: string;
                export const ContentType: string;
                export const EmailQueueId: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const FileContent: string;
                export const FileName: string;
                export const FilePath: string;
                export const FileSize: string;
                export const Id: string;
                export const InsertDate: string;
                export const IsInline: string;
            }
            namespace EmailLogs {
                export function asKey(): typeof EmailLogs;
                export function asTry(): typeof EmailLogs;
                export const Duration: string;
                export const EmailQueueId: string;
                export const EmailSubject: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const ProcessEndTime: string;
                export const ProcessStartTime: string;
                export const SmtpResponse: string;
                export const Status: string;
                export const StatusMessage: string;
                export const ToAddress: string;
            }
            namespace EmailQueue {
                export function asKey(): typeof EmailQueue;
                export function asTry(): typeof EmailQueue;
                export const BccAddresses: string;
                export const Body: string;
                export const BodyText: string;
                export const CcAddresses: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const ErrorMessage: string;
                export const FromAddress: string;
                export const FromName: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const NextRetryAt: string;
                export const Priority: string;
                export const ProcessedAt: string;
                export const ReferenceId: string;
                export const ReferenceType: string;
                export const ReplyToAddress: string;
                export const RetryCount: string;
                export const ScheduledAt: string;
                export const SentAt: string;
                export const SmtpSettingsId: string;
                export const SmtpSettingsName: string;
                export const Status: string;
                export const Subject: string;
                export const TemplateData: string;
                export const TemplateId: string;
                export const TemplateName: string;
                export const ToAddresses: string;
            }
            namespace EmailTemplates {
                export function asKey(): typeof EmailTemplates;
                export function asTry(): typeof EmailTemplates;
                export const Body: string;
                export const BodyText: string;
                export const Category: string;
                export const Description: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const LanguageId: string;
                export const Name: string;
                export const Subject: string;
                export const TemplateKey: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
            namespace SmtpSettings {
                export function asKey(): typeof SmtpSettings;
                export function asTry(): typeof SmtpSettings;
                export const DailyLimit: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const FromAddress: string;
                export const FromName: string;
                export const Host: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const IsDefault: string;
                export const MaxRetryCount: string;
                export const Name: string;
                export const Password: string;
                export const Port: string;
                export const RetryIntervalMinutes: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const UseSsl: string;
                export const Username: string;
            }
        }
        namespace Products {
            export function asKey(): typeof Products;
            export function asTry(): typeof Products;
            namespace Brands {
                export function asKey(): typeof Brands;
                export function asTry(): typeof Brands;
                export const Description: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const Logo: string;
                export const Name: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
            namespace ProductCategory {
                export function asKey(): typeof ProductCategory;
                export function asTry(): typeof ProductCategory;
                export const CreatedAt: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const FullPath: string;
                export const Id: string;
                export const IsActive: string;
                export const Name: string;
                export const ParentId: string;
                export const ParentName: string;
                export const SortOrder: string;
            }
        }
        namespace Setting {
            export function asKey(): typeof Setting;
            export function asTry(): typeof Setting;
            namespace BankAccountInformations {
                export function asKey(): typeof BankAccountInformations;
                export function asTry(): typeof BankAccountInformations;
                export const AccountNo: string;
                export const Bank: string;
                export const Branch: string;
                export const BranchCode: string;
                export const Currency: string;
                export const CurrencyCode: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Firm: string;
                export const Iban: string;
                export const Id: string;
                export const Origin: string;
                export const Payment: string;
                export const Shipment: string;
                export const Swift: string;
                export const TenantId: string;
            }
            namespace Country {
                export function asKey(): typeof Country;
                export function asTry(): typeof Country;
                export const Code: string;
                export const CountryNr: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsoCode3: string;
                export const Name: string;
                export const PhoneCode: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
            namespace CurrencyList {
                export function asKey(): typeof CurrencyList;
                export function asTry(): typeof CurrencyList;
                export const Code: string;
                export const CodeType: string;
                export const DefaultExchangeType: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const IsDefaultCurrency: string;
                export const Name: string;
                export const Symbol: string;
                export const TenantId: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
            namespace DailyExchanges {
                export function asKey(): typeof DailyExchanges;
                export function asTry(): typeof DailyExchanges;
                export const BanknoteBuying: string;
                export const BanknoteSelling: string;
                export const CurrencyCode: string;
                export const CurrencyId: string;
                export const DefaultExchangeTypeId: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const ForexBuying: string;
                export const ForexSelling: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
            namespace NumberTemplates {
                export function asKey(): typeof NumberTemplates;
                export function asTry(): typeof NumberTemplates;
                export const Active: string;
                export const DateFormat: string;
                export const DepartmentId: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const Length: string;
                export const LengthText: string;
                export const Prefix: string;
                export const Suffix: string;
                export const TenantId: string;
                export const Type: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
            namespace Units {
                export function asKey(): typeof Units;
                export function asTry(): typeof Units;
                export const Code: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const IsActive: string;
                export const Name: string;
                export const SortOrder: string;
            }
            namespace VatRates {
                export function asKey(): typeof VatRates;
                export function asTry(): typeof VatRates;
                export const Code: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const IsActive: string;
                export const IsDefault: string;
                export const Name: string;
                export const Rate: string;
                export const SortOrder: string;
            }
            namespace VendorType {
                export function asKey(): typeof VendorType;
                export function asTry(): typeof VendorType;
                export const Description: string;
                export const DiscountType: string;
                export const DiscountValue: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const IsActive: string;
                export const Title: string;
            }
        }
        namespace Warehouse {
            export function asKey(): typeof Warehouse;
            export function asTry(): typeof Warehouse;
            namespace StockEntries {
                export function asKey(): typeof StockEntries;
                export function asTry(): typeof StockEntries;
                export const Attachments: string;
                export const Description: string;
                export const DetailList: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const EntryDate: string;
                export const EntryNo: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const Status: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const WarehouseCode: string;
                export const WarehouseId: string;
                export const WarehouseName: string;
            }
            namespace StockEntryDetails {
                export function asKey(): typeof StockEntryDetails;
                export function asTry(): typeof StockEntryDetails;
                export const Currency: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const Notes: string;
                export const ProductCode: string;
                export const ProductId: string;
                export const ProductName: string;
                export const Quantity: string;
                export const StockEntryId: string;
                export const Unit: string;
                export const UnitPrice: string;
                export const VatRate: string;
            }
            namespace StockExitDetails {
                export function asKey(): typeof StockExitDetails;
                export function asTry(): typeof StockExitDetails;
                export const Currency: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const Notes: string;
                export const ProductCode: string;
                export const ProductId: string;
                export const ProductName: string;
                export const Quantity: string;
                export const StockExitId: string;
                export const Unit: string;
                export const UnitPrice: string;
                export const VatRate: string;
            }
            namespace StockExits {
                export function asKey(): typeof StockExits;
                export function asTry(): typeof StockExits;
                export const Attachments: string;
                export const Description: string;
                export const DetailList: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const ExitDate: string;
                export const ExitNo: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const Status: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
                export const WarehouseCode: string;
                export const WarehouseId: string;
                export const WarehouseName: string;
            }
            namespace StockMovements {
                export function asKey(): typeof StockMovements;
                export function asTry(): typeof StockMovements;
                export const Description: string;
                export const DocumentNo: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const MovementDate: string;
                export const MovementType: string;
                export const ProductCode: string;
                export const ProductId: string;
                export const ProductName: string;
                export const Quantity: string;
                export const Status: string;
                export const WarehouseCode: string;
                export const WarehouseId: string;
                export const WarehouseName: string;
            }
            namespace WarehouseStock {
                export function asKey(): typeof WarehouseStock;
                export function asTry(): typeof WarehouseStock;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const LastUpdateDate: string;
                export const ProductCode: string;
                export const ProductId: string;
                export const ProductName: string;
                export const Quantity: string;
                export const WarehouseCode: string;
                export const WarehouseId: string;
                export const WarehouseName: string;
            }
            namespace Warehouses {
                export function asKey(): typeof Warehouses;
                export function asTry(): typeof Warehouses;
                export const Address: string;
                export const City: string;
                export const Code: string;
                export const Description: string;
                export const Email: string;
                export const EntityPlural: string;
                export const EntitySingular: string;
                export const Id: string;
                export const InsertDate: string;
                export const InsertUserId: string;
                export const IsActive: string;
                export const IsDefault: string;
                export const ManagerName: string;
                export const Name: string;
                export const Phone: string;
                export const UpdateDate: string;
                export const UpdateUserId: string;
            }
        }
    }
    export declare namespace Forms {
        export function asKey(): typeof Forms;
        export function asTry(): typeof Forms;
        namespace Membership {
            export function asKey(): typeof Membership;
            export function asTry(): typeof Membership;
            namespace Login {
                export function asKey(): typeof Login;
                export function asTry(): typeof Login;
                export const ForgotPassword: string;
                export const LoginToYourAccount: string;
                export const RememberMe: string;
                export const SignInButton: string;
                export const SignUpButton: string;
            }
            namespace SignUp {
                export function asKey(): typeof SignUp;
                export function asTry(): typeof SignUp;
                export const ActivateEmailSubject: string;
                export const ActivationCompleteMessage: string;
                export const ConfirmEmail: string;
                export const ConfirmPassword: string;
                export const DisplayName: string;
                export const Email: string;
                export const FormInfo: string;
                export const FormTitle: string;
                export const Password: string;
                export const SubmitButton: string;
                export const Success: string;
            }
        }
        export const SiteTitle: string;
    }
    export declare namespace Site {
        export function asKey(): typeof Site;
        export function asTry(): typeof Site;
        namespace AccessDenied {
            export function asKey(): typeof AccessDenied;
            export function asTry(): typeof AccessDenied;
            export const ClickToChangeUser: string;
            export const ClickToLogin: string;
            export const LackPermissions: string;
            export const NotLoggedIn: string;
            export const PageTitle: string;
        }
        namespace Layout {
            export function asKey(): typeof Layout;
            export function asTry(): typeof Layout;
            export const Language: string;
            export const Theme: string;
        }
        namespace RolePermissionDialog {
            export function asKey(): typeof RolePermissionDialog;
            export function asTry(): typeof RolePermissionDialog;
            export const DialogTitle: string;
            export const EditButton: string;
            export const SaveSuccess: string;
        }
        namespace UserDialog {
            export function asKey(): typeof UserDialog;
            export function asTry(): typeof UserDialog;
            export const EditPermissionsButton: string;
            export const EditRolesButton: string;
        }
        namespace UserPermissionDialog {
            export function asKey(): typeof UserPermissionDialog;
            export function asTry(): typeof UserPermissionDialog;
            export const DialogTitle: string;
            export const Grant: string;
            export const Permission: string;
            export const Revoke: string;
            export const SaveSuccess: string;
        }
        namespace ValidationError {
            export function asKey(): typeof ValidationError;
            export function asTry(): typeof ValidationError;
            export const Title: string;
        }
    }
    export declare namespace Validation {
        export function asKey(): typeof Validation;
        export function asTry(): typeof Validation;
        export const AuthenticationError: string;
        export const CurrentPasswordMismatch: string;
        export const DeleteForeignKeyError: string;
        export const EmailConfirm: string;
        export const EmailInUse: string;
        export const InvalidActivateToken: string;
        export const InvalidResetToken: string;
        export const MinRequiredPasswordLength: string;
        export const PasswordConfirmMismatch: string;
        export const SavePrimaryKeyError: string;
    }

}

const Texts: typeof texts = proxyTexts({}, '', {
    Db: {
        Administration: {
            AuditLog: {},
            Language: {},
            Role: {},
            RolePermission: {},
            User: {},
            UserPermission: {},
            UserRole: {}
        },
        Catalog: {
            Brands: {},
            PriceListItems: {},
            PriceLists: {},
            Products: {}
        },
        Customer: {
            Customers: {}
        },
        Email: {
            EmailAttachments: {},
            EmailLogs: {},
            EmailQueue: {},
            EmailTemplates: {},
            SmtpSettings: {}
        },
        Products: {
            Brands: {},
            ProductCategory: {}
        },
        Setting: {
            BankAccountInformations: {},
            Country: {},
            CurrencyList: {},
            DailyExchanges: {},
            NumberTemplates: {},
            Units: {},
            VatRates: {},
            VendorType: {}
        },
        Warehouse: {
            StockEntries: {},
            StockEntryDetails: {},
            StockExitDetails: {},
            StockExits: {},
            StockMovements: {},
            WarehouseStock: {},
            Warehouses: {}
        }
    },
    Forms: {
        Membership: {
            Login: {},
            SignUp: {}
        }
    },
    Site: {
        AccessDenied: {},
        Layout: {},
        RolePermissionDialog: {},
        UserDialog: {},
        UserPermissionDialog: {},
        ValidationError: {}
    },
    Validation: {}
}) as any;

export const AccessDeniedViewTexts = Texts.Site.AccessDenied;
export const LoginFormTexts = Texts.Forms.Membership.Login;
export const MembershipValidationTexts = Texts.Validation;
export const RolePermissionDialogTexts = Texts.Site.RolePermissionDialog;
export const SignUpFormTexts = Texts.Forms.Membership.SignUp;
export const SiteFormTexts = Texts.Forms;
export const SiteLayoutTexts = Texts.Site.Layout;
export const SqlExceptionHelperTexts = Texts.Validation;
export const UserDialogTexts = Texts.Site.UserDialog;
export const UserPermissionDialogTexts = Texts.Site.UserPermissionDialog;
export const ValidationErrorViewTexts = Texts.Site.ValidationError;