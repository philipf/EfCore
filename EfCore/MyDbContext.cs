using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    public class MyDbContext : DbContext
    {
        public DbSet<ProductLevel> ProductLevels { get; set; }
        public DbSet<ProductNode> ProductNodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Server=.;Initial Catalog=MyDatabase;persist security info=True;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString)
                . EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<BaseSection>();

            modelBuilder.Entity<TermsSection>();
            modelBuilder.Entity<RentalSection>();
            modelBuilder.Entity<BonusPaymentSection>();
            modelBuilder.Entity<FtcSection>();
            modelBuilder.Entity<BasicInfoSection>();
            modelBuilder.Entity<JapanBasicInfoSection>();
        }
    }
}