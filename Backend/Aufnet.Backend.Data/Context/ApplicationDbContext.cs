using Aufnet.Backend.Data.Models.Entities;
using Aufnet.Backend.Data.Models.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Aufnet.Backend.Data.Context
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Staff>()
                .HasIndex(x => x.Code);
            modelBuilder.Entity<DscRecord>()
                .HasIndex(x => x.SeqNo);
            modelBuilder.Entity<DriverDeposit>()
                .HasIndex(x => x.ReceiptNo);
        }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<DscRecord> DscRecords { get; set; }

        public DbSet<DriverDeposit> DriverDeposits { get; set; }

        public DbSet<Terminal> Terminals { get; set; }

        public DbSet<SystemConfig> Configs { get; set; }

        public DbSet<AuditLog> AuditLogs { get; set; }
    }
}
