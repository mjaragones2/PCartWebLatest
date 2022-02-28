namespace PCartWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TransactionReturn : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TransactionReturnRefunds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReturnId = c.Int(nullable: false),
                        ModeOfRP = c.String(),
                        Created_at = c.DateTime(),
                        Updated_at = c.DateTime(),
                        ReturnRefund_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ReturnRefunds", t => t.ReturnRefund_Id)
                .Index(t => t.ReturnRefund_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TransactionReturnRefunds", "ReturnRefund_Id", "dbo.ReturnRefunds");
            DropIndex("dbo.TransactionReturnRefunds", new[] { "ReturnRefund_Id" });
            DropTable("dbo.TransactionReturnRefunds");
        }
    }
}
