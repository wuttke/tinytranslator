namespace TinyTranslatorApplicationServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLog",
                c => new
                    {
                        AuditLogId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        EventDateUTC = c.DateTimeOffset(nullable: false, precision: 7),
                        EventType = c.Int(nullable: false),
                        TableName = c.String(nullable: false, maxLength: 256),
                        RecordId = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.AuditLogId);
            
            CreateTable(
                "dbo.AuditLogDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ColumnName = c.String(nullable: false, maxLength: 256),
                        OriginalValue = c.String(),
                        NewValue = c.String(),
                        AuditLogId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AuditLog", t => t.AuditLogId, cascadeDelete: true)
                .Index(t => t.AuditLogId);
            
            CreateTable(
                "dbo.ProjectLocale",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        LocaleCode = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProjectLocale = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ResourceAssembly",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        FileName = c.String(),
                        FileFormat = c.String(),
                        CreateDateTime = c.DateTime(nullable: false),
                        LastSyncDateTime = c.DateTime(nullable: false),
                        WorstTranslationStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ResourceBundle",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProjectID = c.Int(nullable: false),
                        ResourceAssemblyID = c.Int(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        LastChangeDateTime = c.DateTime(nullable: false),
                        BundleSyncStatus = c.Int(nullable: false),
                        WorstTranslationStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ResourceAssembly", t => t.ResourceAssemblyID, cascadeDelete: true)
                .Index(t => t.ResourceAssemblyID);
            
            CreateTable(
                "dbo.Resource",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        ResourceBundleID = c.Int(nullable: false),
                        Key = c.String(),
                        StringValue = c.String(),
                        BinaryValue = c.Binary(),
                        ResourceType = c.Int(nullable: false),
                        ResourceClass = c.String(),
                        DesignerSupportFlag = c.Boolean(nullable: false),
                        CreateDateTime = c.DateTime(nullable: false),
                        LastChangeDateTime = c.DateTime(nullable: false),
                        ResourceSyncStatus = c.Int(nullable: false),
                        WorstTranslationStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.ResourceBundle", t => t.ResourceBundleID, cascadeDelete: true)
                .Index(t => t.ResourceBundleID);
            
            CreateTable(
                "dbo.ResourceTranslation",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        ResourceAssemblyID = c.Int(nullable: false),
                        ResourceBundleID = c.Int(nullable: false),
                        ResourceID = c.Int(nullable: false),
                        Locale = c.String(),
                        StringValue = c.String(),
                        BinaryValue = c.Binary(),
                        TranslationStatus = c.Int(nullable: false),
                        TranslationBy = c.String(),
                        TranslationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.ResourceAssembly", t => t.ResourceAssemblyID, cascadeDelete: true)
                .ForeignKey("dbo.ResourceBundle", t => t.ResourceBundleID, cascadeDelete: true)
                .ForeignKey("dbo.Resource", t => t.ResourceID, cascadeDelete: true)
                .Index(t => t.ResourceBundleID)
                .Index(t => t.ResourceID);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ResourceTranslation", "ResourceID", "dbo.Resource");
            DropForeignKey("dbo.Resource", "ResourceBundleID", "dbo.ResourceBundle");
            DropForeignKey("dbo.ResourceBundle", "ResourceAssemblyID", "dbo.ResourceAssembly");
            DropForeignKey("dbo.ProjectLocale", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.AuditLogDetail", "AuditLogId", "dbo.AuditLog");
            DropIndex("dbo.ResourceTranslation", new[] { "ResourceID" });
            DropIndex("dbo.Resource", new[] { "ResourceBundleID" });
            DropIndex("dbo.ResourceBundle", new[] { "ResourceAssemblyID" });
            DropIndex("dbo.ProjectLocale", new[] { "ProjectID" });
            DropIndex("dbo.AuditLogDetail", new[] { "AuditLogId" });
            DropTable("dbo.ResourceTranslation");
            DropTable("dbo.Resource");
            DropTable("dbo.ResourceBundle");
            DropTable("dbo.ResourceAssembly");
            DropTable("dbo.Project");
            DropTable("dbo.ProjectLocale");
            DropTable("dbo.AuditLogDetail");
            DropTable("dbo.AuditLog");
        }
    }
}
