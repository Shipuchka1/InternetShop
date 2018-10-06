namespace InternetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderEntries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EndPrice = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Good_Id = c.Int(),
                        Order_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Goods", t => t.Good_Id)
                .ForeignKey("dbo.Orders", t => t.Order_Id)
                .Index(t => t.Good_Id)
                .Index(t => t.Order_Id);
            
            CreateIndex("dbo.Orders", "VoucherId");
            AddForeignKey("dbo.Orders", "VoucherId", "dbo.Vouchers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "VoucherId", "dbo.Vouchers");
            DropForeignKey("dbo.OrderEntries", "Order_Id", "dbo.Orders");
            DropForeignKey("dbo.OrderEntries", "Good_Id", "dbo.Goods");
            DropIndex("dbo.Orders", new[] { "VoucherId" });
            DropIndex("dbo.OrderEntries", new[] { "Order_Id" });
            DropIndex("dbo.OrderEntries", new[] { "Good_Id" });
            DropTable("dbo.OrderEntries");
        }
    }
}
