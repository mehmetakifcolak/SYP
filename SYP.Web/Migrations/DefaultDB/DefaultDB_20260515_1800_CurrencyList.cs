using FluentMigrator;
using System;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260515_1800)]
public class DefaultDB_20260515_1800_CurrencyList : Migration
{
    public override void Up()
    {
        if (!Schema.Table("CurrencyList").Exists())
        {
            this.CreateTableWithId32("CurrencyList", "Id", s => s
                .WithColumn("Code").AsString(10).Nullable()
                .WithColumn("Name").AsString(100).Nullable()
                .WithColumn("CodeType").AsInt16().Nullable()
                .WithColumn("Symbol").AsString(10).Nullable()
                .WithColumn("IsActive").AsBoolean().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("IsDefaultCurrency").AsBoolean().Nullable()
                .WithColumn("DefaultExchangeType").AsInt16().Nullable()
                .WithColumn("TenantId").AsInt32().Nullable());

            Execute.Sql("SET IDENTITY_INSERT [CurrencyList] ON");

            Insert.IntoTable("CurrencyList")
                .Row(new
                {
                    Id = 1,
                    Code = "USD",
                    Name = "ABD Dolari",
                    CodeType = 1,
                    Symbol = "$",
                    IsActive = true,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now
                })
                .Row(new
                {
                    Id = 2,
                    Code = "EUR",
                    Name = "Euro",
                    CodeType = 20,
                    Symbol = "\u20AC",
                    IsActive = true,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now,
                    IsDefaultCurrency = true,
                    DefaultExchangeType = 1
                })
                .Row(new
                {
                    Id = 3,
                    Code = "GBP",
                    Name = "Ingiliz Sterlini",
                    CodeType = 17,
                    Symbol = "\u00A3",
                    IsActive = true,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now
                })
                .Row(new
                {
                    Id = 4,
                    Code = "TRY",
                    Name = "Turk Lirasi",
                    CodeType = 53,
                    Symbol = "\u20BA",
                    IsActive = true,
                    InsertUserId = 1,
                    InsertDate = DateTime.Now
                });

            Execute.Sql("SET IDENTITY_INSERT [CurrencyList] OFF");
        }
    }

    public override void Down()
    {
        Delete.Table("CurrencyList");
    }
}
