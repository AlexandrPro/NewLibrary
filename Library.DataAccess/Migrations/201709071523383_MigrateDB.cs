namespace Library.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookInPublishingHouses", "Book_Id", "dbo.Book");
            DropForeignKey("dbo.BookInPublishingHouses", "PublishingHouse_Id", "dbo.PublishingHouses");
            DropIndex("dbo.BookInPublishingHouses", new[] { "Book_Id" });
            DropIndex("dbo.BookInPublishingHouses", new[] { "PublishingHouse_Id" });
            AlterColumn("dbo.BookInPublishingHouses", "Book_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.BookInPublishingHouses", "PublishingHouse_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.BookInPublishingHouses", "Book_Id");
            CreateIndex("dbo.BookInPublishingHouses", "PublishingHouse_Id");
            AddForeignKey("dbo.BookInPublishingHouses", "Book_Id", "dbo.Book", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BookInPublishingHouses", "PublishingHouse_Id", "dbo.PublishingHouses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookInPublishingHouses", "PublishingHouse_Id", "dbo.PublishingHouses");
            DropForeignKey("dbo.BookInPublishingHouses", "Book_Id", "dbo.Book");
            DropIndex("dbo.BookInPublishingHouses", new[] { "PublishingHouse_Id" });
            DropIndex("dbo.BookInPublishingHouses", new[] { "Book_Id" });
            AlterColumn("dbo.BookInPublishingHouses", "PublishingHouse_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.BookInPublishingHouses", "Book_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.BookInPublishingHouses", "PublishingHouse_Id");
            CreateIndex("dbo.BookInPublishingHouses", "Book_Id");
            AddForeignKey("dbo.BookInPublishingHouses", "PublishingHouse_Id", "dbo.PublishingHouses", "Id");
            AddForeignKey("dbo.BookInPublishingHouses", "Book_Id", "dbo.Book", "Id");
        }
    }
}
