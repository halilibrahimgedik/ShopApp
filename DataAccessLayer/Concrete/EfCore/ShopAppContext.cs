using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EfCore
{
    public class ShopAppContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Shop.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategory>()
                        .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                        .HasOne(pc => pc.Product)
                        .WithMany(p => p.Categories)
                        .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                        .HasOne(pc => pc.Category)
                        .WithMany(c => c.Products)
                        .HasForeignKey(pc => pc.CategoryId);

            // //? Data Seeding Bu işlemleri SeedDatabase.cs static sınıfında hallediyoruz artık

            modelBuilder.Entity<Category>()
               .HasData(
                    new Category { Id = 1, Name = "Telefon", Description = "Telefon kategorisi" },
                    new Category { Id = 2, Name = "Bilgisayar", Description = "Bilgisayar kategorisi" },
                    new Category { Id = 3, Name = "Elektronik", Description = "Elektronik aletler" }
                );

            modelBuilder.Entity<Product>()
                 .HasData(
                     //Telefon
                     new Product { Id = 1, Name = "Iphone 15", Price = 55000, Description = "çok iyi telefon", ImageUrl = "iphone-green.jpg", IsApproved = true },
                    new Product { Id = 2, Name = "Iphone 15", Price = 53000, Description = "çok iyi telefon", ImageUrl = "iphone-blue.jpg", IsApproved = true },
                    new Product { Id = 3, Name = "Iphone 15", Price = 52000, Description = "iyi telefon", ImageUrl = "iphone-black.jpg", IsApproved = true },
                    new Product { Id = 4, Name = "Iphone 15", Price = 51000, Description = "çok iyi telefon", ImageUrl = "iphone-pink.jpg", IsApproved = true },
                    new Product { Id = 5, Name = "Iphone 15", Price = 52000, Description = "çok iyi telefon", ImageUrl = "iphone-yellow.jpg", IsApproved = true },

                     //Bilgisayar
                     new Product { Id = 6, Name = "Bilgisayar", Price = 55000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-office.jpg", IsApproved = true },
                     new Product { Id = 7, Name = "Bilgisayar", Price = 53000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-gaming.jpg", IsApproved = true },
                     new Product { Id = 8, Name = "Bilgisayar", Price = 52000, Description = "iyi Bilgisayar", ImageUrl = "asus-office.jpg", IsApproved = true },
                     new Product { Id = 9, Name = "Bilgisayar", Price = 51000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-office.jpg", IsApproved = true },
                     new Product { Id = 10, Name = "Bilgisayar", Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-gaming.jpg", IsApproved = true },
                     new Product { Id = 11, Name = "Bilgisayar", Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "asus-gaming.jpg", IsApproved = true }
                 );

            modelBuilder.Entity<ProductCategory>()
              .HasData(
                    new ProductCategory() { CategoryId = 1, ProductId = 1 },
                    new ProductCategory() { CategoryId = 1, ProductId = 2 },
                    new ProductCategory() { CategoryId = 1, ProductId = 3 },
                    new ProductCategory() { CategoryId = 1, ProductId = 4 },
                    new ProductCategory() { CategoryId = 1, ProductId = 5 },
                    new ProductCategory() { CategoryId = 2, ProductId = 6 },
                    new ProductCategory() { CategoryId = 2, ProductId = 7 },
                    new ProductCategory() { CategoryId = 2, ProductId = 8 },
                    new ProductCategory() { CategoryId = 2, ProductId = 9 },
                    new ProductCategory() { CategoryId = 2, ProductId = 10 },

                    new ProductCategory() { CategoryId = 3, ProductId = 1 },
                    new ProductCategory() { CategoryId = 3, ProductId = 2 },
                    new ProductCategory() { CategoryId = 3, ProductId = 3 },
                    new ProductCategory() { CategoryId = 3, ProductId = 4 },
                    new ProductCategory() { CategoryId = 3, ProductId = 5 },
                    new ProductCategory() { CategoryId = 3, ProductId = 6 },
                    new ProductCategory() { CategoryId = 3, ProductId = 7 },
                    new ProductCategory() { CategoryId = 3, ProductId = 8 },
                    new ProductCategory() { CategoryId = 3, ProductId = 9 },
                    new ProductCategory() { CategoryId = 3, ProductId = 10 },
                    new ProductCategory() { CategoryId = 3, ProductId = 11 }
                );
        }
    }
}
