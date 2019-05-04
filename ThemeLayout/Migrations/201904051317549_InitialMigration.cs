namespace ThemeLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        EmailId = c.String(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Password = c.String(nullable: false),
                        ConfirmPassword = c.String(),
                        IsEmailVarified = c.Boolean(nullable: false),
                        ActivitionCode = c.Guid(nullable: false),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Review = c.String(),
                        Price = c.Double(nullable: false),
                        Duration = c.String(),
                        Location = c.String(),
                        Facilities = c.String(),
                        Catagory = c.String(),
                        Photo = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Searches",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Ammount = c.Double(nullable: false),
                        Details = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Transports",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        From = c.String(),
                        To = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Facilities = c.String(),
                        BusStation = c.String(),
                        Price = c.Double(nullable: false),
                        Image = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transports");
            DropTable("dbo.Searches");
            DropTable("dbo.Hotels");
            DropTable("dbo.Customers");
        }
    }
}
