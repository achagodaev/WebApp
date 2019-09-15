namespace WebApp.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Sizes", new[] { "Name" });
            AlterColumn("dbo.Sizes", "Name", c => c.String(nullable: false, maxLength: 10));
            CreateIndex("dbo.Sizes", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Sizes", new[] { "Name" });
            AlterColumn("dbo.Sizes", "Name", c => c.String(nullable: false, maxLength: 5));
            CreateIndex("dbo.Sizes", "Name", unique: true);
        }
    }
}
