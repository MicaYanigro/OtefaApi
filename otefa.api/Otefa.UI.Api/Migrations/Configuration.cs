using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Otefa.Domain.Model.Entities;
using Otefa.UI.Api.Models;
using System.Data.Entity.Migrations;

namespace Otefa.UI.Api.Migrations
{
    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {

        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

            ContextKey = "Otefa.UI.Api.Models.ApplicationDbContext";
        }

        protected override void Seed(ApplicationDbContext context)
        {

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            roleManager.Create(new IdentityRole { Name = Roles.Administrator.ToString() });
       
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user1 = new ApplicationUser { UserName = "test1@test.com", Email = "test1@test.com" };

            userManager.Create(user1, "Test123!");
            userManager.AddToRoles(user1.Id, Roles.Administrator.ToString());


            context.SaveChanges();
        }

    }
}