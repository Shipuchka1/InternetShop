namespace InternetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OrderEntries", "Good_Id", "dbo.Goods");
            DropIndex("dbo.OrderEntries", new[] { "Good_Id" });
            RenameColumn(table: "dbo.OrderEntries", name: "Good_Id", newName: "GoodId");
            AlterColumn("dbo.OrderEntries", "GoodId", c => c.Int(nullable: false));
            CreateIndex("dbo.OrderEntries", "GoodId");
            AddForeignKey("dbo.OrderEntries", "GoodId", "dbo.Goods", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderEntries", "GoodId", "dbo.Goods");
            DropIndex("dbo.OrderEntries", new[] { "GoodId" });
            AlterColumn("dbo.OrderEntries", "GoodId", c => c.Int());
            RenameColumn(table: "dbo.OrderEntries", name: "GoodId", newName: "Good_Id");
            CreateIndex("dbo.OrderEntries", "Good_Id");
            AddForeignKey("dbo.OrderEntries", "Good_Id", "dbo.Goods", "Id");
        }
    }
}
