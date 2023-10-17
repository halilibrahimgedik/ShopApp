using EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);

            builder.HasData(
            //Telefon
             new Product { Id = 1, Name = "Iphone 15", Url = "iphone15", IsHome = true, Price = 55000, Description = "çok iyi telefon", ImageUrl = "iphone-green.jpg", IsApproved = true },
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
             new Product { Id = 16, Name = "Arçelik çamaşır Makinesi", Url = "", IsHome = true, Price = 15750, Description = "Düşük enerjili yüksek performanslı", ImageUrl = "arcelik-camasir.jpg", IsApproved = true });

            //builder.Property(p => p.AddedDate).HasDefaultValueSql("date('now')"); //? Sqlite için
            builder.Property(p => p.AddedDate).HasDefaultValueSql("FORMAT(GETDATE(), 'yyyy-MM-dd')");
        }
    }
}
