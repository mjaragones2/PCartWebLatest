namespace PCartWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoopUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CoopDetailsModels", "CoopLogo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CoopDetailsModels", "CoopLogo");
        }
    }
}
