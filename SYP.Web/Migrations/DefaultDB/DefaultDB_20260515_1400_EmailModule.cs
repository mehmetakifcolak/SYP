using FluentMigrator;

namespace SYP.Migrations.DefaultDB;

[Migration(20260515_1400)]
public class DefaultDB_20260515_1400_EmailModule : AutoReversingMigration
{
    public override void Up()
    {
        // SmtpSettings tablosu
        Create.Table("SmtpSettings")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("Name").AsString(100).NotNullable()
            .WithColumn("Host").AsString(200).NotNullable()
            .WithColumn("Port").AsInt32().NotNullable().WithDefaultValue(587)
            .WithColumn("UseSsl").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("Username").AsString(200).Nullable()
            .WithColumn("Password").AsString(500).Nullable()
            .WithColumn("FromAddress").AsString(200).NotNullable()
            .WithColumn("FromName").AsString(200).Nullable()
            .WithColumn("IsDefault").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("MaxRetryCount").AsInt32().NotNullable().WithDefaultValue(3)
            .WithColumn("RetryIntervalMinutes").AsInt32().NotNullable().WithDefaultValue(15)
            .WithColumn("DailyLimit").AsInt32().Nullable()
            .WithColumn("InsertDate").AsDateTime().NotNullable()
            .WithColumn("InsertUserId").AsInt32().NotNullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        // EmailTemplates tablosu
        Create.Table("EmailTemplates")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("TemplateKey").AsString(100).NotNullable()
            .WithColumn("Name").AsString(200).NotNullable()
            .WithColumn("Subject").AsString(500).NotNullable()
            .WithColumn("Body").AsString(int.MaxValue).NotNullable()
            .WithColumn("BodyText").AsString(int.MaxValue).Nullable()
            .WithColumn("LanguageId").AsString(10).Nullable()
            .WithColumn("Category").AsString(100).Nullable()
            .WithColumn("Description").AsString(500).Nullable()
            .WithColumn("IsActive").AsBoolean().NotNullable().WithDefaultValue(true)
            .WithColumn("InsertDate").AsDateTime().NotNullable()
            .WithColumn("InsertUserId").AsInt32().NotNullable()
            .WithColumn("UpdateDate").AsDateTime().Nullable()
            .WithColumn("UpdateUserId").AsInt32().Nullable();

        Create.Index("IX_EmailTemplates_TemplateKey")
            .OnTable("EmailTemplates")
            .OnColumn("TemplateKey").Ascending()
            .WithOptions().Unique();

        // EmailQueue tablosu
        Create.Table("EmailQueue")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("SmtpSettingsId").AsInt32().Nullable()
                .ForeignKey("FK_EmailQueue_SmtpSettings", "SmtpSettings", "Id")
            .WithColumn("TemplateId").AsInt32().Nullable()
                .ForeignKey("FK_EmailQueue_EmailTemplates", "EmailTemplates", "Id")
            .WithColumn("ToAddresses").AsString(int.MaxValue).NotNullable()
            .WithColumn("CcAddresses").AsString(int.MaxValue).Nullable()
            .WithColumn("BccAddresses").AsString(int.MaxValue).Nullable()
            .WithColumn("FromAddress").AsString(200).Nullable()
            .WithColumn("FromName").AsString(200).Nullable()
            .WithColumn("ReplyToAddress").AsString(200).Nullable()
            .WithColumn("Subject").AsString(500).NotNullable()
            .WithColumn("Body").AsString(int.MaxValue).NotNullable()
            .WithColumn("BodyText").AsString(int.MaxValue).Nullable()
            .WithColumn("TemplateData").AsString(int.MaxValue).Nullable()
            .WithColumn("Priority").AsInt32().NotNullable().WithDefaultValue(2)
            .WithColumn("Status").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("ScheduledAt").AsDateTime().Nullable()
            .WithColumn("ProcessedAt").AsDateTime().Nullable()
            .WithColumn("SentAt").AsDateTime().Nullable()
            .WithColumn("ErrorMessage").AsString(int.MaxValue).Nullable()
            .WithColumn("RetryCount").AsInt32().NotNullable().WithDefaultValue(0)
            .WithColumn("NextRetryAt").AsDateTime().Nullable()
            .WithColumn("ReferenceType").AsString(100).Nullable()
            .WithColumn("ReferenceId").AsString(100).Nullable()
            .WithColumn("InsertDate").AsDateTime().NotNullable()
            .WithColumn("InsertUserId").AsInt32().NotNullable();

        Create.Index("IX_EmailQueue_Status_Priority")
            .OnTable("EmailQueue")
            .OnColumn("Status").Ascending()
            .OnColumn("Priority").Ascending()
            .OnColumn("ScheduledAt").Ascending();

        Create.Index("IX_EmailQueue_Reference")
            .OnTable("EmailQueue")
            .OnColumn("ReferenceType").Ascending()
            .OnColumn("ReferenceId").Ascending();

        // EmailAttachments tablosu
        Create.Table("EmailAttachments")
            .WithColumn("Id").AsInt32().PrimaryKey().Identity()
            .WithColumn("EmailQueueId").AsInt64().NotNullable()
                .ForeignKey("FK_EmailAttachments_EmailQueue", "EmailQueue", "Id")
            .WithColumn("FileName").AsString(255).NotNullable()
            .WithColumn("ContentType").AsString(100).NotNullable()
            .WithColumn("FilePath").AsString(500).Nullable()
            .WithColumn("FileContent").AsBinary(int.MaxValue).Nullable()
            .WithColumn("FileSize").AsInt64().NotNullable()
            .WithColumn("IsInline").AsBoolean().NotNullable().WithDefaultValue(false)
            .WithColumn("ContentId").AsString(100).Nullable()
            .WithColumn("InsertDate").AsDateTime().NotNullable();

        // EmailLogs tablosu
        Create.Table("EmailLogs")
            .WithColumn("Id").AsInt64().PrimaryKey().Identity()
            .WithColumn("EmailQueueId").AsInt64().NotNullable()
                .ForeignKey("FK_EmailLogs_EmailQueue", "EmailQueue", "Id")
            .WithColumn("Status").AsInt32().NotNullable()
            .WithColumn("StatusMessage").AsString(int.MaxValue).Nullable()
            .WithColumn("SmtpResponse").AsString(int.MaxValue).Nullable()
            .WithColumn("ToAddress").AsString(200).Nullable()
            .WithColumn("ProcessStartTime").AsDateTime().Nullable()
            .WithColumn("ProcessEndTime").AsDateTime().Nullable()
            .WithColumn("Duration").AsInt32().Nullable()
            .WithColumn("InsertDate").AsDateTime().NotNullable();

        Create.Index("IX_EmailLogs_EmailQueueId")
            .OnTable("EmailLogs")
            .OnColumn("EmailQueueId").Ascending();
    }
}
