using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260520_1000)]
public class DefaultDB_20260520_1000_BusinessTables : Migration
{
    public override void Up()
    {
        // 1. CATALOG MODULE
        CreateBrandsTable();
        CreateProductCategoryTable();
        CreateProductsTable();
        CreatePriceListsTable();
        CreatePriceListItemsTable();

        // 2. CUSTOMER MODULE
        CreateCustomersTable();
        CreateVendorTypeTable();
        CreateCountryTable();
        CreateBankAccountInformationsTable();

        // 3. WAREHOUSE MODULE
        CreateWarehousesTable();
        CreateStockEntriesTable();
        CreateStockEntryDetailsTable();
        CreateStockExitsTable();
        CreateStockExitDetailsTable();
        CreateWarehouseStockTable();
        CreateStockMovementsView();

        // 4. PRICING MODULE
        CreateCurrencyListTable();
        CreateDailyExchangesTable();
        CreateVatRatesTable();
        CreateUnitsTable();

        // 5. EMAIL MODULE
        CreateSmtpSettingsTable();
        CreateEmailTemplatesTable();
        CreateEmailQueueTable();
        CreateEmailLogsTable();
        CreateEmailAttachmentsTable();

        // 6. SYSTEM MODULE
        CreateAuditLogTable();
        CreateNumberTemplatesTable();
        CreateFileArchiveTable();

        // 7. FOREIGN KEYS
        CreateAllForeignKeys();

        // 8. INDEXES
        CreateAllIndexes();

        // 9. Fix Users.IsActive to BIT
        StandardizeUsersIsActive();
    }

    public override void Down()
    {
        // Drop in reverse order
        Execute.Sql("IF EXISTS (SELECT * FROM sys.views WHERE name = 'StockMovements') DROP VIEW StockMovements");

        if (Schema.Table("EmailAttachments").Exists()) Delete.Table("EmailAttachments");
        if (Schema.Table("EmailLogs").Exists()) Delete.Table("EmailLogs");
        if (Schema.Table("EmailQueue").Exists()) Delete.Table("EmailQueue");
        if (Schema.Table("EmailTemplates").Exists()) Delete.Table("EmailTemplates");
        if (Schema.Table("SmtpSettings").Exists()) Delete.Table("SmtpSettings");

        if (Schema.Table("WarehouseStock").Exists()) Delete.Table("WarehouseStock");
        if (Schema.Table("StockExitDetails").Exists()) Delete.Table("StockExitDetails");
        if (Schema.Table("StockExits").Exists()) Delete.Table("StockExits");
        if (Schema.Table("StockEntryDetails").Exists()) Delete.Table("StockEntryDetails");
        if (Schema.Table("StockEntries").Exists()) Delete.Table("StockEntries");
        if (Schema.Table("Warehouses").Exists()) Delete.Table("Warehouses");

        if (Schema.Table("PriceListItems").Exists()) Delete.Table("PriceListItems");
        if (Schema.Table("PriceLists").Exists()) Delete.Table("PriceLists");
        if (Schema.Table("DailyExchanges").Exists()) Delete.Table("DailyExchanges");
        if (Schema.Table("CurrencyList").Exists()) Delete.Table("CurrencyList");
        if (Schema.Table("VatRates").Exists()) Delete.Table("VatRates");
        if (Schema.Table("Units").Exists()) Delete.Table("Units");

        if (Schema.Table("Products").Exists()) Delete.Table("Products");
        if (Schema.Table("ProductCategory").Exists()) Delete.Table("ProductCategory");
        if (Schema.Table("Brands").Exists()) Delete.Table("Brands");

        if (Schema.Table("BankAccountInformations").Exists()) Delete.Table("BankAccountInformations");
        if (Schema.Table("Customers").Exists()) Delete.Table("Customers");
        if (Schema.Table("Country").Exists()) Delete.Table("Country");
        if (Schema.Table("VendorType").Exists()) Delete.Table("VendorType");

        if (Schema.Table("FileArchive").Exists()) Delete.Table("FileArchive");
        if (Schema.Table("NumberTemplates").Exists()) Delete.Table("NumberTemplates");
        if (Schema.Table("AuditLog").Exists()) Delete.Table("AuditLog");
    }

    #region CATALOG MODULE

    private void CreateBrandsTable()
    {
        if (!Schema.Table("Brands").Exists())
        {
            Create.Table("Brands")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(500).Nullable()
                .WithColumn("Description").AsString(4000).Nullable()
                .WithColumn("Logo").AsString(400).Nullable()
                .WithColumn("IsActive").AsBoolean().Nullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateProductCategoryTable()
    {
        if (!Schema.Table("ProductCategory").Exists())
        {
            Create.Table("ProductCategory")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(500).Nullable()
                .WithColumn("Description").AsString(4000).Nullable()
                .WithColumn("ParentId").AsInt32().Nullable()
                .WithColumn("Path").AsString(1000).Nullable()
                .WithColumn("Level").AsInt32().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateProductsTable()
    {
        if (!Schema.Table("Products").Exists())
        {
            Create.Table("Products")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(50).Nullable()
                .WithColumn("Name").AsString(500).Nullable()
                .WithColumn("Name2").AsString(500).Nullable()
                .WithColumn("Description").AsString(4000).Nullable()
                .WithColumn("Barcode").AsString(250).Nullable()
                .WithColumn("ProductImage").AsString(200).Nullable()
                .WithColumn("UnitPrice").AsDecimal(18, 4).Nullable()
                .WithColumn("CategoryId").AsInt32().Nullable()
                .WithColumn("BrandId").AsInt32().Nullable()
                .WithColumn("UnitId").AsInt32().Nullable()
                .WithColumn("CurrencyId").AsInt32().Nullable()
                .WithColumn("VatRateId").AsInt32().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreatePriceListsTable()
    {
        if (!Schema.Table("PriceLists").Exists())
        {
            Create.Table("PriceLists")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(50).Nullable()
                .WithColumn("Name").AsString(500).NotNullable()
                .WithColumn("Description").AsString(4000).Nullable()
                .WithColumn("ValidFrom").AsDateTime().Nullable()
                .WithColumn("ValidTo").AsDateTime().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("IsDefault").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreatePriceListItemsTable()
    {
        if (!Schema.Table("PriceListItems").Exists())
        {
            Create.Table("PriceListItems")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("PriceListId").AsInt32().NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .WithColumn("UnitPrice").AsDecimal(18, 4).NotNullable()
                .WithColumn("DiscountRate").AsDecimal(18, 2).Nullable()
                .WithColumn("Notes").AsString(500).Nullable();
        }
    }

    #endregion

    #region CUSTOMER MODULE

    private void CreateCustomersTable()
    {
        if (!Schema.Table("Customers").Exists())
        {
            Create.Table("Customers")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("Name").AsString(300).Nullable()
                .WithColumn("FirstName").AsString(1000).Nullable()
                .WithColumn("LastName").AsString(300).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("Phone").AsString(50).Nullable()
                .WithColumn("Phone2").AsString(50).Nullable()
                .WithColumn("Address").AsString(4000).Nullable()
                .WithColumn("CountryId").AsInt32().Nullable()
                .WithColumn("City").AsString(100).Nullable()
                .WithColumn("District").AsString(100).Nullable()
                .WithColumn("TaxOffice").AsString(100).Nullable()
                .WithColumn("TaxNumber").AsString(100).Nullable()
                .WithColumn("UserId").AsInt32().Nullable()
                .WithColumn("VendorTypeId").AsInt32().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().NotNullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateVendorTypeTable()
    {
        if (!Schema.Table("VendorType").Exists())
        {
            Create.Table("VendorType")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateCountryTable()
    {
        if (!Schema.Table("Country").Exists())
        {
            Create.Table("Country")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).Nullable()
                .WithColumn("Code").AsString(200).Nullable()
                .WithColumn("IsoCode3").AsString(100).Nullable()
                .WithColumn("PhoneCode").AsString(100).Nullable()
                .WithColumn("CountryNr").AsString(100).Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable();
        }
    }

    private void CreateBankAccountInformationsTable()
    {
        if (!Schema.Table("BankAccountInformations").Exists())
        {
            Create.Table("BankAccountInformations")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Firm").AsString(100).Nullable()
                .WithColumn("Bank").AsString(50).Nullable()
                .WithColumn("Branch").AsString(50).Nullable()
                .WithColumn("BranchCode").AsString(50).Nullable()
                .WithColumn("AccountNo").AsString(50).Nullable()
                .WithColumn("Iban").AsString(50).Nullable()
                .WithColumn("Swift").AsString(50).Nullable()
                .WithColumn("Currency").AsString(50).Nullable()
                .WithColumn("Origin").AsString(50).Nullable()
                .WithColumn("Payment").AsString(50).Nullable()
                .WithColumn("Shipment").AsString(100).Nullable()
                .WithColumn("TenantId").AsInt32().Nullable();
        }
    }

    #endregion

    #region WAREHOUSE MODULE

    private void CreateWarehousesTable()
    {
        if (!Schema.Table("Warehouses").Exists())
        {
            Create.Table("Warehouses")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("Address").AsString(500).Nullable()
                .WithColumn("City").AsString(100).Nullable()
                .WithColumn("Phone").AsString(50).Nullable()
                .WithColumn("Email").AsString(200).Nullable()
                .WithColumn("ManagerName").AsString(200).Nullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("IsDefault").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateStockEntriesTable()
    {
        if (!Schema.Table("StockEntries").Exists())
        {
            Create.Table("StockEntries")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("EntryNo").AsString(50).NotNullable()
                .WithColumn("WarehouseId").AsInt32().NotNullable()
                .WithColumn("EntryDate").AsDateTime().NotNullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("Status").AsInt16().NotNullable().WithDefaultValue(0)
                .WithColumn("Attachments").AsString(int.MaxValue).Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateStockEntryDetailsTable()
    {
        if (!Schema.Table("StockEntryDetails").Exists())
        {
            Create.Table("StockEntryDetails")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("StockEntryId").AsInt32().NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .WithColumn("Quantity").AsDecimal(18, 4).NotNullable()
                .WithColumn("Unit").AsString(10).Nullable()
                .WithColumn("Currency").AsString(10).Nullable()
                .WithColumn("VatRate").AsDecimal(18, 4).Nullable()
                .WithColumn("UnitPrice").AsDecimal(18, 4).Nullable()
                .WithColumn("Notes").AsString(500).Nullable();
        }
    }

    private void CreateStockExitsTable()
    {
        if (!Schema.Table("StockExits").Exists())
        {
            Create.Table("StockExits")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("ExitNo").AsString(50).NotNullable()
                .WithColumn("WarehouseId").AsInt32().NotNullable()
                .WithColumn("ExitDate").AsDateTime().NotNullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("Status").AsInt16().NotNullable().WithDefaultValue(0)
                .WithColumn("Attachments").AsString(int.MaxValue).Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateStockExitDetailsTable()
    {
        if (!Schema.Table("StockExitDetails").Exists())
        {
            Create.Table("StockExitDetails")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("StockExitId").AsInt32().NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .WithColumn("Quantity").AsDecimal(18, 4).NotNullable()
                .WithColumn("Unit").AsString(10).Nullable()
                .WithColumn("Currency").AsString(10).Nullable()
                .WithColumn("VatRate").AsDecimal(18, 4).Nullable()
                .WithColumn("UnitPrice").AsDecimal(18, 4).Nullable()
                .WithColumn("Notes").AsString(500).Nullable();
        }
    }

    private void CreateWarehouseStockTable()
    {
        if (!Schema.Table("WarehouseStock").Exists())
        {
            Create.Table("WarehouseStock")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("WarehouseId").AsInt32().NotNullable()
                .WithColumn("ProductId").AsInt32().NotNullable()
                .WithColumn("Quantity").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
                .WithColumn("ReservedQuantity").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
                .WithColumn("AvailableQuantity").AsDecimal(18, 4).NotNullable().WithDefaultValue(0)
                .WithColumn("LastUpdateDate").AsDateTime().Nullable();
        }
    }

    private void CreateStockMovementsView()
    {
        Execute.Sql(@"
            IF NOT EXISTS (SELECT * FROM sys.views WHERE name = 'StockMovements')
            BEGIN
                EXEC('
                CREATE VIEW [dbo].[StockMovements] AS
                SELECT
                    CONCAT(''E'', sed.Id) AS Id,
                    ''Entry'' AS MovementType,
                    se.EntryNo AS DocumentNo,
                    se.WarehouseId,
                    w.Code AS WarehouseCode,
                    w.Name AS WarehouseName,
                    sed.ProductId,
                    p.Code AS ProductCode,
                    p.Name AS ProductName,
                    sed.Quantity AS Quantity,
                    se.EntryDate AS MovementDate,
                    se.Status,
                    se.Description,
                    se.InsertDate,
                    se.InsertUserId
                FROM StockEntries se
                INNER JOIN StockEntryDetails sed ON sed.StockEntryId = se.Id
                INNER JOIN Warehouses w ON w.Id = se.WarehouseId
                INNER JOIN Products p ON p.Id = sed.ProductId

                UNION ALL

                SELECT
                    CONCAT(''X'', sxd.Id) AS Id,
                    ''Exit'' AS MovementType,
                    sx.ExitNo AS DocumentNo,
                    sx.WarehouseId,
                    w.Code AS WarehouseCode,
                    w.Name AS WarehouseName,
                    sxd.ProductId,
                    p.Code AS ProductCode,
                    p.Name AS ProductName,
                    -sxd.Quantity AS Quantity,
                    sx.ExitDate AS MovementDate,
                    sx.Status,
                    sx.Description,
                    sx.InsertDate,
                    sx.InsertUserId
                FROM StockExits sx
                INNER JOIN StockExitDetails sxd ON sxd.StockExitId = sx.Id
                INNER JOIN Warehouses w ON w.Id = sx.WarehouseId
                INNER JOIN Products p ON p.Id = sxd.ProductId
                ')
            END
        ");
    }

    #endregion

    #region PRICING MODULE

    private void CreateCurrencyListTable()
    {
        if (!Schema.Table("CurrencyList").Exists())
        {
            Create.Table("CurrencyList")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(5).Nullable()
                .WithColumn("Name").AsString(50).Nullable()
                .WithColumn("CodeType").AsByte().Nullable()
                .WithColumn("Symbol").AsString(5).Nullable()
                .WithColumn("IsActive").AsBoolean().Nullable().WithDefaultValue(true)
                .WithColumn("IsDefaultCurrency").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("DefaultExchangeType").AsInt16().Nullable()
                .WithColumn("TenantId").AsInt32().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable();
        }
    }

    private void CreateDailyExchangesTable()
    {
        if (!Schema.Table("DailyExchanges").Exists())
        {
            Create.Table("DailyExchanges")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("CurrencyId").AsInt32().NotNullable()
                .WithColumn("CurrencyCode").AsFixedLengthString(3).NotNullable()
                .WithColumn("ForexBuying").AsDecimal(18, 4).Nullable()
                .WithColumn("ForexSelling").AsDecimal(18, 4).Nullable()
                .WithColumn("BanknoteBuying").AsDecimal(18, 4).Nullable()
                .WithColumn("BanknoteSelling").AsDecimal(18, 4).Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("DefaultExchangeTypeId").AsByte().Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable();
        }
    }

    private void CreateVatRatesTable()
    {
        if (!Schema.Table("VatRates").Exists())
        {
            Create.Table("VatRates")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Rate").AsDecimal(5, 2).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateUnitsTable()
    {
        if (!Schema.Table("Units").Exists())
        {
            Create.Table("Units")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Name").AsString(100).NotNullable()
                .WithColumn("Code").AsString(20).Nullable()
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    #endregion

    #region EMAIL MODULE

    private void CreateSmtpSettingsTable()
    {
        if (!Schema.Table("SmtpSettings").Exists())
        {
            Create.Table("SmtpSettings")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Host").AsString(200).NotNullable()
                .WithColumn("Port").AsInt32().NotNullable().WithDefaultValue(587)
                .WithColumn("UseSsl").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Username").AsString(100).Nullable()
                .WithColumn("Password").AsString(100).Nullable()
                .WithColumn("FromEmail").AsString(200).NotNullable()
                .WithColumn("FromName").AsString(200).Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("IsDefault").AsBoolean().Nullable().WithDefaultValue(false)
                .WithColumn("Description").AsString(500).Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateEmailTemplatesTable()
    {
        if (!Schema.Table("EmailTemplates").Exists())
        {
            Create.Table("EmailTemplates")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Code").AsString(50).NotNullable()
                .WithColumn("Name").AsString(200).NotNullable()
                .WithColumn("Subject").AsString(500).NotNullable()
                .WithColumn("Body").AsString(int.MaxValue).NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateEmailQueueTable()
    {
        if (!Schema.Table("EmailQueue").Exists())
        {
            Create.Table("EmailQueue")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("ToEmail").AsString(200).NotNullable()
                .WithColumn("ToName").AsString(200).Nullable()
                .WithColumn("CcEmail").AsString(500).Nullable()
                .WithColumn("BccEmail").AsString(500).Nullable()
                .WithColumn("ReplyTo").AsString(200).Nullable()
                .WithColumn("Subject").AsString(500).NotNullable()
                .WithColumn("Body").AsString(int.MaxValue).NotNullable()
                .WithColumn("IsHtml").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("Priority").AsInt32().NotNullable().WithDefaultValue(3)
                .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("RetryCount").AsInt32().NotNullable().WithDefaultValue(0)
                .WithColumn("MaxRetries").AsInt32().NotNullable().WithDefaultValue(3)
                .WithColumn("ScheduledDate").AsDateTime().Nullable()
                .WithColumn("SentDate").AsDateTime().Nullable()
                .WithColumn("ErrorMessage").AsString(int.MaxValue).Nullable()
                .WithColumn("InsertDate").AsDateTime().NotNullable()
                .WithColumn("InsertUserId").AsInt32().Nullable();
        }
    }

    private void CreateEmailLogsTable()
    {
        if (!Schema.Table("EmailLogs").Exists())
        {
            Create.Table("EmailLogs")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("EmailQueueId").AsInt64().NotNullable()
                .WithColumn("Status").AsInt32().NotNullable()
                .WithColumn("StatusMessage").AsString(int.MaxValue).Nullable()
                .WithColumn("SmtpResponse").AsString(int.MaxValue).Nullable()
                .WithColumn("ToAddress").AsString(200).Nullable()
                .WithColumn("ProcessStartTime").AsDateTime().Nullable()
                .WithColumn("ProcessEndTime").AsDateTime().Nullable()
                .WithColumn("Duration").AsInt32().Nullable()
                .WithColumn("InsertDate").AsDateTime().NotNullable();
        }
    }

    private void CreateEmailAttachmentsTable()
    {
        if (!Schema.Table("EmailAttachments").Exists())
        {
            Create.Table("EmailAttachments")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("EmailQueueId").AsInt64().NotNullable()
                .WithColumn("FileName").AsString(255).NotNullable()
                .WithColumn("ContentType").AsString(100).NotNullable()
                .WithColumn("FilePath").AsString(500).Nullable()
                .WithColumn("FileContent").AsBinary(int.MaxValue).Nullable()
                .WithColumn("FileSize").AsInt64().NotNullable()
                .WithColumn("IsInline").AsBoolean().NotNullable().WithDefaultValue(false)
                .WithColumn("ContentId").AsString(100).Nullable()
                .WithColumn("InsertDate").AsDateTime().NotNullable();
        }
    }

    #endregion

    #region SYSTEM MODULE

    private void CreateAuditLogTable()
    {
        if (!Schema.Table("AuditLog").Exists())
        {
            Create.Table("AuditLog")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("EntityType").AsString(100).NotNullable()
                .WithColumn("EntityId").AsString(50).NotNullable()
                .WithColumn("EntityName").AsString(500).Nullable()
                .WithColumn("ActionType").AsString(20).NotNullable()
                .WithColumn("ActionDate").AsDateTime().NotNullable()
                .WithColumn("UserId").AsInt32().Nullable()
                .WithColumn("Username").AsString(100).Nullable()
                .WithColumn("IpAddress").AsString(50).Nullable()
                .WithColumn("OldValues").AsString(int.MaxValue).Nullable()
                .WithColumn("NewValues").AsString(int.MaxValue).Nullable()
                .WithColumn("ChangedFields").AsString(1000).Nullable();
        }
    }

    private void CreateNumberTemplatesTable()
    {
        if (!Schema.Table("NumberTemplates").Exists())
        {
            Create.Table("NumberTemplates")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("Type").AsInt32().NotNullable()
                .WithColumn("Prefix").AsString(50).Nullable()
                .WithColumn("Suffix").AsString(50).Nullable()
                .WithColumn("Length").AsInt32().Nullable()
                .WithColumn("DateFormat").AsString(50).Nullable()
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable();
        }
    }

    private void CreateFileArchiveTable()
    {
        if (!Schema.Table("FileArchive").Exists())
        {
            Create.Table("FileArchive")
                .WithColumn("Id").AsInt32().PrimaryKey().Identity()
                .WithColumn("FileName").AsString(500).NotNullable()
                .WithColumn("FilePath").AsString(1000).NotNullable()
                .WithColumn("FileSize").AsInt64().NotNullable()
                .WithColumn("ContentType").AsString(100).Nullable()
                .WithColumn("EntityType").AsString(100).Nullable()
                .WithColumn("EntityId").AsString(50).Nullable()
                .WithColumn("Description").AsString(1000).Nullable()
                .WithColumn("Active").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("InsertDate").AsDateTime().NotNullable()
                .WithColumn("InsertUserId").AsInt32().Nullable();
        }
    }

    #endregion

    #region FOREIGN KEYS

    private void CreateAllForeignKeys()
    {
        // Products foreign keys
        if (!Schema.Table("Products").Constraint("FK_Products_Brands").Exists())
            Create.ForeignKey("FK_Products_Brands")
                .FromTable("Products").ForeignColumn("BrandId")
                .ToTable("Brands").PrimaryColumn("Id");

        if (!Schema.Table("Products").Constraint("FK_Products_ProductCategory").Exists())
            Create.ForeignKey("FK_Products_ProductCategory")
                .FromTable("Products").ForeignColumn("CategoryId")
                .ToTable("ProductCategory").PrimaryColumn("Id");

        if (!Schema.Table("Products").Constraint("FK_Products_Units").Exists())
            Create.ForeignKey("FK_Products_Units")
                .FromTable("Products").ForeignColumn("UnitId")
                .ToTable("Units").PrimaryColumn("Id");

        if (!Schema.Table("Products").Constraint("FK_Products_CurrencyList").Exists())
            Create.ForeignKey("FK_Products_CurrencyList")
                .FromTable("Products").ForeignColumn("CurrencyId")
                .ToTable("CurrencyList").PrimaryColumn("Id");

        if (!Schema.Table("Products").Constraint("FK_Products_VatRates").Exists())
            Create.ForeignKey("FK_Products_VatRates")
                .FromTable("Products").ForeignColumn("VatRateId")
                .ToTable("VatRates").PrimaryColumn("Id");

        // ProductCategory self-referencing foreign key
        if (!Schema.Table("ProductCategory").Constraint("FK_ProductCategory_Parent").Exists())
            Create.ForeignKey("FK_ProductCategory_Parent")
                .FromTable("ProductCategory").ForeignColumn("ParentId")
                .ToTable("ProductCategory").PrimaryColumn("Id");

        // PriceListItems foreign keys
        if (!Schema.Table("PriceListItems").Constraint("FK_PriceListItems_PriceLists").Exists())
            Create.ForeignKey("FK_PriceListItems_PriceLists")
                .FromTable("PriceListItems").ForeignColumn("PriceListId")
                .ToTable("PriceLists").PrimaryColumn("Id");

        if (!Schema.Table("PriceListItems").Constraint("FK_PriceListItems_Products").Exists())
            Create.ForeignKey("FK_PriceListItems_Products")
                .FromTable("PriceListItems").ForeignColumn("ProductId")
                .ToTable("Products").PrimaryColumn("Id");

        // Customers foreign keys
        if (!Schema.Table("Customers").Constraint("FK_Customers_Country").Exists())
            Create.ForeignKey("FK_Customers_Country")
                .FromTable("Customers").ForeignColumn("CountryId")
                .ToTable("Country").PrimaryColumn("Id");

        if (!Schema.Table("Customers").Constraint("FK_Customers_Users").Exists())
            Create.ForeignKey("FK_Customers_Users")
                .FromTable("Customers").ForeignColumn("UserId")
                .ToTable("Users").PrimaryColumn("UserId");

        if (!Schema.Table("Customers").Constraint("FK_Customers_VendorType").Exists())
            Create.ForeignKey("FK_Customers_VendorType")
                .FromTable("Customers").ForeignColumn("VendorTypeId")
                .ToTable("VendorType").PrimaryColumn("Id");

        // StockEntries foreign keys
        if (!Schema.Table("StockEntries").Constraint("FK_StockEntries_Warehouses").Exists())
            Create.ForeignKey("FK_StockEntries_Warehouses")
                .FromTable("StockEntries").ForeignColumn("WarehouseId")
                .ToTable("Warehouses").PrimaryColumn("Id");

        // StockEntryDetails foreign keys
        if (!Schema.Table("StockEntryDetails").Constraint("FK_StockEntryDetails_StockEntries").Exists())
            Create.ForeignKey("FK_StockEntryDetails_StockEntries")
                .FromTable("StockEntryDetails").ForeignColumn("StockEntryId")
                .ToTable("StockEntries").PrimaryColumn("Id");

        if (!Schema.Table("StockEntryDetails").Constraint("FK_StockEntryDetails_Products").Exists())
            Create.ForeignKey("FK_StockEntryDetails_Products")
                .FromTable("StockEntryDetails").ForeignColumn("ProductId")
                .ToTable("Products").PrimaryColumn("Id");

        // StockExits foreign keys
        if (!Schema.Table("StockExits").Constraint("FK_StockExits_Warehouses").Exists())
            Create.ForeignKey("FK_StockExits_Warehouses")
                .FromTable("StockExits").ForeignColumn("WarehouseId")
                .ToTable("Warehouses").PrimaryColumn("Id");

        // StockExitDetails foreign keys
        if (!Schema.Table("StockExitDetails").Constraint("FK_StockExitDetails_StockExits").Exists())
            Create.ForeignKey("FK_StockExitDetails_StockExits")
                .FromTable("StockExitDetails").ForeignColumn("StockExitId")
                .ToTable("StockExits").PrimaryColumn("Id");

        if (!Schema.Table("StockExitDetails").Constraint("FK_StockExitDetails_Products").Exists())
            Create.ForeignKey("FK_StockExitDetails_Products")
                .FromTable("StockExitDetails").ForeignColumn("ProductId")
                .ToTable("Products").PrimaryColumn("Id");

        // WarehouseStock foreign keys
        if (!Schema.Table("WarehouseStock").Constraint("FK_WarehouseStock_Warehouses").Exists())
            Create.ForeignKey("FK_WarehouseStock_Warehouses")
                .FromTable("WarehouseStock").ForeignColumn("WarehouseId")
                .ToTable("Warehouses").PrimaryColumn("Id");

        if (!Schema.Table("WarehouseStock").Constraint("FK_WarehouseStock_Products").Exists())
            Create.ForeignKey("FK_WarehouseStock_Products")
                .FromTable("WarehouseStock").ForeignColumn("ProductId")
                .ToTable("Products").PrimaryColumn("Id");

        // DailyExchanges foreign keys
        if (!Schema.Table("DailyExchanges").Constraint("FK_DailyExchanges_CurrencyList").Exists())
            Create.ForeignKey("FK_DailyExchanges_CurrencyList")
                .FromTable("DailyExchanges").ForeignColumn("CurrencyId")
                .ToTable("CurrencyList").PrimaryColumn("Id");

        // EmailLogs foreign keys
        if (!Schema.Table("EmailLogs").Constraint("FK_EmailLogs_EmailQueue").Exists())
            Create.ForeignKey("FK_EmailLogs_EmailQueue")
                .FromTable("EmailLogs").ForeignColumn("EmailQueueId")
                .ToTable("EmailQueue").PrimaryColumn("Id");

        // EmailAttachments foreign keys
        if (!Schema.Table("EmailAttachments").Constraint("FK_EmailAttachments_EmailQueue").Exists())
            Create.ForeignKey("FK_EmailAttachments_EmailQueue")
                .FromTable("EmailAttachments").ForeignColumn("EmailQueueId")
                .ToTable("EmailQueue").PrimaryColumn("Id");
    }

    #endregion

    #region INDEXES

    private void CreateAllIndexes()
    {
        // Warehouses unique index on Code
        if (!Schema.Table("Warehouses").Index("UQ_Warehouses_Code").Exists())
            Create.Index("UQ_Warehouses_Code").OnTable("Warehouses")
                .OnColumn("Code").Ascending()
                .WithOptions().Unique();

        // Customers index on Code
        if (!Schema.Table("Customers").Index("IX_Customers_Code").Exists())
            Create.Index("IX_Customers_Code").OnTable("Customers")
                .OnColumn("Code").Ascending();

        // Customers index on Email
        if (!Schema.Table("Customers").Index("IX_Customers_Email").Exists())
            Create.Index("IX_Customers_Email").OnTable("Customers")
                .OnColumn("Email").Ascending();

        // EmailQueue index on Status
        if (!Schema.Table("EmailQueue").Index("IX_EmailQueue_Status").Exists())
            Create.Index("IX_EmailQueue_Status").OnTable("EmailQueue")
                .OnColumn("Status").Ascending();

        // EmailQueue index on ScheduledDate
        if (!Schema.Table("EmailQueue").Index("IX_EmailQueue_ScheduledDate").Exists())
            Create.Index("IX_EmailQueue_ScheduledDate").OnTable("EmailQueue")
                .OnColumn("ScheduledDate").Ascending();

        // EmailTemplates unique index on Code
        if (!Schema.Table("EmailTemplates").Index("UQ_EmailTemplates_Code").Exists())
            Create.Index("UQ_EmailTemplates_Code").OnTable("EmailTemplates")
                .OnColumn("Code").Ascending()
                .WithOptions().Unique();

        // AuditLog index on EntityType + EntityId
        if (!Schema.Table("AuditLog").Index("IX_AuditLog_Entity").Exists())
            Create.Index("IX_AuditLog_Entity").OnTable("AuditLog")
                .OnColumn("EntityType").Ascending()
                .OnColumn("EntityId").Ascending();

        // AuditLog index on ActionDate
        if (!Schema.Table("AuditLog").Index("IX_AuditLog_ActionDate").Exists())
            Create.Index("IX_AuditLog_ActionDate").OnTable("AuditLog")
                .OnColumn("ActionDate").Descending();

        // DailyExchanges index on CurrencyId + InsertDate
        if (!Schema.Table("DailyExchanges").Index("IX_DailyExchanges_Currency_Date").Exists())
            Create.Index("IX_DailyExchanges_Currency_Date").OnTable("DailyExchanges")
                .OnColumn("CurrencyId").Ascending()
                .OnColumn("InsertDate").Descending();

        // WarehouseStock unique index on WarehouseId + ProductId
        if (!Schema.Table("WarehouseStock").Index("UQ_WarehouseStock_Warehouse_Product").Exists())
            Create.Index("UQ_WarehouseStock_Warehouse_Product").OnTable("WarehouseStock")
                .OnColumn("WarehouseId").Ascending()
                .OnColumn("ProductId").Ascending()
                .WithOptions().Unique();

        // PriceListItems index on PriceListId + ProductId (non-unique, allows duplicates)
        if (!Schema.Table("PriceListItems").Index("IX_PriceListItems_List_Product").Exists())
            Create.Index("IX_PriceListItems_List_Product").OnTable("PriceListItems")
                .OnColumn("PriceListId").Ascending()
                .OnColumn("ProductId").Ascending();

        // NumberTemplates index on Type
        if (!Schema.Table("NumberTemplates").Index("IX_NumberTemplates_Type").Exists())
            Create.Index("IX_NumberTemplates_Type").OnTable("NumberTemplates")
                .OnColumn("Type").Ascending();

        // FileArchive index on EntityType + EntityId
        if (!Schema.Table("FileArchive").Index("IX_FileArchive_Entity").Exists())
            Create.Index("IX_FileArchive_Entity").OnTable("FileArchive")
                .OnColumn("EntityType").Ascending()
                .OnColumn("EntityId").Ascending();

        // Products index on CategoryId
        if (!Schema.Table("Products").Index("IX_Products_CategoryId").Exists())
            Create.Index("IX_Products_CategoryId").OnTable("Products")
                .OnColumn("CategoryId").Ascending();

        // Products index on BrandId
        if (!Schema.Table("Products").Index("IX_Products_BrandId").Exists())
            Create.Index("IX_Products_BrandId").OnTable("Products")
                .OnColumn("BrandId").Ascending();
    }

    #endregion

    #region FIX USERS TABLE

    private void StandardizeUsersIsActive()
    {
        // Convert Users.IsActive from SMALLINT to BIT
        if (Schema.Table("Users").Column("IsActive").Exists())
        {
            Execute.Sql(@"
                -- Check if column is not already BIT
                IF EXISTS (
                    SELECT * FROM INFORMATION_SCHEMA.COLUMNS
                    WHERE TABLE_NAME = 'Users'
                    AND COLUMN_NAME = 'IsActive'
                    AND DATA_TYPE = 'smallint'
                )
                BEGIN
                    -- Add temporary column
                    ALTER TABLE Users ADD IsActive_Temp BIT NULL;

                    -- Copy values (SMALLINT -> BIT)
                    UPDATE Users
                    SET IsActive_Temp = CASE
                        WHEN IsActive = 1 THEN 1
                        ELSE 0
                    END;

                    -- Drop old column
                    ALTER TABLE Users DROP COLUMN IsActive;

                    -- Rename temp column
                    EXEC sp_rename 'Users.IsActive_Temp', 'IsActive', 'COLUMN';

                    -- Add NOT NULL constraint with default
                    ALTER TABLE Users ALTER COLUMN IsActive BIT NOT NULL;
                    ALTER TABLE Users ADD CONSTRAINT DF_Users_IsActive DEFAULT 1 FOR IsActive;
                END
            ");
        }
    }

    #endregion
}
