using Microsoft.EntityFrameworkCore;
using System;

namespace Shop.API.Entities
{
    public class ProductRepositoryContext : DbContext
    {
        public ProductRepositoryContext(DbContextOptions<ProductRepositoryContext> options) 
            : base(options)
        {
            //Database.Migrate();
        }

        //Connection between the Business Layer and the Database
        public DbSet<Product> products { get; set; }

        //Dummy data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Seed the database
            modelBuilder.Entity<Product>().HasData(
                new Product()
                {
                    id = Guid.NewGuid(),
                    description = "An apartment located in Cali, Colombia",
                    type = TYPE.Apartment.ToString(),
                    value = 45000000,
                    purchasedDate = DateTime.Now,
                    status = true
                }, new Product()
                {
                    id = Guid.NewGuid(),
                    description = "Chevrolet Corvette from Europe",
                    type = TYPE.Vehicle.ToString(),
                    value = 1200000000,
                    purchasedDate = DateTime.Now,
                    status = true
                }
                ) ;
        }
    }
}
