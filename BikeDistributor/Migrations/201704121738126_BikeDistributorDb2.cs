namespace BikeDistributor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BikeDistributorDb2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PlaceOrderViewModels", "Brand", c => c.String(nullable: false));
            AlterColumn("dbo.PlaceOrderViewModels", "Model", c => c.String(nullable: false));
            AlterColumn("dbo.PlaceOrderViewModels", "BikeSize", c => c.String(nullable: false));
            AlterColumn("dbo.PlaceOrderViewModels", "PaymentMethod", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PlaceOrderViewModels", "PaymentMethod", c => c.String());
            AlterColumn("dbo.PlaceOrderViewModels", "BikeSize", c => c.String());
            AlterColumn("dbo.PlaceOrderViewModels", "Model", c => c.String());
            AlterColumn("dbo.PlaceOrderViewModels", "Brand", c => c.String());
        }
    }
}
