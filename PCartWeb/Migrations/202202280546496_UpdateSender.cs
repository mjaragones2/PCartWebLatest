namespace PCartWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSender : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionReturnRefunds", "SenderName", c => c.String());
            AddColumn("dbo.TransactionReturnRefunds", "Contact", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransactionReturnRefunds", "Contact");
            DropColumn("dbo.TransactionReturnRefunds", "SenderName");
        }
    }
}
