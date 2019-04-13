using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    public class MyDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            const string connectionString = "Server=.;Initial Catalog=MyDatabase;persist security info=True;Integrated Security=True";

            optionsBuilder.UseSqlServer(connectionString)
                . EnableSensitiveDataLogging();
        }
    }
}