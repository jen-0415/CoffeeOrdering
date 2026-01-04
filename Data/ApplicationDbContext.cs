using CoffeeOrdering.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeOrdering.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<CoffeeOrder> CoffeeOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure decimal precision for Price
            modelBuilder.Entity<CoffeeOrder>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);
        }
    }
}