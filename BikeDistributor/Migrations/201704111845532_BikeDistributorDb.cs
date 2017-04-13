namespace BikeDistributor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BikeDistributorDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bike",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(nullable: false, maxLength: 50, unicode: false),
                        Model = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(unicode: false),
                        BikeSize = c.String(maxLength: 50, unicode: false),
                        BikeType = c.String(maxLength: 50, unicode: false),
                        InventoryStatus = c.String(maxLength: 50, unicode: false),
                        Price = c.Decimal(precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Address = c.String(maxLength: 150, unicode: false),
                        WebAddress = c.String(unicode: false),
                        PhoneNumber = c.String(nullable: false, maxLength: 50, unicode: false),
                        PrimaryContact = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(),
                        PaymentMethod = c.String(maxLength: 50, unicode: false),
                        Company_Id = c.Int(),
                        Receipt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Company", t => t.Company_Id)
                .ForeignKey("dbo.Receipt", t => t.Receipt_Id)
                .Index(t => t.Company_Id)
                .Index(t => t.Receipt_Id);
            
            CreateTable(
                "dbo.Receipt",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderNumber = c.String(maxLength: 150, unicode: false),
                        Body = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlaceOrderViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Brand = c.String(),
                        Model = c.String(),
                        BikeSize = c.String(),
                        PaymentMethod = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        Order_Id = c.Int(),
                        Receipt_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id)
                .ForeignKey("dbo.Receipt", t => t.Receipt_Id)
                .Index(t => t.Order_Id)
                .Index(t => t.Receipt_Id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlaceOrderViewModels", "Receipt_Id", "dbo.Receipt");
            DropForeignKey("dbo.PlaceOrderViewModels", "Order_Id", "dbo.Order");
            DropForeignKey("dbo.Order", "Receipt_Id", "dbo.Receipt");
            DropForeignKey("dbo.Order", "Company_Id", "dbo.Company");
            DropIndex("dbo.PlaceOrderViewModels", new[] { "Receipt_Id" });
            DropIndex("dbo.PlaceOrderViewModels", new[] { "Order_Id" });
            DropIndex("dbo.Order", new[] { "Receipt_Id" });
            DropIndex("dbo.Order", new[] { "Company_Id" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.PlaceOrderViewModels");
            DropTable("dbo.Receipt");
            DropTable("dbo.Order");
            DropTable("dbo.Company");
            DropTable("dbo.Bike");
        }
    }
}
