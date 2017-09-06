namespace Library.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDB1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublishingHouses", "Address", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.PublishingHouses", "Addres");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PublishingHouses", "Addres", c => c.String(nullable: false, maxLength: 500));
            DropColumn("dbo.PublishingHouses", "Address");
        }
    }
}
