using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DeviceStore.WebUI.Models
{
    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            var role1 = new IdentityRole { Name = "Admin" };

            roleManager.Create(role1);

            var admin = new ApplicationUser
            {
                Email = ConfigurationManager.AppSettings["adminEmail"],
                UserName = ConfigurationManager.AppSettings["adminEmail"],
            };
            string password = ConfigurationManager.AppSettings["adminPassword"];
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, role1.Name);
            }

            base.Seed(context);
        }
    }
}