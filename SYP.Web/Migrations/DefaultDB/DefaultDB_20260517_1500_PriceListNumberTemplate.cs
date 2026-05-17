using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260517_1500)]
public class DefaultDB_20260517_1500_PriceListNumberTemplate : AutoReversingMigration
{
    public override void Up()
    {
        // Fiyat Listesi için NumberTemplate kaydı ekle
        // Type = 11 (FiyatListesi enum değeri)
        Insert.IntoTable("NumberTemplates").Row(new
        {
            Type = 11,
            Prefix = "FL-",
            DateFormat = "yyyy",
            Suffix = "-",
            Length = 3,
            Active = true,
            InsertDate = System.DateTime.Now
        });
    }
}
