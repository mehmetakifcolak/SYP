using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260520_1100)]
public class DefaultDB_20260520_1100_SeedData : Migration
{
    public override void Up()
    {
        SeedCurrencies();
        SeedUnits();
        SeedVatRates();
        SeedEmailTemplates();
        SeedNumberTemplates();
    }

    public override void Down()
    {
        Execute.Sql("DELETE FROM NumberTemplates WHERE Type = 11");
        Execute.Sql("DELETE FROM EmailTemplates WHERE Code = 'NEW_CUSTOMER_WELCOME'");
        Execute.Sql("DELETE FROM VatRates WHERE Id IN (1, 2, 3, 4, 5)");
        Execute.Sql("DELETE FROM Units WHERE Id IN (1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11)");
        Execute.Sql("DELETE FROM CurrencyList WHERE Id IN (1, 2, 3, 4)");
    }

    private void SeedCurrencies()
    {
        Execute.Sql(@"
            IF NOT EXISTS (SELECT * FROM CurrencyList WHERE Id = 1)
            BEGIN
                SET IDENTITY_INSERT CurrencyList ON;

                INSERT INTO CurrencyList (Id, Code, Name, Symbol, IsActive, IsDefaultCurrency, InsertDate)
                VALUES
                    (1, 'USD', 'US Dollar', '$', 1, 0, GETDATE()),
                    (2, 'EUR', 'Euro', '€', 1, 0, GETDATE()),
                    (3, 'GBP', 'British Pound', '£', 1, 0, GETDATE()),
                    (4, 'TRY', 'Turkish Lira', '₺', 1, 1, GETDATE());

                SET IDENTITY_INSERT CurrencyList OFF;
            END
        ");
    }

    private void SeedUnits()
    {
        Execute.Sql(@"
            IF NOT EXISTS (SELECT * FROM Units WHERE Id = 1)
            BEGIN
                SET IDENTITY_INSERT Units ON;

                INSERT INTO Units (Id, Name, Code, IsActive, InsertDate)
                VALUES
                    (1, 'Adet', 'ADET', 1, GETDATE()),
                    (2, 'Kilogram', 'KG', 1, GETDATE()),
                    (3, 'Gram', 'GR', 1, GETDATE()),
                    (4, 'Litre', 'LT', 1, GETDATE()),
                    (5, 'Mililitre', 'ML', 1, GETDATE()),
                    (6, 'Metre', 'MT', 1, GETDATE()),
                    (7, 'Santimetre', 'CM', 1, GETDATE()),
                    (8, 'Metrekare', 'M2', 1, GETDATE()),
                    (9, 'Paket', 'PAKET', 1, GETDATE()),
                    (10, 'Kutu', 'KUTU', 1, GETDATE()),
                    (11, 'Koli', 'KOLI', 1, GETDATE());

                SET IDENTITY_INSERT Units OFF;
            END
        ");
    }

    private void SeedVatRates()
    {
        Execute.Sql(@"
            IF NOT EXISTS (SELECT * FROM VatRates WHERE Id = 1)
            BEGIN
                SET IDENTITY_INSERT VatRates ON;

                INSERT INTO VatRates (Id, Name, Rate, IsActive, InsertDate)
                VALUES
                    (1, '%0', 0.00, 1, GETDATE()),
                    (2, '%1', 1.00, 1, GETDATE()),
                    (3, '%8', 8.00, 1, GETDATE()),
                    (4, '%10', 10.00, 1, GETDATE()),
                    (5, '%20', 20.00, 1, GETDATE());

                SET IDENTITY_INSERT VatRates OFF;
            END
        ");
    }

    private void SeedEmailTemplates()
    {
        Execute.Sql(@"
            IF NOT EXISTS (SELECT * FROM EmailTemplates WHERE Code = 'NEW_CUSTOMER_WELCOME')
            BEGIN
                INSERT INTO EmailTemplates (Code, Name, Subject, Body, IsActive, InsertDate)
                VALUES (
                    'NEW_CUSTOMER_WELCOME',
                    'Yeni Müşteri Hoş Geldiniz',
                    'Hoş Geldiniz - {{CustomerName}}',
                    '<h2>Merhaba {{CustomerName}},</h2>
                    <p>Sistemimize hoş geldiniz! Hesabınız başarıyla oluşturuldu.</p>
                    <p>Kullanıcı adınız: <strong>{{Username}}</strong></p>
                    <p>Giriş yapmak için <a href=""{{LoginUrl}}"">buraya tıklayın</a>.</p>
                    <p>Saygılarımızla,<br/>{{CompanyName}}</p>',
                    1,
                    GETDATE()
                );
            END
        ");
    }

    private void SeedNumberTemplates()
    {
        Execute.Sql(@"
            IF NOT EXISTS (SELECT * FROM NumberTemplates WHERE Type = 11)
            BEGIN
                INSERT INTO NumberTemplates (Type, Prefix, Length, DateFormat, Active, InsertDate)
                VALUES (
                    11,
                    'FL-',
                    5,
                    'yyyyMM',
                    1,
                    GETDATE()
                );
            END
        ");
    }
}
