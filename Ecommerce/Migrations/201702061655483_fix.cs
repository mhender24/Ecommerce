namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.ApplicationUser", newSchema: "Data");
            MoveTable(name: "dbo.IdentityRole", newSchema: "Data");
        }
        
        public override void Down()
        {
            MoveTable(name: "Data.IdentityRole", newSchema: "dbo");
            MoveTable(name: "Data.ApplicationUser", newSchema: "dbo");
        }
    }
}
