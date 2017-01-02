namespace PriceCompareModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Chains",
                c => new
                    {
                        ChainID = c.Long(nullable: false),
                        ChainName = c.String(),
                    })
                .PrimaryKey(t => t.ChainID);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        StoreID = c.Long(nullable: false, identity: true),
                        StoreCode = c.Long(nullable: false),
                        StoreName = c.String(),
                        ChainID = c.Long(nullable: false),
                        Address = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.StoreID)
                .ForeignKey("dbo.Chains", t => t.ChainID, cascadeDelete: true)
                .Index(t => t.ChainID);
            
            CreateTable(
                "dbo.Prices",
                c => new
                    {
                        PriceID = c.Long(nullable: false, identity: true),
                        ItemID = c.Long(nullable: false),
                        StoreID = c.Long(nullable: false),
                        ItemPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.PriceID)
                .ForeignKey("dbo.Items", t => t.ItemID, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreID, cascadeDelete: true)
                .Index(t => t.ItemID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.Items",
                c => new
                    {
                        ItemID = c.Long(nullable: false),
                        ItemName = c.String(),
                        Description = c.String(),
                        ManufacturerName = c.String(),
                        Quantity = c.String(),
                        UnitQty = c.String(),
                    })
                .PrimaryKey(t => t.ItemID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prices", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.Prices", "ItemID", "dbo.Items");
            DropForeignKey("dbo.Stores", "ChainID", "dbo.Chains");
            DropIndex("dbo.Prices", new[] { "StoreID" });
            DropIndex("dbo.Prices", new[] { "ItemID" });
            DropIndex("dbo.Stores", new[] { "ChainID" });
            DropTable("dbo.Items");
            DropTable("dbo.Prices");
            DropTable("dbo.Stores");
            DropTable("dbo.Chains");
        }
    }
}
