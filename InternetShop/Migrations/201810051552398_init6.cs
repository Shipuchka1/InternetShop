namespace InternetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init6 : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.SaleGoodMaps", "SaleId");
            CreateIndex("dbo.SaleGoodMaps", "GoodId");
            AddForeignKey("dbo.SaleGoodMaps", "GoodId", "dbo.Goods", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SaleGoodMaps", "SaleId", "dbo.Sales", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SaleGoodMaps", "SaleId", "dbo.Sales");
            DropForeignKey("dbo.SaleGoodMaps", "GoodId", "dbo.Goods");
            DropIndex("dbo.SaleGoodMaps", new[] { "GoodId" });
            DropIndex("dbo.SaleGoodMaps", new[] { "SaleId" });
        }
    }
}
