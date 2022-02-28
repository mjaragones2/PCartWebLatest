namespace PCartWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTransactionTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TransactionReturnRefunds", "Receipt", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TransactionReturnRefunds", "Receipt");
        }
    }
}
