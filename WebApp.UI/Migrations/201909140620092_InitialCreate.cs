namespace WebApp.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderProductAddresses",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.AddressId })
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.OrderProducts", t => new { t.OrderId, t.ProductId }, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId })
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.OrderProducts",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.Name, unique: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderProductDeliveries",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        DeliveryId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        DeliveryImagePath = c.String(),
                        AcceptanceId = c.Int(),
                        AcceptanceDate = c.DateTime(),
                        AcceptanceImagePath = c.String(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.DeliveryId })
                .ForeignKey("dbo.OrderProducts", t => new { t.OrderId, t.ProductId }, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId });
            
            CreateTable(
                "dbo.OrderProductDeliveryMaterials",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        DeliveryId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        DeliveryQuantity = c.Double(nullable: false),
                        AcceptanceQuantity = c.Double(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.DeliveryId, t.MaterialId })
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.OrderProductMaterials", t => new { t.OrderId, t.ProductId, t.MaterialId })
                .ForeignKey("dbo.OrderProductDeliveries", t => new { t.OrderId, t.ProductId, t.DeliveryId }, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId, t.MaterialId })
                .Index(t => new { t.OrderId, t.ProductId, t.DeliveryId })
                .Index(t => t.MaterialId);
            
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderProductMaterials",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        MaterialId = c.Int(nullable: false),
                        SupplierId = c.Int(nullable: false),
                        Rate = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.MaterialId })
                .ForeignKey("dbo.Materials", t => t.MaterialId, cascadeDelete: true)
                .ForeignKey("dbo.OrderProducts", t => new { t.OrderId, t.ProductId }, cascadeDelete: true)
                .ForeignKey("dbo.Suppliers", t => t.SupplierId, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId })
                .Index(t => t.MaterialId)
                .Index(t => t.SupplierId);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderProductAddressDeliveries",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        DeliveryId = c.Int(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        DeliveryImagePath = c.String(),
                        AcceptanceId = c.Int(),
                        AcceptanceDate = c.DateTime(),
                        AcceptanceImagePath = c.String(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.AddressId, t.DeliveryId })
                .ForeignKey("dbo.OrderProductAddresses", t => new { t.OrderId, t.ProductId, t.AddressId }, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId, t.AddressId });
            
            CreateTable(
                "dbo.OrderProductAddressDeliverySizes",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        DeliveryId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        DeliveryQuantity = c.Int(nullable: false),
                        AcceptanceQuantity = c.Int(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.AddressId, t.DeliveryId, t.SizeId })
                .ForeignKey("dbo.OrderProductAddressDeliveries", t => new { t.OrderId, t.ProductId, t.AddressId, t.DeliveryId }, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId, t.AddressId, t.DeliveryId })
                .Index(t => t.SizeId);
            
            CreateTable(
                "dbo.Sizes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);
            
            CreateTable(
                "dbo.OrderProductAddressProductionDateSizes",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        ProductionDate = c.DateTime(nullable: false),
                        SizeId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.AddressId, t.ProductionDate, t.SizeId })
                .ForeignKey("dbo.OrderProductAddressProductionDates", t => new { t.OrderId, t.ProductId, t.AddressId, t.ProductionDate }, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId, t.AddressId, t.ProductionDate })
                .Index(t => t.SizeId);
            
            CreateTable(
                "dbo.OrderProductAddressProductionDates",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        ProductionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.AddressId, t.ProductionDate })
                .ForeignKey("dbo.OrderProductAddresses", t => new { t.OrderId, t.ProductId, t.AddressId }, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId, t.AddressId });
            
            CreateTable(
                "dbo.OrderProductAddressSizes",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        AddressId = c.Int(nullable: false),
                        SizeId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.OrderId, t.ProductId, t.AddressId, t.SizeId })
                .ForeignKey("dbo.OrderProductAddresses", t => new { t.OrderId, t.ProductId, t.AddressId }, cascadeDelete: true)
                .ForeignKey("dbo.Sizes", t => t.SizeId, cascadeDelete: true)
                .Index(t => new { t.OrderId, t.ProductId, t.AddressId })
                .Index(t => t.SizeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderProductAddressSizes", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.OrderProductAddressSizes", new[] { "OrderId", "ProductId", "AddressId" }, "dbo.OrderProductAddresses");
            DropForeignKey("dbo.OrderProductAddressProductionDateSizes", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.OrderProductAddressProductionDateSizes", new[] { "OrderId", "ProductId", "AddressId", "ProductionDate" }, "dbo.OrderProductAddressProductionDates");
            DropForeignKey("dbo.OrderProductAddressProductionDates", new[] { "OrderId", "ProductId", "AddressId" }, "dbo.OrderProductAddresses");
            DropForeignKey("dbo.OrderProductAddressDeliverySizes", "SizeId", "dbo.Sizes");
            DropForeignKey("dbo.OrderProductAddressDeliverySizes", new[] { "OrderId", "ProductId", "AddressId", "DeliveryId" }, "dbo.OrderProductAddressDeliveries");
            DropForeignKey("dbo.OrderProductAddressDeliveries", new[] { "OrderId", "ProductId", "AddressId" }, "dbo.OrderProductAddresses");
            DropForeignKey("dbo.OrderProducts", "ProductId", "dbo.Products");
            DropForeignKey("dbo.OrderProductDeliveryMaterials", new[] { "OrderId", "ProductId", "DeliveryId" }, "dbo.OrderProductDeliveries");
            DropForeignKey("dbo.OrderProductMaterials", "SupplierId", "dbo.Suppliers");
            DropForeignKey("dbo.OrderProductDeliveryMaterials", new[] { "OrderId", "ProductId", "MaterialId" }, "dbo.OrderProductMaterials");
            DropForeignKey("dbo.OrderProductMaterials", new[] { "OrderId", "ProductId" }, "dbo.OrderProducts");
            DropForeignKey("dbo.OrderProductMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.OrderProductDeliveryMaterials", "MaterialId", "dbo.Materials");
            DropForeignKey("dbo.OrderProductDeliveries", new[] { "OrderId", "ProductId" }, "dbo.OrderProducts");
            DropForeignKey("dbo.OrderProductAddresses", new[] { "OrderId", "ProductId" }, "dbo.OrderProducts");
            DropForeignKey("dbo.OrderProducts", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.OrderProductAddresses", "AddressId", "dbo.Addresses");
            DropIndex("dbo.OrderProductAddressSizes", new[] { "SizeId" });
            DropIndex("dbo.OrderProductAddressSizes", new[] { "OrderId", "ProductId", "AddressId" });
            DropIndex("dbo.OrderProductAddressProductionDates", new[] { "OrderId", "ProductId", "AddressId" });
            DropIndex("dbo.OrderProductAddressProductionDateSizes", new[] { "SizeId" });
            DropIndex("dbo.OrderProductAddressProductionDateSizes", new[] { "OrderId", "ProductId", "AddressId", "ProductionDate" });
            DropIndex("dbo.Sizes", new[] { "Name" });
            DropIndex("dbo.OrderProductAddressDeliverySizes", new[] { "SizeId" });
            DropIndex("dbo.OrderProductAddressDeliverySizes", new[] { "OrderId", "ProductId", "AddressId", "DeliveryId" });
            DropIndex("dbo.OrderProductAddressDeliveries", new[] { "OrderId", "ProductId", "AddressId" });
            DropIndex("dbo.Products", new[] { "Name" });
            DropIndex("dbo.Suppliers", new[] { "Name" });
            DropIndex("dbo.OrderProductMaterials", new[] { "SupplierId" });
            DropIndex("dbo.OrderProductMaterials", new[] { "MaterialId" });
            DropIndex("dbo.OrderProductMaterials", new[] { "OrderId", "ProductId" });
            DropIndex("dbo.Materials", new[] { "Name" });
            DropIndex("dbo.OrderProductDeliveryMaterials", new[] { "MaterialId" });
            DropIndex("dbo.OrderProductDeliveryMaterials", new[] { "OrderId", "ProductId", "DeliveryId" });
            DropIndex("dbo.OrderProductDeliveryMaterials", new[] { "OrderId", "ProductId", "MaterialId" });
            DropIndex("dbo.OrderProductDeliveries", new[] { "OrderId", "ProductId" });
            DropIndex("dbo.Customers", new[] { "Name" });
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropIndex("dbo.Orders", new[] { "Name" });
            DropIndex("dbo.OrderProducts", new[] { "ProductId" });
            DropIndex("dbo.OrderProducts", new[] { "OrderId" });
            DropIndex("dbo.OrderProductAddresses", new[] { "AddressId" });
            DropIndex("dbo.OrderProductAddresses", new[] { "OrderId", "ProductId" });
            DropIndex("dbo.Addresses", new[] { "Name" });
            DropTable("dbo.OrderProductAddressSizes");
            DropTable("dbo.OrderProductAddressProductionDates");
            DropTable("dbo.OrderProductAddressProductionDateSizes");
            DropTable("dbo.Sizes");
            DropTable("dbo.OrderProductAddressDeliverySizes");
            DropTable("dbo.OrderProductAddressDeliveries");
            DropTable("dbo.Products");
            DropTable("dbo.Suppliers");
            DropTable("dbo.OrderProductMaterials");
            DropTable("dbo.Materials");
            DropTable("dbo.OrderProductDeliveryMaterials");
            DropTable("dbo.OrderProductDeliveries");
            DropTable("dbo.Customers");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderProducts");
            DropTable("dbo.OrderProductAddresses");
            DropTable("dbo.Addresses");
        }
    }
}
