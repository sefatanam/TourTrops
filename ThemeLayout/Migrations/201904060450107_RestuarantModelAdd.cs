namespace ThemeLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RestuarantModelAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restuarants",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Restuarants");
        }
    }
}
