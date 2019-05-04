using System.Data.Entity;
using ThemeLayout.Models;

namespace ThemeLayout
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext() : base("ProjectDbContext") { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Transport> Transports { get; set; }
        public DbSet<Search> Searches { get; set; }
        public DbSet<Restuarant> Restuarants { get; set; }

    }
}