using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.Data.Context;
using Aufnet.Backend.Data.Models.Entities;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Aufnet.Backend.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize( ApplicationDbContext context, UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager )
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            //create users
            if (!context.Users.Any())
            {
                if (!await roleManager.RoleExistsAsync("SuperAdmin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                    await roleManager.CreateAsync(new IdentityRole("customer"));
                    await roleManager.CreateAsync(new IdentityRole("merchant"));
                    await roleManager.CreateAsync(new IdentityRole("ticket_attendant"));
                    await roleManager.CreateAsync(new IdentityRole("manager"));

                }
            }
            //Create User=admin
            //var superUser = new ApplicationUser
            //{
            //    UserName = "sa",
            //    Email = "admin@aufnet.com",
            //    EmailConfirmed = true
            //};
            //var adminresult = await userManager.CreateAsync(superUser, "Potatohair51!");
            //await userManager.AddToRoleAsync(superUser, "SuperAdmin");
        }
    }
}
