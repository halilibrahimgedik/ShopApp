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
                    new Category { Id = 1, Name = "Telefon", Description = "Telefon kategorisi", Url = "telefon" },
                    new Category { Id = 2, Name = "Bilgisayar", Description = "Bilgisayar kategorisi", Url = "bilgisayar" },
                    new Category { Id = 3, Name = "Elektronik", Description = "Elektronik aletler", Url = "elektronik" },
                    new Category { Id = 4, Name = "Kitap", Description = "Kitap Okumak", Url = "kitap" },
                    new Category { Id = 5, Name = "Beyaz Eşya", Description = "Elektronik ev aletleri", Url = "beyaz-esya" }
                );

            modelBuilder.Entity<Product>()
                 .HasData(
                     //Telefon
                     new Product { Id = 1, Name = "Iphone 15", Url="iphone15", IsHome=true, Price = 55000, Description = "çok iyi telefon", ImageUrl = "iphone-green.jpg", IsApproved = true },
                    new Product { Id = 2, Name = "Iphone 15", Url = "iphone15", IsHome = true, Price = 53000, Description = "çok iyi telefon", ImageUrl = "iphone-blue.jpg", IsApproved = true },
                    new Product { Id = 3, Name = "Iphone 15", Url = "iphone15", IsHome = true, Price = 52000, Description = "iyi telefon", ImageUrl = "iphone-black.jpg", IsApproved = true },
                    new Product { Id = 4, Name = "Iphone 15", Url = "iphone15", IsHome = true, Price = 51000, Description = "çok iyi telefon", ImageUrl = "iphone-pink.jpg", IsApproved = true },
                    new Product { Id = 5, Name = "Iphone 15", Url = "iphone15", IsHome = true, Price = 52000, Description = "çok iyi telefon", ImageUrl = "iphone-yellow.jpg", IsApproved = true },

                     //Bilgisayar
                     new Product { Id = 6, Name = "Lenovo Creative", Url = "", IsHome = true, Price = 55000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-office.jpg", IsApproved = true },
                     new Product { Id = 7, Name = "Lenovo Ideapad", Url = "", IsHome = true, Price = 53000, Description = "çok iyi Bilgisayar", ImageUrl = "lenovo-gaming.jpg", IsApproved = true },
                     new Product { Id = 8, Name = "Asus Vivobook", Url = "", IsHome = false, Price = 52000, Description = "iyi Bilgisayar", ImageUrl = "asus-office.jpg", IsApproved = true },
                     new Product { Id = 9, Name = "Hp latitude", Url = "", IsHome = true, Price = 51000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-office.jpg", IsApproved = true },
                     new Product { Id = 10, Name = "Hp Victus", Url = "", IsHome = true, Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "hp-gaming.jpg", IsApproved = true },
                     new Product { Id = 11, Name = "Asus Tuf", Url = "", IsHome = true, Price = 52000, Description = "çok iyi Bilgisayar", ImageUrl = "asus-gaming.jpg", IsApproved = true },

                      new Product { Id = 12, Name = "Atomik Alışkanlıklar", Url = "", IsHome = true, Price = 320, Description = "Küçük değişimler ile neler yapabileceğinizi tahmin bile edemezsiniz", ImageUrl = "atomik.jpg", IsApproved = true },

                      new Product { Id = 13, Name = "Outliers (Çizginin Dışındakiler)", Url = "", IsHome = false, Price = 340, Description = "Başarılı insanlar hakkında anlatılan bir hikaye vardır; onların zeki ve hırslı oldukları söylenir. Outliers’te Malcolm Gladwell başarının gerçek hikayesinin bundan çok farklı olduğunu ve bazı insanların neden başarılı olduğunu anlamak için, bunların çevrelerine daha dikkatli bakmamız gerektiğini iddia ediyor.", ImageUrl = "outliers.jpg", IsApproved = true },

                      new Product { Id = 14, Name = "Bilgisayar Ağları ve İnternet", Url = "", IsHome = false, Price = 360, Description = "Bu kitap, ağ iletişiminin temel kavramlarından, İnternette sayısı her geçen gün artan güncel uygulamalara; farklı haberleşme teknolojilerinden ağ programlama tekniklerine kadar farklı yelpazedeki konuları gerek genel konseptleri gerekse teknik detayları ile açıklamaktadır.", ImageUrl = "pc-network.jpg", IsApproved = true },

                      new Product { Id = 15, Name = "Bosch Buzdolabı", Url = "", IsHome = true, Price = 20250, Description = "Alttan Donduruculu Buzdolabı", ImageUrl = "bosch-buzdolap.jpg", IsApproved = true },
                      new Product { Id = 16, Name = "Arçelik çamaşır Makinesi", Url = "", IsHome = true, Price = 15750, Description = "Düşük enerjili yüksek performanslı", ImageUrl = "arcelik-camasir.jpg", IsApproved = true }

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
                    new ProductCategory() { CategoryId = 2, ProductId = 11 },

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
                    new ProductCategory() { CategoryId = 3, ProductId = 11 },

                    new ProductCategory() { CategoryId = 4, ProductId = 12 },
                    new ProductCategory() { CategoryId = 4, ProductId = 13 },
                    new ProductCategory() { CategoryId = 4, ProductId = 14 },

                    new ProductCategory() { CategoryId = 5, ProductId = 15 },
                    new ProductCategory() { CategoryId = 3, ProductId = 15 },
                    new ProductCategory() { CategoryId = 3, ProductId = 16 },
                    new ProductCategory() { CategoryId = 5, ProductId = 16 }
                );
        }
    }
}
