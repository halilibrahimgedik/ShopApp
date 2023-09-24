using System.Linq;
using DataAccessLayer.Concrete.EfCore;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace shopapp.data.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopAppContext();

            // ! Bekleyen Migration Yoksa
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(SeedCategories);
                }

                if (context.Products.Count() == 0)
                {
                    context.Products.AddRange(SeedProducts);
                }
            }
            context.SaveChanges();
        }

        private static List<Category> SeedCategories = new(){
            new Category { Name = "Telefon", Description = "Telefon kategorisi" },
            new Category { Name = "Bilgisayar", Description = "Bilgisayar kategorisi" },
            new Category { Name = "Elektronik", Description = "Elektronik aletler" }
        };

        private static List<Product> SeedProducts = new()
        {
            // Telefonlar
            new Product {Name = "Iphone 15", Price = 55000, Description = "çok iyi telefon", ImageUrl = "iphone-green.jpg", IsApproved = true },
            new Product {Name = "Iphone 15", Price = 53000, Description = "çok iyi telefon", ImageUrl = "iphone-blue.jpg", IsApproved = true },
            new Product {Name = "Iphone 15", Price = 52000, Description = "iyi telefon", ImageUrl = "iphone-black.jpg", IsApproved = true },
             new Product {Name = "Iphone 15", Price = 51000, Description = "çok iyi telefon", ImageUrl = "iphone-pink.jpg", IsApproved = true },
            new Product {Name = "Iphone 15", Price = 52000, Description = "çok iyi telefon", ImageUrl = "iphone-yellow.jpg", IsApproved = true },

                // Bilgisayarlar
            new Product {Name = "Bilgisayar", Price = 55000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-office.jpg", IsApproved = true },
            new Product {Name = "Bilgisayar", Price = 53000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-gaming.jpg", IsApproved = true },
            new Product {Name = "Bilgisayar", Price = 52000, Description = "iyi Bilgisayar", ImageUrl = "asus-office.jpg", IsApproved = true },
            new Product {Name = "Bilgisayar", Price = 51000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-office.jpg", IsApproved = true },
            new Product {Name = "Bilgisayar", Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-gaming.jpg", IsApproved = true },
            new Product {Name = "Bilgisayar", Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "asus-gaming.jpg", IsApproved = true }
        };
    }
}