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
        public static async Task Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            //create users
            if (!context.Users.Any())
            {
                if (!await roleManager.RoleExistsAsync("SuperAdmin"))
                {
                    await roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                    await roleManager.CreateAsync(new IdentityRole("admin-hr"));
                    await roleManager.CreateAsync(new IdentityRole("admin-ops"));
                    await roleManager.CreateAsync(new IdentityRole("cit"));
                    //await roleManager.CreateAsync(new IdentityRole("General"));
                }
                //Create User=admin
                var superUser = new ApplicationUser
                {
                    UserName = "sa",
                    Email = "developers@switchlink.com.au",
                    EmailConfirmed = true
                };
                var adminresult = await userManager.CreateAsync(superUser, "Potatohair51!");
                await userManager.AddToRoleAsync(superUser, "SuperAdmin");
            }


            //if (!context.Terminals.Any())
            //{
            //    //terminal
            //    context.Terminals.Add(new Terminal
            //    {
            //        TerminalId = "T00001",
            //        Status = "OK"
            //    });
            //    context.Terminals.Add(new Terminal
            //    {
            //        TerminalId = "T00002",
            //        Status = "OK"
            //    });
            //    context.Terminals.Add(new Terminal
            //    {
            //        TerminalId = "T00003",
            //        Status = "OK"
            //    });
            //    context.Terminals.Add(new Terminal
            //    {
            //        TerminalId = "T00004",
            //        Status = "OK"
            //    });
            //}

            ////system configs
            //if (!context.Configs.Any())
            //{
            //    context.Configs.Add(new SystemConfig
            //    {
            //        Key = ConfigKeys.DailyDiscrepancyLimit,
            //        Value = "20"
            //    });
            //    context.Configs.Add(new SystemConfig
            //    {
            //        Key = ConfigKeys.EndOfShiftTradingDayStartHour,
            //        Value = "14"
            //    });
            //    context.Configs.Add(new SystemConfig
            //    {
            //        Key = ConfigKeys.EndOfShiftTradingDayEndHour,
            //        Value = "18"
            //    });
            //    context.Configs.Add(new SystemConfig
            //    {
            //        Key = ConfigKeys.EndOfDayHour,
            //        Value = "17"
            //    });
            //}

            ////import staff data
            //if (!context.Staffs.Any())
            //{
            //    //create staffs
            //    using (var csv = new CsvReader(File.OpenText(System.AppContext.BaseDirectory + "/Data/staff_database_v2.txt"), new CsvConfiguration
            //    {
            //        Delimiter = "\t"
            //    }))
            //    {
            //        var cntNew = 0;
            //        var cntExisting = 0;
            //        var cntMatch = 0;
            //        while (csv.Read())
            //        {
            //            var code = csv.GetField<string>(0);
            //            var name = csv.GetField<string>(1);
            //            //var pin = code.PadLeft(4, '0');

            //            var existingStaff = context.Staffs.FirstOrDefault(x => x.Code == code);
            //            if (existingStaff != null)
            //            {
            //                cntExisting++;
            //                if (existingStaff.FullName == name)
            //                    cntMatch++;
            //                else
            //                {
            //                    Console.WriteLine($"Driver name does not macth with exisiting {code}");
            //                }
            //            }
            //            else
            //            {
            //                cntNew++;
            //                context.Staffs.Add(new Staff
            //                {
            //                    EffectiveDate = DateTime.UtcNow.ToTradingTimeFromUtc().DayStart(),
            //                    FullName = name,
            //                    Code = code,
            //                    Roles = "Driver",
            //                    Pin = "9999"
            //                });
            //            }

            //        }

            //        Console.WriteLine($"Total new {cntNew}, Exists: {cntExisting}, Matched: {cntMatch}");
            //    }
            //}

            context.SaveChanges();

           
        }
    }
}
