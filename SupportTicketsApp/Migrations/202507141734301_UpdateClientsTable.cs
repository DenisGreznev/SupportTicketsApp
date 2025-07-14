namespace SupportTicketsApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateClientsTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Dolgnost", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Employees", "Email", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Employees", "Telefon", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Telefon");
            DropColumn("dbo.Employees", "Email");
            DropColumn("dbo.Employees", "Dolgnost");
        }
    }
}
