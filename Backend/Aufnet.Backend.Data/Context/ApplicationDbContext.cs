using Aufnet.Backend.Data.Models.Entities.Customers;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Aufnet.Backend.Data.Models.Entities.Merchants;
using Aufnet.Backend.Data.Models.Entities.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Aufnet.Backend.Data.Context
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options ) : base(options)
        {
        }

        public ApplicationDbContext() // For the sake of unit tests
        {
            
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ProductTransaction>()
                .HasKey(pt => new
                {
                    pt.ProductId, pt.TransactionId
                });
            builder.Entity<ProductTransaction>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTransactionst)
                .HasForeignKey(pt => pt.ProductId);

            builder.Entity<ProductTransaction>()
                .HasOne(pt => pt.Transaction)
                .WithMany(t => t.ProductTransactionst)
                .HasForeignKey(pt => pt.TransactionId);

            

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
        //public DbSet<BookmarkedMerchantEvent> BookmarkedMerchantEvents { get; set; }

        public DbSet<Contract> Contracts { get; set; }
        public DbSet<MerchantProfile> MerchantProfiles { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTransaction> ProductTransactions { get; set; }
        public DbSet<ItemBasedOffer> ItemBasedOffers { get; set; }
        //public DbSet<Region> Regions { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        //public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Category> Categories { get; set; }

        public void Seed( IApplicationBuilder app )
        {
            // Get an instance of the DbContext from the DI container
            using (var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>())
            {
                context.Categories.Add(new Category()
                {
                    Id = 1,
                    ImageUrl = "",
                    DisplayName = "root",
                });
                context.Categories.Add(new Category()
                {
                    Id = 2,
                    ImageUrl = "Cat1Url",
                    DisplayName = "Cat1",
                    ParentId = 1
                });
                context.Categories.Add(new Category()
                {
                    Id = 3,
                    ImageUrl = "Cat2Url",
                    DisplayName = "Cat2",
                    ParentId = 1
                });
                context.Categories.Add(new Category()
                {
                    Id = 21,
                    ImageUrl = "Cat11Url",
                    DisplayName = "Cat11",
                    ParentId = 2
                });
                context.Categories.Add(new Category()
                {
                    Id = 31,
                    ImageUrl = "Cat21Url",
                    DisplayName = "Cat21",
                    ParentId = 3
                });
                context.Categories.Add(new Category()
                {
                    Id = 32,
                    ImageUrl = "Cat22Url",
                    DisplayName = "Cat22",
                    ParentId = 3
                });
                context.SaveChanges();
            }
        }

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
