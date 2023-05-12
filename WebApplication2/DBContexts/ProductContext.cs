using Microsoft.EntityFrameworkCore;
using WebApplication2.Models.Orders;
using WebApplication2.Models.Products;

namespace WebApplication2.DBContexts
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options)
        {
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var products = new List<Product>()
            {
                  new Product()
          {
              Id = 1,
              Name = "Electronics",
              Description = "Electronic Items",
              Price = 0
          },
          new Product()
          {
              Id = 2,
              Name = "Clothes",
              Description = "Dresses",
              Price = 2.50M
          },
          new Product()
          {
              Id = 3,
              Name = "Grocery",
              Description = "Grocery Items",
              Price = 5.50M
          }
            };

            var order = new Order() { Customer = "Homer", Address = "Springfield", Status = "Active", Id = 1};
            var od = new List<OrderDetail>()
            {
                new OrderDetail() {Quantity = 2, Id = 1, ProductId = 1, OrderId = 1},
                new OrderDetail() {Quantity = 4, Id = 2, ProductId = 2, OrderId = 1},
            };

                        modelBuilder.Entity<Product>().HasData(products);
                        modelBuilder.Entity<Order>().HasData(order);
                        modelBuilder.Entity<OrderDetail>().HasData(od); 

        }
    }
}
