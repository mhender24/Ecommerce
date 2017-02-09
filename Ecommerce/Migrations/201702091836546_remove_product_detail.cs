namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_product_detail : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "ProductDetailId", "dbo.ProductDetail");
            DropIndex("dbo.Product", new[] { "ProductDetailId" });
            AddColumn("dbo.Product", "Description", c => c.String());
            AddColumn("dbo.Product", "ProductImage", c => c.String());
            DropColumn("dbo.Product", "ProductDetailId");
            DropTable("dbo.ProductDetail");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ProductDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        ProductImage = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Product", "ProductDetailId", c => c.Int(nullable: false));
            DropColumn("dbo.Product", "ProductImage");
            DropColumn("dbo.Product", "Description");
            CreateIndex("dbo.Product", "ProductDetailId");
            AddForeignKey("dbo.Product", "ProductDetailId", "dbo.ProductDetail", "Id", cascadeDelete: true);
        }
    }
}
