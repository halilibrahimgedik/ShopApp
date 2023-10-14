using EntityLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

            builder .HasOne(pc => pc.Product)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(pc => pc.ProductId);

            builder.HasOne(pc => pc.Category)
                   .WithMany(c => c.Products)
                   .HasForeignKey(pc => pc.CategoryId);

            builder.HasData(
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
