using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingHours.Model.DbContext
{
    internal class AppDbInitializer : CreateDatabaseIfNotExists<AppDbContext>
    {
        protected override void Seed(AppDbContext context)
        {
            using (var roleManager = new RoleManager<ApplicationRole, int>(new RoleStore<ApplicationRole, int, ApplicationUserRole>(context)))
            {
                foreach (var role in Enum.GetValues(typeof(Roles)).Cast<Roles>())
                {
                    roleManager.Create(new ApplicationRole
                    {
                        Name = role.ToString()
                    });
                }
            }

            var varsi = new ApplicationUser
            {
                FullName = "Varsányi Márton",
                Email = "varsi.marci@gmail.com",
                UserName = "varsi.marci"
            };

            var ecsedi = new ApplicationUser
            {
                FullName = "Ecsedi Gergő",
                Email = "ecsedigergo@gmail.com",
                UserName = "ecsedigergo"
            };

            context.UserManager.Create(ecsedi, "123456");
            context.UserManager.Create(varsi, "123456");
            context.SaveChanges();

            context.UserManager.AddToRole(varsi.Id, Roles.Manager.ToString());
            context.UserManager.AddToRole(ecsedi.Id, Roles.Manager.ToString());
            context.SaveChanges();
        }
    }
}
