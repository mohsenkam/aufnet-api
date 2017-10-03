using Aufnet.Backend.Data.Models.Entities.Admin;
using Aufnet.Backend.Data.Models.Entities.Customer;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Data.Models.Entities.Reminder;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Data.Models.Entities.Transaction;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Aufnet.Backend.Data.Context
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<IdentityRole>().Property(r => r.Id).HasMaxLength(256);
            //builder.Entity<IdentityRole>().Property(r => r.Name).IsRequired();
            //builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(256).IsRequired());
            ////builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).IsUnicode(false));
            //builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(256).IsRequired());
            ////builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).IsUnicode(false));
            //builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(256).IsRequired());
            //builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).IsUnicode(false));

        }

        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<BookmarkedMerchantEvent> BookmarkedMerchantEvents { get; set; }

        public DbSet<MerchantContract> MerchantContracts { get; set; }
        public DbSet<MerchantProfile> MerchantProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<MerchantProduct> Products { get; set; }
        public DbSet<MerchantEvent> MerchantEvents { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Category> Categories { get; set; }

    }

    //public class ApplicationDbContextFactory : IDbContextFactory<ApplicationDbContext>
    //{
    //    public ApplicationDbContext Create( DbContextFactoryOptions options )
    //    {
    //        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
    //        return new ApplicationDbContext(optionsBuilder.Options);
    //    }
    //}
}
