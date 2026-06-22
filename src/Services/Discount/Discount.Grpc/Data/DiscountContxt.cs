using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
    public class DiscountContxt:DbContext
    {
        public DiscountContxt(DbContextOptions<DiscountContxt> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Copun>().HasData(
                new Copun { Id = 1, ProductName = "IPhone X", Description = "IPhone Discount", Amount = 150 },
                new Copun { Id = 2, ProductName = "Samsung 10", Description = "Samsung Discount", Amount = 100 }
                );
        }
        public DbSet<Copun> Copuns { get; set; }
    }
}
