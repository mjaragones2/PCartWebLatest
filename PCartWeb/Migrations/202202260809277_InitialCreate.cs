namespace PCartWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountsReceiveds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CoopId = c.Int(nullable: false),
                        AccountId = c.String(),
                        Receipt = c.String(),
                        ModeOfPayment = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Email = c.String(),
                        Fullname = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderCancels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserOrder_ID = c.Int(nullable: false),
                        CancelledBy = c.String(),
                        Reason = c.String(),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.CategoryDetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ChatMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoopChatId = c.Int(nullable: false),
                        From = c.String(),
                        MessageBody = c.String(),
                        DateSent = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommissionTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CommissionSales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoopCode = c.Int(nullable: false),
                        CoopAdminId = c.String(),
                        CommissionFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserOrderID = c.Int(nullable: false),
                        Status = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Complaints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Fullname = c.String(),
                        Category = c.String(),
                        Reason = c.String(),
                        Description = c.String(),
                        PostFile = c.String(),
                        Status = c.String(),
                        DateCreated = c.String(),
                        DateUpdated = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoopAdminDetailsModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Image = c.String(),
                        Gender = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Bdate = c.String(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Coop_code = c.Int(nullable: false),
                        Approval = c.String(),
                        Email = c.String(),
                        IsLocked = c.String(),
                        IsResign = c.String(),
                        DateResigned = c.String(),
                        PreviousEmail = c.String(),
                        Coop_Id = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CoopDetailsModels", t => t.Coop_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Coop_Id);
            
            CreateTable(
                "dbo.CoopDetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoopName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Contact = c.String(),
                        Approval = c.String(),
                        IsLocked = c.String(),
                        MembershipForm = c.String(),
                        Coop_Created = c.DateTime(nullable: false),
                        Coop_Updated = c.DateTime(nullable: false),
                        DateAccLocked = c.String(),
                        DateAccRetrieved = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoopChats",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoopId = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoopDocumentImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Document_image = c.String(),
                        Userid = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.Userid);
            
            CreateTable(
                "dbo.CoopLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Geolocation = c.Geography(),
                        CoopId = c.String(),
                        Coop_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoopDetailsModels", t => t.Coop_Id)
                .Index(t => t.Coop_Id);
            
            CreateTable(
                "dbo.CoopMemberDiscounts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MemberDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        COOP_ID = c.Int(nullable: false),
                        COOP_AdminID = c.String(),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CoopApplicationRecords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoopName = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Contact = c.String(),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Image = c.String(),
                        Gender = c.String(nullable: false),
                        Status = c.String(nullable: false),
                        AdminContact = c.String(nullable: false),
                        AdminAddress = c.String(nullable: false),
                        Bdate = c.String(nullable: false),
                        Approval = c.String(),
                        Email = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        DateAnswered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CoopApplicationDocus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        COOP_RecordID = c.Int(nullable: false),
                        Document_image = c.String(nullable: false),
                        CoopRecord_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoopApplicationRecords", t => t.CoopRecord_Id)
                .Index(t => t.CoopRecord_Id);
            
            CreateTable(
                "dbo.ProductCosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProdId = c.Int(nullable: false),
                        Created_at = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductDetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product_Name = c.String(nullable: false),
                        Product_image = c.String(nullable: false),
                        Product_desc = c.String(nullable: false),
                        Product_qty = c.Int(nullable: false),
                        Product_sold = c.Int(nullable: false),
                        DiscountedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_status = c.String(),
                        ExpiryDate = c.String(),
                        Category_Id = c.Int(nullable: false),
                        Categoryname = c.String(),
                        CoopAdminId = c.String(),
                        CoopId = c.Int(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        Prod_Created_at = c.DateTime(nullable: false),
                        Prod_Updated_at = c.DateTime(nullable: false),
                        Date_ApprovalStatus = c.DateTime(nullable: false),
                        Category_Id1 = c.Int(),
                        Coop_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CategoryDetailsModels", t => t.Category_Id1)
                .ForeignKey("dbo.AspNetUsers", t => t.Coop_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.Category_Id1)
                .Index(t => t.Coop_Id);
            
            CreateTable(
                "dbo.DeliveryStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserOrderId = c.Int(nullable: false),
                        DriverId = c.String(),
                        PickUpDate = c.String(),
                        PickUpSuccessDate = c.DateTime(),
                        ExpectedDeldate = c.DateTime(),
                        DateDelivered = c.DateTime(),
                        Status = c.String(),
                        ReturnedReason = c.String(),
                        Proof = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscountedProducts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DiscountID = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DiscountModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        CoopID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Percent = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateStart = c.String(nullable: false),
                        DateEnd = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DriverDetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Image = c.String(),
                        Driver_License = c.String(nullable: false),
                        Contact = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Bdate = c.String(nullable: false),
                        CStatus = c.String(nullable: false),
                        PlateNum = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        UserId = c.String(nullable: false, maxLength: 128),
                        CoopId = c.String(),
                        IsActive = c.String(),
                        IsAvailable = c.Boolean(nullable: false),
                        IsOnDuty = c.Boolean(nullable: false),
                        Coop_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoopDetailsModels", t => t.Coop_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.Coop_Id);
            
            CreateTable(
                "dbo.EWalletHistories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EWallet_ID = c.Int(nullable: false),
                        Action = c.String(),
                        Description = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Address = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Geolocation = c.Geography(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Role = c.String(),
                        Logs = c.String(),
                        Date = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductManufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Manufacturer = c.String(),
                        ProdId = c.Int(nullable: false),
                        Created_at = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.COOPMembershipFees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        COOP_ID = c.String(),
                        COOP_AdminID = c.String(),
                        MemFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CustomerMemberships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Formpath = c.String(),
                        UserId = c.String(maxLength: 128),
                        RequestStatus = c.String(),
                        Coop_code = c.String(),
                        Date_requested = c.String(),
                        Date_joined = c.String(),
                        Coop_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CoopDetailsModels", t => t.Coop_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Coop_Id);
            
            CreateTable(
                "dbo.NotificationModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ToRole = c.String(),
                        ToCOOP_ID = c.String(),
                        ToUser = c.String(),
                        NotifFrom = c.String(),
                        NotifHeader = c.String(),
                        NotifMessage = c.String(),
                        NavigateURL = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        DateReceived = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProdCheckouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Int(nullable: false),
                        OrderId = c.String(),
                        ProductId = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        PartialTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Checkout_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserCheckouts", t => t.Checkout_Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .Index(t => t.Checkout_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.UserCheckouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        CoopId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ExternalImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Product_image = c.String(),
                        ProductId = c.String(),
                        Userid = c.String(maxLength: 128),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Userid)
                .Index(t => t.Userid)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.PriceTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProdId = c.Int(nullable: false),
                        VarId = c.Int(nullable: false),
                        Created_at = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductCarts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Qty = c.Int(nullable: false),
                        VarId = c.Int(nullable: false),
                        Variation = c.String(),
                        CartId = c.String(),
                        ProductId = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Cart_Id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserCarts", t => t.Cart_Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .Index(t => t.Cart_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProdOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CoopId = c.Int(nullable: false),
                        UOrderId = c.String(),
                        ProdName = c.String(),
                        Variation = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MemberDiscountedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DiscountedPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProdId = c.String(),
                        Qty = c.Int(nullable: false),
                        SubTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductVariations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        ProdId = c.Int(nullable: false),
                        Created_at = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ReturnRefundItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReturnId = c.Int(nullable: false),
                        ProdOrderId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReturnRefunds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserOrderId = c.Int(nullable: false),
                        UserId = c.String(),
                        CoopId = c.Int(nullable: false),
                        Type = c.String(),
                        Status = c.String(),
                        Reason = c.String(),
                        RefundAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateRefunded = c.DateTime(),
                        IsAccepted = c.Boolean(nullable: false),
                        DateAccepted = c.DateTime(),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Desc = c.String(),
                        Rate = c.Int(nullable: false),
                        ProdId = c.Int(nullable: false),
                        ProdOrderId = c.Int(nullable: false),
                        UserId = c.String(),
                        Created_at = c.String(),
                        IsAnonymous = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.CustomerDetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Firstname = c.String(nullable: false),
                        Lastname = c.String(nullable: false),
                        Image = c.String(),
                        Contact = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Bdate = c.String(nullable: false),
                        Gender = c.String(nullable: false),
                        Role = c.String(nullable: false),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        AccountId = c.String(),
                        CoopId = c.String(),
                        CoopAdminId = c.String(),
                        IsActive = c.String(),
                        MemberLock = c.String(),
                        DateAccLocked = c.String(),
                        DateAccRetrieved = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.EWallets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        COOP_ID = c.String(),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.String(),
                        Created_At = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CoopId = c.Int(nullable: false),
                        OrderCreated_at = c.String(),
                        OStatus = c.String(),
                        ModeOfPay = c.String(),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CommissionFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Delivery_fee = c.Double(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VoucherDetailsModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VoucherCode = c.String(),
                        Name = c.String(nullable: false),
                        Percent_Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Min_spend = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateStart = c.String(),
                        ExpiryDate = c.String(),
                        Created_at = c.DateTime(nullable: false),
                        Updated_at = c.DateTime(nullable: false),
                        DiscountType = c.String(),
                        UserType = c.String(),
                        CoopAdminId = c.String(),
                        CoopId = c.Int(nullable: false),
                        CustomerId = c.String(maxLength: 128),
                        Coop_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Coop_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CustomerId)
                .Index(t => t.CustomerId)
                .Index(t => t.Coop_Id);
            
            CreateTable(
                "dbo.UserVoucherUseds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        CoopId = c.Int(nullable: false),
                        UserOrderId = c.String(),
                        VoucherCode = c.String(),
                        Status = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WishLists",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.String(),
                        Favorite = c.Boolean(nullable: false),
                        UserId = c.String(maxLength: 128),
                        Created_at = c.DateTime(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductDetailsModels", t => t.Product_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.WithdrawRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoopId = c.Int(nullable: false),
                        Fullname = c.String(),
                        Contact = c.String(),
                        Method = c.String(),
                        Email = c.String(),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ChargeFee = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Receipt = c.String(),
                        RequestStatus = c.String(),
                        DateReqeuested = c.String(),
                        DateFulfilled = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WishLists", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.WishLists", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.VoucherDetailsModels", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.VoucherDetailsModels", "Coop_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerDetailsModels", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProductVariations", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.ProductCarts", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.ProductCarts", "Cart_Id", "dbo.UserCarts");
            DropForeignKey("dbo.PriceTables", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.ExternalImages", "Userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.ExternalImages", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.ProdCheckouts", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.ProdCheckouts", "Checkout_Id", "dbo.UserCheckouts");
            DropForeignKey("dbo.UserCheckouts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerMemberships", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CustomerMemberships", "Coop_Id", "dbo.CoopDetailsModels");
            DropForeignKey("dbo.ProductManufacturers", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.Locations", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DriverDetailsModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DriverDetailsModels", "Coop_Id", "dbo.CoopDetailsModels");
            DropForeignKey("dbo.ProductCosts", "Product_Id", "dbo.ProductDetailsModels");
            DropForeignKey("dbo.ProductDetailsModels", "CustomerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductDetailsModels", "Coop_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ProductDetailsModels", "Category_Id1", "dbo.CategoryDetailsModels");
            DropForeignKey("dbo.CoopApplicationDocus", "CoopRecord_Id", "dbo.CoopApplicationRecords");
            DropForeignKey("dbo.CoopLocations", "Coop_Id", "dbo.CoopDetailsModels");
            DropForeignKey("dbo.CoopDocumentImages", "Userid", "dbo.AspNetUsers");
            DropForeignKey("dbo.CoopAdminDetailsModels", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CoopAdminDetailsModels", "Coop_Id", "dbo.CoopDetailsModels");
            DropForeignKey("dbo.UserCarts", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.WishLists", new[] { "Product_Id" });
            DropIndex("dbo.WishLists", new[] { "UserId" });
            DropIndex("dbo.VoucherDetailsModels", new[] { "Coop_Id" });
            DropIndex("dbo.VoucherDetailsModels", new[] { "CustomerId" });
            DropIndex("dbo.CustomerDetailsModels", new[] { "User_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProductVariations", new[] { "Product_Id" });
            DropIndex("dbo.ProductCarts", new[] { "Product_Id" });
            DropIndex("dbo.ProductCarts", new[] { "Cart_Id" });
            DropIndex("dbo.PriceTables", new[] { "Product_Id" });
            DropIndex("dbo.ExternalImages", new[] { "Product_Id" });
            DropIndex("dbo.ExternalImages", new[] { "Userid" });
            DropIndex("dbo.UserCheckouts", new[] { "UserId" });
            DropIndex("dbo.ProdCheckouts", new[] { "Product_Id" });
            DropIndex("dbo.ProdCheckouts", new[] { "Checkout_Id" });
            DropIndex("dbo.CustomerMemberships", new[] { "Coop_Id" });
            DropIndex("dbo.CustomerMemberships", new[] { "UserId" });
            DropIndex("dbo.ProductManufacturers", new[] { "Product_Id" });
            DropIndex("dbo.Locations", new[] { "UserId" });
            DropIndex("dbo.DriverDetailsModels", new[] { "Coop_Id" });
            DropIndex("dbo.DriverDetailsModels", new[] { "UserId" });
            DropIndex("dbo.ProductDetailsModels", new[] { "Coop_Id" });
            DropIndex("dbo.ProductDetailsModels", new[] { "Category_Id1" });
            DropIndex("dbo.ProductDetailsModels", new[] { "CustomerId" });
            DropIndex("dbo.ProductCosts", new[] { "Product_Id" });
            DropIndex("dbo.CoopApplicationDocus", new[] { "CoopRecord_Id" });
            DropIndex("dbo.CoopLocations", new[] { "Coop_Id" });
            DropIndex("dbo.CoopDocumentImages", new[] { "Userid" });
            DropIndex("dbo.CoopAdminDetailsModels", new[] { "Coop_Id" });
            DropIndex("dbo.CoopAdminDetailsModels", new[] { "UserId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.UserCarts", new[] { "UserId" });
            DropTable("dbo.WithdrawRequests");
            DropTable("dbo.WishLists");
            DropTable("dbo.UserVoucherUseds");
            DropTable("dbo.VoucherDetailsModels");
            DropTable("dbo.UserOrders");
            DropTable("dbo.EWallets");
            DropTable("dbo.CustomerDetailsModels");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Reviews");
            DropTable("dbo.ReturnRefunds");
            DropTable("dbo.ReturnRefundItems");
            DropTable("dbo.ProductVariations");
            DropTable("dbo.ProdOrders");
            DropTable("dbo.ProductCarts");
            DropTable("dbo.PriceTables");
            DropTable("dbo.ExternalImages");
            DropTable("dbo.UserCheckouts");
            DropTable("dbo.ProdCheckouts");
            DropTable("dbo.NotificationModels");
            DropTable("dbo.CustomerMemberships");
            DropTable("dbo.COOPMembershipFees");
            DropTable("dbo.ProductManufacturers");
            DropTable("dbo.UserLogs");
            DropTable("dbo.Locations");
            DropTable("dbo.EWalletHistories");
            DropTable("dbo.DriverDetailsModels");
            DropTable("dbo.DiscountModels");
            DropTable("dbo.DiscountedProducts");
            DropTable("dbo.DeliveryStatus");
            DropTable("dbo.ProductDetailsModels");
            DropTable("dbo.ProductCosts");
            DropTable("dbo.CoopApplicationDocus");
            DropTable("dbo.CoopApplicationRecords");
            DropTable("dbo.CoopMemberDiscounts");
            DropTable("dbo.CoopLocations");
            DropTable("dbo.CoopDocumentImages");
            DropTable("dbo.CoopChats");
            DropTable("dbo.CoopDetailsModels");
            DropTable("dbo.CoopAdminDetailsModels");
            DropTable("dbo.Complaints");
            DropTable("dbo.CommissionSales");
            DropTable("dbo.CommissionTables");
            DropTable("dbo.ChatMessages");
            DropTable("dbo.CategoryDetailsModels");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.UserCarts");
            DropTable("dbo.OrderCancels");
            DropTable("dbo.AccountsReceiveds");
        }
    }
}
