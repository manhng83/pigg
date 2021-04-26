namespace Pigg.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomPage",
                c => new
                    {
                        PageId = c.Int(nullable: false, identity: true),
                        ParentCustomPageId = c.Int(),
                        CultureCode = c.String(nullable: false, maxLength: 15),
                        Title = c.String(nullable: false, maxLength: 255),
                        LongTitle = c.String(maxLength: 255),
                        Description = c.String(),
                        PageContent = c.String(),
                        Keywords = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        IsFrontPage = c.Boolean(nullable: false),
                        ShowInList = c.Boolean(nullable: false),
                        OrderInList = c.Decimal(precision: 18, scale: 2),
                        EntityId = c.Guid(nullable: false),
                        CustomPage2_CustomPageId = c.Int(),
                    })
                .PrimaryKey(t => t.PageId)
                .ForeignKey("dbo.CustomPage", t => t.ParentCustomPageId)
                .ForeignKey("dbo.CustomPage", t => t.CustomPage2_CustomPageId)
                .Index(t => t.ParentCustomPageId)
                .Index(t => t.CustomPage2_CustomPageId);
            
            CreateTable(
                "dbo.WebPartPlacement",
                c => new
                    {
                        WebPartPlacementId = c.Int(nullable: false, identity: true),
                        WebPartZone = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.WebPartPlacementId);
            
            CreateTable(
                "dbo.ContentList",
                c => new
                    {
                        ContentListId = c.Int(nullable: false, identity: true),
                        WebPartPlacementId = c.Int(),
                        Visible = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContentListId)
                .ForeignKey("dbo.WebPartPlacement", t => t.WebPartPlacementId)
                .Index(t => t.WebPartPlacementId);
            
            CreateTable(
                "dbo.ContentListItem",
                c => new
                    {
                        ContentListItemId = c.Int(nullable: false, identity: true),
                        ContentListId = c.Int(nullable: false),
                        LanguageIsoCode = c.String(nullable: false, maxLength: 15),
                        ParentContentListItemId = c.Int(),
                        Title = c.String(nullable: false, maxLength: 255),
                        LongTitle = c.String(maxLength: 255),
                        Description = c.String(),
                        PageContent = c.String(),
                        Keywords = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        IsFrontPage = c.Boolean(nullable: false),
                        ShowInList = c.Boolean(nullable: false),
                        OrderInList = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ContentListItemId)
                .ForeignKey("dbo.ContentList", t => t.ContentListId, cascadeDelete: true)
                .Index(t => t.ContentListId);
            
            CreateTable(
                "dbo.CustomSetting",
                c => new
                    {
                        CustomSettingId = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 150),
                        Title = c.String(),
                        Value = c.String(nullable: false),
                        Description = c.String(),
                        Reserved = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.CustomSettingId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.ContentListItem", new[] { "ContentListId" });
            DropIndex("dbo.ContentList", new[] { "WebPartPlacementId" });
            DropIndex("dbo.CustomPage", new[] { "CustomPage2_CustomPageId" });
            DropIndex("dbo.CustomPage", new[] { "ParentCustomPageId" });
            DropForeignKey("dbo.ContentListItem", "ContentListId", "dbo.ContentList");
            DropForeignKey("dbo.ContentList", "WebPartPlacementId", "dbo.WebPartPlacement");
            DropForeignKey("dbo.CustomPage", "CustomPage2_CustomPageId", "dbo.CustomPage");
            DropForeignKey("dbo.CustomPage", "ParentCustomPageId", "dbo.CustomPage");
            DropTable("dbo.People");
            DropTable("dbo.CustomSetting");
            DropTable("dbo.ContentListItem");
            DropTable("dbo.ContentList");
            DropTable("dbo.WebPartPlacement");
            DropTable("dbo.CustomPage");
        }
    }
}
