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
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                    new Category { Id = 1, Name = "Telefon", Description = "Telefon kategorisi", Url = "telefon" },
                    new Category { Id = 2, Name = "Bilgisayar", Description = "Bilgisayar kategorisi", Url = "bilgisayar" },
                    new Category { Id = 3, Name = "Elektronik", Description = "Elektronik aletler", Url = "elektronik" },
                    new Category { Id = 4, Name = "Kitap", Description = "Kitap Okumak", Url = "kitap" },
                    new Category { Id = 5, Name = "Beyaz Eşya", Description = "Elektronik ev aletleri", Url = "beyaz-esya" }
                );
        }
    }
}
