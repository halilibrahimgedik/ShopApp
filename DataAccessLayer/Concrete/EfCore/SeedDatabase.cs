using System.Linq;
using DataAccessLayer.Concrete.EfCore;
using EntityLayer;
using Microsoft.EntityFrameworkCore;

namespace shopapp.data.Concrete.EfCore
{
    //public static class SeedDatabase
    //{
    //    public static void Seed()
    //    {
    //        var context = new ShopAppContext();

    //        // ! Bekleyen Migration Yoksa
    //        if (context.Database.GetPendingMigrations().Count() == 0)
    //        {
    //            if (context.Categories.Count() == 0)
    //            {
    //                context.Categories.AddRange(SeedCategories);
    //            }

    //            if (context.Products.Count() == 0)
    //            {
    //                context.Products.AddRange(SeedProducts);
    //                context.AddRange(SeedProductCategories); // talo adý yok,iliþkili tablo, veri olduðu için anlýyor program
    //            }
    //        }
    //        context.SaveChanges();
    //    }

    //    private static List<Category> SeedCategories = new(){
    //        new Category { Name = "Telefon", Description = "Telefon kategorisi" },
    //        new Category { Name = "Bilgisayar", Description = "Bilgisayar kategorisi" },
    //        new Category { Name = "Elektronik", Description = "Elektronik aletler" }
    //    };

    //    private static List<Product> SeedProducts = new()
    //    {
    //        // Telefonlar
    //        new Product {Id=1, Name = "Iphone 15", Price = 55000, Description = "çok iyi telefon", ImageUrl = "iphone-green.jpg", IsApproved = true },
    //        new Product {Id=2,Name = "Iphone 15", Price = 53000, Description = "çok iyi telefon", ImageUrl = "iphone-blue.jpg", IsApproved = true },
    //        new Product {Name = "Iphone 15", Price = 52000, Description = "iyi telefon", ImageUrl = "iphone-black.jpg", IsApproved = true },
    //        new Product {Name = "Iphone 15", Price = 51000, Description = "çok iyi telefon", ImageUrl = "iphone-pink.jpg", IsApproved = true },
    //        new Product {Name = "Iphone 15", Price = 52000, Description = "çok iyi telefon", ImageUrl = "iphone-yellow.jpg", IsApproved = true },
    //            // Bilgisayarlar
    //        new Product {Name = "Bilgisayar", Price = 55000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-office.jpg", IsApproved = true },
    //        new Product {Name = "Bilgisayar", Price = 53000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-gaming.jpg", IsApproved = true },
    //        new Product {Name = "Bilgisayar", Price = 52000, Description = "iyi Bilgisayar", ImageUrl = "asus-office.jpg", IsApproved = true },
    //        new Product {Name = "Bilgisayar", Price = 51000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-office.jpg", IsApproved = true },
    //        new Product {Name = "Bilgisayar", Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-gaming.jpg", IsApproved = true },
    //        new Product {Name = "Bilgisayar", Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "asus-gaming.jpg", IsApproved = true}
    //    };

    //    private static List<ProductCategory> SeedProductCategories = new()
    //    {
    //        //Telefonlar
    //        new(){ ProductId = 0, CategoryId =0},
    //        new(){ ProductId = 0, CategoryId =2},
    //        new(){ ProductId = 1, CategoryId =0},
    //        new(){ ProductId = 1, CategoryId =2},
    //        new(){ ProductId = 2, CategoryId =0},
    //        new(){ ProductId = 2, CategoryId =2},
    //        new(){ ProductId = 3, CategoryId =0},
    //        new(){ ProductId = 3, CategoryId =2},
    //        new(){ ProductId = 4, CategoryId =0},
    //        new(){ ProductId = 4, CategoryId =2},
    //        new(){ ProductId = 5, CategoryId =0},
    //        new(){ ProductId = 5, CategoryId =2},

    //        // Bilgisayarlar
    //        new(){ ProductId = 6, CategoryId =1},
    //        new(){ ProductId = 6, CategoryId =2},
    //        new(){ ProductId = 7, CategoryId =1},
    //        new(){ ProductId = 7, CategoryId =2},
    //        new(){ ProductId = 8, CategoryId =1},
    //        new(){ ProductId = 8, CategoryId =2},
    //        new(){ ProductId = 9, CategoryId =1},
    //        new(){ ProductId = 9, CategoryId =2},
    //        new(){ ProductId = 10, CategoryId =1},
    //        new(){ ProductId = 10, CategoryId =2},
    //        new(){ ProductId = 11, CategoryId =1},
    //        new(){ ProductId = 11, CategoryId =2},
    //    };

    //}
}