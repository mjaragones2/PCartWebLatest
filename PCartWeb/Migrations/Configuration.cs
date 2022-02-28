
namespace PCartWeb.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using PCartWeb.Models;
    using Microsoft.SqlServer.Types;
    using System.Data.Entity.SqlServer;
    using Bogus;
    using System.Collections.Generic;
    using System.Data.Entity.Spatial;

    internal sealed class Configuration : DbMigrationsConfiguration<PCartWeb.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "PCartWeb.Models.ApplicationDbContext";
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
            SqlProviderServices.SqlServerTypesAssemblyName = typeof(SqlGeography).Assembly.FullName;
        }

        protected override void Seed(PCartWeb.Models.ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (!roleManager.RoleExists("Admin"))
            {

                // first we create Admin rool    
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "PCartTeam@gmail.com";
                user.Email = "PCartTeam@gmail.com";
                user.EmailConfirmed = true;

                string userPWD = "PcartTeam@2021";

                var chkUser = UserManager.Create(user, userPWD);

                //Here we create the super admin ewallet
                var adminEwallet = new EWallet();
                adminEwallet.UserID = user.Id;
                adminEwallet.Balance = 0;
                adminEwallet.Status = "Active";
                adminEwallet.Created_At = DateTime.Now;
                context.UserEWallet.Add(adminEwallet);
                context.SaveChanges();

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Coop Admin role     
            if (!roleManager.RoleExists("Coop Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Coop Admin";
                roleManager.Create(role);
            }

            // creating Creating Non-member role     
            if (!roleManager.RoleExists("Non-member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Non-member";
                roleManager.Create(role);
            }

            // creating Creating Member role     
            if (!roleManager.RoleExists("Member"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Member";
                roleManager.Create(role);
            }

            // creating Creating Member role     
            if (!roleManager.RoleExists("Driver"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Driver";
                roleManager.Create(role);
            }
            int counter = 2;
            int countercoop = 1;

            //----------------------- Adding Default Commission Rate ---------------
            var checkrate = context.CommissionDetails.ToList();
            if (checkrate.Count == 0)
            {
                var comrate = new CommissionTable
                {
                    Rate = 2,
                    Updated_at = DateTime.Now
                };
                context.CommissionDetails.Add(comrate);
                context.SaveChanges();
            }

            //----------------------- Adding Category ------------------------------

            int cat_id = 1;
            List<CategoryDetailsModel> categoryDetails = GetCategoryDetails();

            var checkcateg = context.CategoryDetails.ToList();
            if (checkcateg.Count == 0)
            {
                for (int cat = 0; cat < 2; cat++)
                {
                    var getcat = categoryDetails.Where(x => x.Id == cat_id).FirstOrDefault();
                    var addcat = new CategoryDetailsModel();
                    addcat.Description = getcat.Description;
                    addcat.Created_at = getcat.Created_at;
                    addcat.Name = getcat.Name;
                    addcat.Updated_at = getcat.Updated_at;
                    context.CategoryDetails.Add(addcat);
                    context.SaveChanges();
                    cat_id++;
                }
                cat_id = 1;
            }

            //---------------------------- Adding 3 Coops --------------------------------
            var cooplist = context.CoopDetails.ToList();
            if (cooplist.Count == 0)
            {
                for (int k = 0; k < 3; k++)
                {
                    var faker = new Faker("en_US");
                    var pass = "PCartpass@2021";
                    var seekerrole = roleManager.FindByName("Coop Admin");
                    if (seekerrole == null)
                    {
                        seekerrole = new IdentityRole("Coop Admin");
                        var roleresult = roleManager.Create(seekerrole);
                    }
                    if (countercoop > 4)
                    {
                        countercoop = 1;
                    }
                    if (counter > 12)
                    {
                        counter = 1;
                    }
                    var addseeker = new Faker<ApplicationUser>()
                        .RuleFor(x => x.UserName, x => x.Person.Email)
                        .RuleFor(x => x.Email, x => x.Person.Email)
                        .RuleFor(x => x.EmailConfirmed, true);

                    var valuesaddseeker = addseeker.Generate();
                    var findseek = UserManager.FindByName(valuesaddseeker.UserName);
                    if (findseek == null)
                    {
                        var res = UserManager.Create(valuesaddseeker, pass);
                        if (res.Succeeded)
                        {
                            List<RandomCompName> coopname = GetRandomCoopName();
                            List<RandomLocation> location = GetRandomLocation();
                            var getcoop = coopname.Where(x => x.Id == countercoop).FirstOrDefault();
                            var getexactloc = location.Where(x => x.Id == counter).FirstOrDefault();
                            var coopidentity = new Faker<CoopDetailsModel>()
                                .RuleFor(x => x.Address, getexactloc.Address)
                                .RuleFor(x => x.Approval, "Approved")
                                .RuleFor(x => x.Contact, x => x.Person.Phone)
                                .RuleFor(x => x.CoopName, getcoop.CoopName)
                                .RuleFor(x => x.Coop_Created, DateTime.Now)
                                .RuleFor(x => x.Coop_Updated, DateTime.Now)
                                .RuleFor(x => x.IsLocked, "Unlock")
                                .RuleFor(x => x.MembershipForm, "sample.pdf");

                            var coopidentity_generate = coopidentity.Generate();
                            context.CoopDetails.Add(coopidentity_generate);
                            context.SaveChanges();

                            var cooplocationidentity = new Faker<CoopLocation>()
                                .RuleFor(x => x.Address, x => x.Person.Address.City)
                                .RuleFor(x => x.CoopId, coopidentity_generate.Id.ToString())
                                .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + x.Person.Address.Geo.Lng + " " + x.Person.Address.Geo.Lat + ")"))
                                .RuleFor(x => x.Created_at, DateTime.Now);
                            var cooplocationidentity_generate = cooplocationidentity.Generate();
                            context.CoopLocations.Add(cooplocationidentity_generate);
                            context.SaveChanges();

                            var coopadminlocation = new Faker<Location>()
                                .RuleFor(x => x.Address, x => x.Person.Address.City)
                                .RuleFor(x => x.Created_at, DateTime.Now)
                                .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + x.Person.Address.Geo.Lng + " " + x.Person.Address.Geo.Lat + ")"))
                                .RuleFor(x => x.UserId, valuesaddseeker.Id);

                            var coopadminlocation_generate = coopadminlocation.Generate();
                            context.Locations.Add(coopadminlocation_generate);
                            context.SaveChanges();

                            var coopdetailidentity = new Faker<CoopAdminDetailsModel>()
                                .RuleFor(x => x.Address, x => x.Person.Address.City)
                                .RuleFor(x => x.Approval, "Approved")
                                .RuleFor(x => x.Bdate, x => x.Person.DateOfBirth.ToString())
                                .RuleFor(x => x.Contact, x => x.Person.Phone)
                                .RuleFor(x => x.Coop_code, coopidentity_generate.Id)
                                .RuleFor(x => x.Created_at, DateTime.Now)
                                .RuleFor(x => x.Email, valuesaddseeker.Email)
                                .RuleFor(x => x.Firstname, x => x.Person.FirstName)
                                .RuleFor(x => x.Gender, x => x.Person.Gender.ToString())
                                .RuleFor(x => x.Image, "defaultprofile.jpg")
                                .RuleFor(x => x.Lastname, x => x.Person.LastName)
                                .RuleFor(x => x.Status, "Single")
                                .RuleFor(x => x.Updated_at, DateTime.Now)
                                .RuleFor(x => x.UserId, valuesaddseeker.Id);
                            var coopdetailidentity_generate = coopdetailidentity.Generate();
                            context.CoopAdminDetails.Add(coopdetailidentity_generate);
                            context.SaveChanges();

                            var ewallet = new EWallet();
                            ewallet.UserID = valuesaddseeker.Id;
                            ewallet.COOP_ID = coopidentity_generate.Id.ToString();
                            ewallet.Balance = 0;
                            ewallet.Status = "Active";
                            ewallet.Created_At = DateTime.Now;
                            context.UserEWallet.Add(ewallet);
                            context.SaveChanges();

                            res = UserManager.SetLockoutEnabled(valuesaddseeker.Id, true);
                            var seekerroles = UserManager.GetRoles(valuesaddseeker.Id);
                            if (!seekerroles.Contains(seekerrole.Id))
                            {
                                var result = UserManager.AddToRole(valuesaddseeker.Id, "Coop Admin");
                            }




                            //-------------------- Adding 50 Products per COOP ------------------------

                            for (int prod = 0; prod < 50; prod++)
                            {
                                if (cat_id > 2)
                                {
                                    cat_id = 1;
                                }
                                Random rnd = new Random();
                                int qty = rnd.Next(50, 100);
                                var checkcategory = checkcateg.Where(x => x.Id == cat_id).FirstOrDefault();
                                var prodidentity = new Faker<ProductDetailsModel>()
                                    .RuleFor(x => x.Categoryname, checkcategory.Name)
                                    .RuleFor(x => x.Category_Id, checkcategory.Id)
                                    .RuleFor(x => x.CoopId, coopidentity_generate.Id)
                                    .RuleFor(x => x.Date_ApprovalStatus, DateTime.Now)
                                    .RuleFor(x => x.Product_desc, x => x.Commerce.ProductDescription())
                                    .RuleFor(x => x.Product_image, "products.png")
                                    .RuleFor(x => x.Product_Name, x => x.Commerce.ProductName())
                                    .RuleFor(x => x.Product_qty, qty)
                                    .RuleFor(x => x.Product_status, "Approved")
                                    .RuleFor(x => x.CoopAdminId, valuesaddseeker.Id)
                                    .RuleFor(x => x.Prod_Created_at, DateTime.Now)
                                    .RuleFor(x => x.Prod_Updated_at, DateTime.Now);
                                var prodidentity_generate = prodidentity.Generate();
                                var checkprod = context.ProductDetails.Where(x => x.Id == prodidentity_generate.Id).FirstOrDefault();
                                if (checkprod == null)
                                {
                                    context.ProductDetails.Add(prodidentity_generate);
                                    context.SaveChanges();

                                    var priceidentity = new Faker<PriceTable>()
                                        .RuleFor(x => x.Created_at, DateTime.Now.ToString())
                                        .RuleFor(x => x.Price, x => decimal.Parse(x.Commerce.Price()))
                                        .RuleFor(x => x.ProdId, prodidentity_generate.Id);
                                    var priceidentity_generate = priceidentity.Generate();
                                    context.Prices.Add(priceidentity_generate);
                                    context.SaveChanges();

                                    var costidentity = new ProductCost();
                                    costidentity.Cost = priceidentity_generate.Price - 20;
                                    costidentity.Created_at = DateTime.Now.ToString();
                                    costidentity.ProdId = prodidentity_generate.Id;
                                    context.Cost.Add(costidentity);
                                    context.SaveChanges();

                                    var manufactureridentity = new Faker<ProductManufacturer>()
                                        .RuleFor(x => x.Created_at, DateTime.Now.ToString())
                                        .RuleFor(x => x.Manufacturer, x => x.Company.CompanyName())
                                        .RuleFor(x => x.ProdId, prodidentity_generate.Id);
                                    var manufact_generate = manufactureridentity.Generate();
                                    context.Manufacturer.Add(manufact_generate);
                                    context.SaveChanges();
                                    cat_id++;
                                }
                            }

                            //---------------------- Adding 10 riders per COOP-------------------------

                            for (int rider = 0; rider < 10; rider++)
                            {
                                var driverrole = roleManager.FindByName("Driver");
                                var addrideridentity = new Faker<ApplicationUser>()
                                    .RuleFor(x => x.Email, x => x.Person.Email)
                                    .RuleFor(x => x.UserName, x => x.Person.Email)
                                    .RuleFor(x => x.EmailConfirmed, true);
                                var addrider_generate = addrideridentity.Generate();
                                var findrider = UserManager.FindByName(addrider_generate.UserName);
                                if (findrider == null)
                                {
                                    var createrider = UserManager.Create(addrider_generate, pass);

                                    if (createrider.Succeeded)
                                    {
                                        var riderdetail_identity = new Faker<DriverDetailsModel>()
                                        .RuleFor(x => x.Address, x => x.Person.Address.City)
                                        .RuleFor(x => x.Bdate, x => x.Person.DateOfBirth.ToString())
                                        .RuleFor(x => x.Contact, x => x.Person.Phone)
                                        .RuleFor(x => x.CoopId, coopidentity_generate.Id.ToString())
                                        .RuleFor(x => x.Created_at, DateTime.Now)
                                        .RuleFor(x => x.CStatus, "Single")
                                        .RuleFor(x => x.Driver_License, "deliverybox.png")
                                        .RuleFor(x => x.Firstname, x => x.Person.FirstName)
                                        .RuleFor(x => x.Gender, x => x.Person.Gender.ToString())
                                        .RuleFor(x => x.Image, "profile.jpeg")
                                        .RuleFor(x => x.IsActive, "Active")
                                        .RuleFor(x => x.IsAvailable, true)
                                        .RuleFor(x => x.IsOnDuty, true)
                                        .RuleFor(x => x.Lastname, x => x.Person.LastName)
                                        .RuleFor(x => x.PlateNum, "AZW-09HZ")
                                        .RuleFor(x => x.Updated_at, DateTime.Now)
                                        .RuleFor(x => x.UserId, addrider_generate.Id);

                                        var riderdetail_generate = riderdetail_identity.Generate();
                                        context.DriverDetails.Add(riderdetail_generate);
                                        context.SaveChanges();
                                        createrider = UserManager.SetLockoutEnabled(addrider_generate.Id, true);

                                        var riderlocation = new Faker<Location>()
                                            .RuleFor(x => x.Address, riderdetail_generate.Address)
                                            .RuleFor(x => x.Created_at, DateTime.Now)
                                            .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + x.Person.Address.Geo.Lng + " " + x.Person.Address.Geo.Lat + ")"))
                                            .RuleFor(x => x.UserId, addrider_generate.Id);
                                        var riderloc_generate = riderlocation.Generate();
                                        context.Locations.Add(riderloc_generate);
                                        context.SaveChanges();

                                        var checkriderrole = UserManager.GetRoles(addrider_generate.Id);
                                        if (!checkriderrole.Contains(driverrole.Id))
                                        {
                                            var addinguserrole = UserManager.AddToRole(addrider_generate.Id, "Driver");
                                        }
                                    }


                                }
                            }

                            countercoop++;
                            counter++;
                        }


                    }


                }
            }
            else
            {
                for (int k = 0; k < 2; k++)
                {
                    var faker = new Faker("en_US");
                    var pass = "PCartpass@2021";
                    var seekerrole = roleManager.FindByName("Coop Admin");
                    if (seekerrole == null)
                    {
                        seekerrole = new IdentityRole("Coop Admin");
                        var roleresult = roleManager.Create(seekerrole);
                    }
                    if (countercoop > 4)
                    {
                        countercoop = 1;
                    }
                    if (counter > 12)
                    {
                        counter = 1;
                    }
                    var addseeker = new Faker<ApplicationUser>()
                        .RuleFor(x => x.UserName, x => x.Person.Email)
                        .RuleFor(x => x.Email, x => x.Person.Email)
                        .RuleFor(x => x.EmailConfirmed, true);

                    var valuesaddseeker = addseeker.Generate();
                    var findseek = UserManager.FindByName(valuesaddseeker.UserName);
                    if (findseek == null)
                    {
                        var res = UserManager.Create(valuesaddseeker, pass);
                        if (res.Succeeded)
                        {
                            List<RandomCompName> coopname = GetRandomCoopName();
                            List<RandomLocation> location = GetRandomLocation();
                            var getcoop = coopname.Where(x => x.Id == countercoop).FirstOrDefault();
                            var getexactloc = location.Where(x => x.Id == counter).FirstOrDefault();
                            var coopidentity = new Faker<CoopDetailsModel>()
                                .RuleFor(x => x.Address, getexactloc.Address)
                                .RuleFor(x => x.Approval, "Approved")
                                .RuleFor(x => x.Contact, x => x.Person.Phone)
                                .RuleFor(x => x.CoopName, getcoop.CoopName)
                                .RuleFor(x => x.Coop_Created, DateTime.Now)
                                .RuleFor(x => x.Coop_Updated, DateTime.Now)
                                .RuleFor(x => x.IsLocked, "Unlock")
                                .RuleFor(x => x.MembershipForm, "sample.pdf");

                            var coopidentity_generate = coopidentity.Generate();
                            context.CoopDetails.Add(coopidentity_generate);
                            context.SaveChanges();

                            var cooplocationidentity = new Faker<CoopLocation>()
                                .RuleFor(x => x.Address, x => x.Person.Address.City)
                                .RuleFor(x => x.CoopId, coopidentity_generate.Id.ToString())
                                .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + x.Person.Address.Geo.Lng + " " + x.Person.Address.Geo.Lat + ")"))
                                .RuleFor(x => x.Created_at, DateTime.Now);
                            var cooplocationidentity_generate = cooplocationidentity.Generate();
                            context.CoopLocations.Add(cooplocationidentity_generate);
                            context.SaveChanges();

                            var coopadminlocation = new Faker<Location>()
                                .RuleFor(x => x.Address, x => x.Person.Address.City)
                                .RuleFor(x => x.Created_at, DateTime.Now)
                                .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + x.Person.Address.Geo.Lng + " " + x.Person.Address.Geo.Lat + ")"))
                                .RuleFor(x => x.UserId, valuesaddseeker.Id);

                            var coopadminlocation_generate = coopadminlocation.Generate();
                            context.Locations.Add(coopadminlocation_generate);
                            context.SaveChanges();

                            var coopdetailidentity = new Faker<CoopAdminDetailsModel>()
                                .RuleFor(x => x.Address, x => x.Person.Address.City)
                                .RuleFor(x => x.Approval, "Approved")
                                .RuleFor(x => x.Bdate, x => x.Person.DateOfBirth.ToString())
                                .RuleFor(x => x.Contact, x => x.Person.Phone)
                                .RuleFor(x => x.Coop_code, coopidentity_generate.Id)
                                .RuleFor(x => x.Created_at, DateTime.Now)
                                .RuleFor(x => x.Email, valuesaddseeker.Email)
                                .RuleFor(x => x.Firstname, x => x.Person.FirstName)
                                .RuleFor(x => x.Gender, x => x.Person.Gender.ToString())
                                .RuleFor(x => x.Image, "defaultprofile.jpg")
                                .RuleFor(x => x.Lastname, x => x.Person.LastName)
                                .RuleFor(x => x.Status, "Single")
                                .RuleFor(x => x.Updated_at, DateTime.Now)
                                .RuleFor(x => x.UserId, valuesaddseeker.Id);
                            var coopdetailidentity_generate = coopdetailidentity.Generate();
                            context.CoopAdminDetails.Add(coopdetailidentity_generate);
                            context.SaveChanges();

                            var ewallet = new EWallet();
                            ewallet.UserID = valuesaddseeker.Id;
                            ewallet.COOP_ID = coopidentity_generate.Id.ToString();
                            ewallet.Balance = 0;
                            ewallet.Status = "Active";
                            ewallet.Created_At = DateTime.Now;
                            context.UserEWallet.Add(ewallet);
                            context.SaveChanges();

                            res = UserManager.SetLockoutEnabled(valuesaddseeker.Id, true);
                            var seekerroles = UserManager.GetRoles(valuesaddseeker.Id);
                            if (!seekerroles.Contains(seekerrole.Id))
                            {
                                var result = UserManager.AddToRole(valuesaddseeker.Id, "Coop Admin");
                            }




                            //-------------------- Adding 50 Products per COOP ------------------------

                            for (int prod = 0; prod < 50; prod++)
                            {
                                if (cat_id > 2)
                                {
                                    cat_id = 1;
                                }
                                Random rnd = new Random();
                                int qty = rnd.Next(50, 100);
                                var checkcategory = checkcateg.Where(x => x.Id == cat_id).FirstOrDefault();
                                var prodidentity = new Faker<ProductDetailsModel>()
                                    .RuleFor(x => x.Categoryname, checkcategory.Name)
                                    .RuleFor(x => x.Category_Id, checkcategory.Id)
                                    .RuleFor(x => x.CoopId, coopidentity_generate.Id)
                                    .RuleFor(x => x.Date_ApprovalStatus, DateTime.Now)
                                    .RuleFor(x => x.Product_desc, x => x.Commerce.ProductDescription())
                                    .RuleFor(x => x.Product_image, "products.png")
                                    .RuleFor(x => x.Product_Name, x => x.Commerce.ProductName())
                                    .RuleFor(x => x.Product_qty, qty)
                                    .RuleFor(x => x.Product_status, "Approved")
                                    .RuleFor(x => x.CoopAdminId, valuesaddseeker.Id)
                                    .RuleFor(x => x.Prod_Created_at, DateTime.Now)
                                    .RuleFor(x => x.Prod_Updated_at, DateTime.Now);
                                var prodidentity_generate = prodidentity.Generate();
                                var checkprod = context.ProductDetails.Where(x => x.Id == prodidentity_generate.Id).FirstOrDefault();
                                if (checkprod == null)
                                {
                                    context.ProductDetails.Add(prodidentity_generate);
                                    context.SaveChanges();

                                    var priceidentity = new Faker<PriceTable>()
                                        .RuleFor(x => x.Created_at, DateTime.Now.ToString())
                                        .RuleFor(x => x.Price, x => decimal.Parse(x.Commerce.Price()))
                                        .RuleFor(x => x.ProdId, prodidentity_generate.Id);
                                    var priceidentity_generate = priceidentity.Generate();
                                    context.Prices.Add(priceidentity_generate);
                                    context.SaveChanges();

                                    var costidentity = new ProductCost();
                                    costidentity.Cost = priceidentity_generate.Price - 20;
                                    costidentity.Created_at = DateTime.Now.ToString();
                                    costidentity.ProdId = prodidentity_generate.Id;
                                    context.Cost.Add(costidentity);
                                    context.SaveChanges();

                                    var manufactureridentity = new Faker<ProductManufacturer>()
                                        .RuleFor(x => x.Created_at, DateTime.Now.ToString())
                                        .RuleFor(x => x.Manufacturer, x => x.Company.CompanyName())
                                        .RuleFor(x => x.ProdId, prodidentity_generate.Id);
                                    var manufact_generate = manufactureridentity.Generate();
                                    context.Manufacturer.Add(manufact_generate);
                                    context.SaveChanges();
                                    cat_id++;
                                }
                            }

                            //---------------------- Adding 10 riders per COOP-------------------------

                            for (int rider = 0; rider < 10; rider++)
                            {
                                var driverrole = roleManager.FindByName("Driver");
                                var addrideridentity = new Faker<ApplicationUser>()
                                    .RuleFor(x => x.Email, x => x.Person.Email)
                                    .RuleFor(x => x.UserName, x => x.Person.Email)
                                    .RuleFor(x => x.EmailConfirmed, true);
                                var addrider_generate = addrideridentity.Generate();
                                var findrider = UserManager.FindByName(addrider_generate.UserName);
                                if (findrider == null)
                                {
                                    var createrider = UserManager.Create(addrider_generate, pass);

                                    if (createrider.Succeeded)
                                    {
                                        var riderdetail_identity = new Faker<DriverDetailsModel>()
                                        .RuleFor(x => x.Address, x => x.Person.Address.City)
                                        .RuleFor(x => x.Bdate, x => x.Person.DateOfBirth.ToString())
                                        .RuleFor(x => x.Contact, x => x.Person.Phone)
                                        .RuleFor(x => x.CoopId, coopidentity_generate.Id.ToString())
                                        .RuleFor(x => x.Created_at, DateTime.Now)
                                        .RuleFor(x => x.CStatus, "Single")
                                        .RuleFor(x => x.Driver_License, "deliverybox.png")
                                        .RuleFor(x => x.Firstname, x => x.Person.FirstName)
                                        .RuleFor(x => x.Gender, x => x.Person.Gender.ToString())
                                        .RuleFor(x => x.Image, "profile.jpeg")
                                        .RuleFor(x => x.IsActive, "Active")
                                        .RuleFor(x => x.IsAvailable, true)
                                        .RuleFor(x => x.IsOnDuty, true)
                                        .RuleFor(x => x.Lastname, x => x.Person.LastName)
                                        .RuleFor(x => x.PlateNum, "AZW-09HZ")
                                        .RuleFor(x => x.Updated_at, DateTime.Now)
                                        .RuleFor(x => x.UserId, addrider_generate.Id);

                                        var riderdetail_generate = riderdetail_identity.Generate();
                                        context.DriverDetails.Add(riderdetail_generate);
                                        context.SaveChanges();
                                        createrider = UserManager.SetLockoutEnabled(addrider_generate.Id, true);

                                        var riderlocation = new Faker<Location>()
                                            .RuleFor(x => x.Address, riderdetail_generate.Address)
                                            .RuleFor(x => x.Created_at, DateTime.Now)
                                            .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + x.Person.Address.Geo.Lng + " " + x.Person.Address.Geo.Lat + ")"))
                                            .RuleFor(x => x.UserId, addrider_generate.Id);
                                        var riderloc_generate = riderlocation.Generate();
                                        context.Locations.Add(riderloc_generate);
                                        context.SaveChanges();

                                        var checkriderrole = UserManager.GetRoles(addrider_generate.Id);
                                        if (!checkriderrole.Contains(driverrole.Id))
                                        {
                                            var addinguserrole = UserManager.AddToRole(addrider_generate.Id, "Driver");
                                        }
                                    }


                                }
                            }

                            countercoop++;
                            counter++;
                        }


                    }


                }
            }
            counter = 1;
            //---------------------------- Adding 500 Customer (Members) ----------------------
            var customer = context.UserDetails.ToList();
            if (customer.Count == 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    var faker = new Faker("en_US");
                    var pass = "PCartpass@2021";
                    var seekrole = roleManager.FindByName("Member");
                    var addseeker = new Faker<ApplicationUser>()
                        .RuleFor(x => x.UserName, x => x.Person.Email)
                        .RuleFor(x => x.Email, x => x.Person.Email)
                        .RuleFor(x => x.EmailConfirmed, true);
                    var valuesaddseeker = addseeker.Generate();
                    var findseek = UserManager.FindByName(valuesaddseeker.UserName);
                    if (findseek == null)
                    {
                        Random rand = new Random();
                        int coopid_use = rand.Next(1, 3);
                        var usManager = UserManager.Create(valuesaddseeker, pass);

                        if (usManager.Succeeded)
                        {
                            var seekeridentity = new Faker<CustomerDetailsModel>()
                            .RuleFor(x => x.Firstname, x => x.Person.FirstName)
                            .RuleFor(x => x.Lastname, x => x.Person.LastName)
                            .RuleFor(x => x.Created_at, DateTime.Now)
                            .RuleFor(x => x.Updated_at, DateTime.Now)
                            .RuleFor(x => x.Image, "profile.jpeg")
                            .RuleFor(x => x.Contact, x => x.Person.Phone)
                            .RuleFor(x => x.CoopId, coopid_use.ToString())
                            .RuleFor(x => x.IsActive, "Active")
                            .RuleFor(x => x.Bdate, x => x.Person.DateOfBirth.ToString())
                            .RuleFor(x => x.Address, x => x.Person.Address.City)
                            .RuleFor(x => x.Gender, x => x.Person.Gender.ToString())
                            .RuleFor(x => x.AccountId, valuesaddseeker.Id)
                            .RuleFor(x => x.Role, "Member")
                            .RuleFor(x => x.MemberLock, "Active");
                            var seekeridentity_generate = seekeridentity.Generate();
                            context.UserDetails.Add(seekeridentity_generate);
                            context.SaveChanges();

                            if (counter > 12)
                            {
                                counter = 1;
                            }

                            var cart = new UserCart { UserId = valuesaddseeker.Id };
                            context.Cart.Add(cart);
                            context.SaveChanges();
                            List<RandomLocation> loclist = GetRandomLocation();
                            var result = loclist.Where(x => x.Id == counter).FirstOrDefault();
                            var locationidentity = new Faker<Location>()
                                .RuleFor(x => x.Address, result.Address)
                                .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + result.Longitude + " " + result.Latitude + ")"))
                                .RuleFor(x => x.Created_at, DateTime.Now)
                                .RuleFor(x => x.UserId, valuesaddseeker.Id);
                            var locationidentity_generate = locationidentity.Generate();
                            context.Locations.Add(locationidentity_generate);
                            context.SaveChanges();
                            counter++;
                            var ewalletseeker = new EWallet { Balance = 0, COOP_ID = coopid_use.ToString(), Created_At = DateTime.Now, Status = "Active", UserID = valuesaddseeker.Id };
                            context.UserEWallet.Add(ewalletseeker);
                            context.SaveChanges();
                            usManager = UserManager.SetLockoutEnabled(valuesaddseeker.Id, true);

                            var seekerroles = UserManager.GetRoles(valuesaddseeker.Id);
                            if (!seekerroles.Contains(seekrole.Id))
                            {
                                var roleres = UserManager.AddToRole(valuesaddseeker.Id, "Member");
                            }
                            coopid_use++;
                        }


                    }

                }

                //-------------------- Adding 500 Customers (Non-Members) ------------------

                for (int j = 0; j < 10; j++)
                {
                    var faker = new Faker("en_US");
                    var pass = "PCartpass@2021";
                    var seekrole = roleManager.FindByName("Non-member");
                    var addseeker = new Faker<ApplicationUser>()
                        .RuleFor(x => x.UserName, x => x.Person.Email)
                        .RuleFor(x => x.Email, x => x.Person.Email)
                        .RuleFor(x => x.EmailConfirmed, true);
                    var valuesaddseeker = addseeker.Generate();
                    var findseek = UserManager.FindByName(valuesaddseeker.UserName);
                    if (findseek == null)
                    {
                        Random rand = new Random();
                        int coopid_use = rand.Next(1, 3);
                        var usManager = UserManager.Create(valuesaddseeker, pass);



                        if (usManager.Succeeded)
                        {
                            var seekeridentity = new Faker<CustomerDetailsModel>()
                            .RuleFor(x => x.Firstname, x => x.Person.FirstName)
                            .RuleFor(x => x.Lastname, x => x.Person.LastName)
                            .RuleFor(x => x.Created_at, DateTime.Now)
                            .RuleFor(x => x.Updated_at, DateTime.Now)
                            .RuleFor(x => x.Image, "profile.jpeg")
                            .RuleFor(x => x.Contact, x => x.Person.Phone)
                            .RuleFor(x => x.IsActive, "Active")
                            .RuleFor(x => x.Bdate, x => x.Person.DateOfBirth.ToString())
                            .RuleFor(x => x.Address, x => x.Person.Address.City)
                            .RuleFor(x => x.Gender, x => x.Person.Gender.ToString())
                            .RuleFor(x => x.AccountId, valuesaddseeker.Id)
                            .RuleFor(x => x.Role, "Non-Member")
                            .RuleFor(x => x.MemberLock, "Active");
                            var seekeridentity_generate = seekeridentity.Generate();
                            context.UserDetails.Add(seekeridentity_generate);
                            context.SaveChanges();

                            var cart = new UserCart { UserId = valuesaddseeker.Id };
                            context.Cart.Add(cart);
                            context.SaveChanges();
                            if (counter > 12)
                            {
                                counter = 1;
                            }

                            List<RandomLocation> loclist = GetRandomLocation();
                            var result = loclist.Where(x => x.Id == counter).FirstOrDefault();
                            var locationidentity = new Faker<Location>()
                                .RuleFor(x => x.Address, result.Address)
                                .RuleFor(x => x.Geolocation, x => DbGeography.FromText("POINT(" + result.Longitude + " " + result.Latitude + ")"))
                                .RuleFor(x => x.Created_at, DateTime.Now)
                                .RuleFor(x => x.UserId, valuesaddseeker.Id);
                            var locationidentity_generate = locationidentity.Generate();
                            context.Locations.Add(locationidentity_generate);
                            context.SaveChanges();
                            counter++;
                            var ewalletseeker = new EWallet { Balance = 0, COOP_ID = coopid_use.ToString(), Created_At = DateTime.Now, Status = "Active", UserID = valuesaddseeker.Id };
                            context.UserEWallet.Add(ewalletseeker);
                            context.SaveChanges();
                            usManager = UserManager.SetLockoutEnabled(valuesaddseeker.Id, true);

                            var seekerroles = UserManager.GetRoles(valuesaddseeker.Id);
                            if (!seekerroles.Contains(seekrole.Id))
                            {
                                var roleres = UserManager.AddToRole(valuesaddseeker.Id, "Non-member");
                            }
                        }
                    }
                }
            }




        }

        public List<RandomLocation> GetRandomLocation()
        {
            List<RandomLocation> RlocList = new List<RandomLocation>
            {
                 new RandomLocation {Id = 1, Address = "18 Horseshoe Drive, Cebu City, Philippines", Latitude = "10.315700", Longitude = "123.897170" },
                 new RandomLocation {Id = 2, Address = "2-14 J. Solon Drive, Cebu City 6000, Philippines", Latitude = "10.324800", Longitude = "123.900560" },
                 new RandomLocation {Id = 3, Address = "22 Macopa St., Cebu City, Philippines", Latitude = "14.682710", Longitude = "121.110250" },
                 new RandomLocation {Id = 4, Address = "29 Pelaez Extension, Cebu City, Philippines", Latitude = "10.301390", Longitude = "123.897500" },
                 new RandomLocation {Id = 5, Address = "Conequip Philippines, Inc., company, Mandaue, Philippines", Latitude = "10.323803600000002", Longitude = "123.93889095886573" },
                 new RandomLocation {Id = 6, Address = "LandBank of the Philippines, bank, Naga, Philippines", Latitude = "10.2093458", Longitude = "123.7589744" },
                 new RandomLocation {Id = 7, Address = "City Homes, neighbourhood, Lapu-Lapu, Philippines", Latitude = "10.2935883", Longitude = "123.9701831" },
                 new RandomLocation {Id = 8, Address = "Agpasan Binaliw Cebu City, residential, Cebu City, Philippines", Latitude = "10.41641185", Longitude = "123.90720949888288" },
                 new RandomLocation {Id = 9, Address = "Sergio Osmeña Jr. Avenue / Sergio Osmeña Jr. Boulevard, Cebu City, Philippines", Latitude = "10.314410", Longitude = "123.958480" },
                 new RandomLocation {Id = 10, Address = "R. Rabaya Street, Talisay, Philippines", Latitude = "10.257290", Longitude = "123.849980" },
                 new RandomLocation {Id = 11, Address = "F. Deiparine Street, Talisay, Philippines", Latitude = "10.258490", Longitude = "123.844480" },
                 new RandomLocation {Id = 12, Address = "Kapitan Deiparine Road, Talisay, Philippines", Latitude = "10.257020", Longitude = "123.845510" },
            };
            return RlocList;
        }

        public List<RandomCompName> GetRandomCoopName()
        {
            List<RandomCompName> RCooplist = new List<RandomCompName>
            {
                new RandomCompName { Id = 1, CoopName = "Lamac Multi-Purpose Coop."},
                new RandomCompName { Id = 2, CoopName = "Cebu People's Multi Purpose Coop."},
                new RandomCompName { Id = 3, CoopName = "Bayanihan Multi Purpose"}
            };
            return RCooplist;
        }

        public List<CategoryDetailsModel> GetCategoryDetails()
        {
            List<CategoryDetailsModel> categories = new List<CategoryDetailsModel>
            {
                new CategoryDetailsModel { Id = 1, Created_at = DateTime.Now, Description = "Plants", Name = "Plants", Updated_at = DateTime.Now },
                new CategoryDetailsModel { Id = 2, Created_at = DateTime.Now, Description = "Wooden Furnitures and etc.", Name = "Furnitures", Updated_at = DateTime.Now }
            };
            return categories;
        }
    }
}