namespace InternetShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Goods", "UrlImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Goods", "UrlImage");
        }
    }
}
