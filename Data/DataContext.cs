using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace hrd_backend.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Define DbSet properties for each entity/table
      //  public DbSet<Company> Companies { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Configure entity properties and relationships if needed
        //    modelBuilder.Entity<Company>().HasKey(c => c.Id);
        //}
    }
}
