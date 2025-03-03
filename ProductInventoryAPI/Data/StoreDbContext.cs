using Microsoft.EntityFrameworkCore;
using ProductInventoryAPI.Models;

namespace ProductInventoryAPI.Data
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

      
            modelBuilder.Entity<Category>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);


            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Books" }
            );

          
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Price = 2500, Stock = 10, IsAvailable = true, CategoryId = 1 },
                new Product { Id = 2, Name = "Smartphone", Price = 1500, Stock = 15, IsAvailable = true, CategoryId = 1 },
                new Product { Id = 3, Name = "T-Shirt", Price = 50, Stock = 100, IsAvailable = true, CategoryId = 2 },
                new Product { Id = 4, Name = "Jeans", Price = 120, Stock = 50, IsAvailable = true, CategoryId = 2 },
                new Product { Id = 5, Name = "C# Programming Book", Price = 99, Stock = 30, IsAvailable = true, CategoryId = 3 }
            );

        }
    }
}
