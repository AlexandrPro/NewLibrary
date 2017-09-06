namespace Library.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PublishingHouses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 200),
                        Addres = c.String(nullable: false, maxLength: 500),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PublishingHouseBooks",
                c => new
                    {
                        PublishingHouse_Id = c.String(nullable: false, maxLength: 128),
                        Book_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.PublishingHouse_Id, t.Book_Id })
                .ForeignKey("dbo.PublishingHouses", t => t.PublishingHouse_Id, cascadeDelete: true)
                .ForeignKey("dbo.Book", t => t.Book_Id, cascadeDelete: true)
                .Index(t => t.PublishingHouse_Id)
                .Index(t => t.Book_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublishingHouseBooks", "Book_Id", "dbo.Book");
            DropForeignKey("dbo.PublishingHouseBooks", "PublishingHouse_Id", "dbo.PublishingHouses");
            DropIndex("dbo.PublishingHouseBooks", new[] { "Book_Id" });
            DropIndex("dbo.PublishingHouseBooks", new[] { "PublishingHouse_Id" });
            DropTable("dbo.PublishingHouseBooks");
            DropTable("dbo.PublishingHouses");
        }
    }
}
