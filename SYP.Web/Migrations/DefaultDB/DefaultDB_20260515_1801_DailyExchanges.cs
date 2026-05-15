using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[DefaultDB, MigrationKey(20260515_1801)]
public class DefaultDB_20260515_1801_DailyExchanges : AutoReversingMigration
{
    public override void Up()
    {
        if (!Schema.Table("DailyExchanges").Exists())
        {
            this.CreateTableWithId32("DailyExchanges", "Id", s => s
                .WithColumn("CurrencyId").AsInt32().NotNullable()
                .WithColumn("CurrencyCode").AsFixedLengthString(3).NotNullable()
                .WithColumn("ForexBuying").AsDecimal(18, 4).Nullable()
                .WithColumn("ForexSelling").AsDecimal(18, 4).Nullable()
                .WithColumn("BanknoteBuying").AsDecimal(18, 4).Nullable()
                .WithColumn("BanknoteSelling").AsDecimal(18, 4).Nullable()
                .WithColumn("InsertDate").AsDateTime().Nullable()
                .WithColumn("InsertUserId").AsInt32().Nullable()
                .WithColumn("UpdateUserId").AsInt32().Nullable()
                .WithColumn("UpdateDate").AsDateTime().Nullable()
                .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
                .WithColumn("DefaultExchangeTypeId").AsByte().Nullable());
        }
    }
}
