using Aufnet.Backend.Data.Models.Entities.Customer;
using Aufnet.Backend.Data.Models.Entities.Merchant;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Aufnet.Backend.Data.Models.Entities.Transaction;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Data.Context
{
    public class ApplicationDbContext:IdentityDbContext//<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }

        public DbSet<CustomerProfile> CustomerProfiles { get; set; }
        public DbSet<BookmarkedMerchantEvent> BookmarkedMerchantEvents { get; set; }

        public DbSet<MerchantProfile> MerchantProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<MerchantProduct> Products { get; set; }
       
         public DbSet<Region> Regions { get; set; }
          public DbSet<Point> Points { get; set; }

    }
}
