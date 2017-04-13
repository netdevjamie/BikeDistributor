namespace BikeDistributor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BikeDistributorDb1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bike", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bike", "Quantity");
        }
    }
}
