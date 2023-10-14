using DataAccessLayer.Abstract;
using EntityLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.EfCore
{
    public class EfCoreCategoryRepository : EfCoreGenericRepository<Category>, ICategoryRepository
    {
        private readonly ShopAppContext shopAppContext;

        public EfCoreCategoryRepository(ShopAppContext context) : base(context)
        {
            shopAppContext = context;
        }

        public void DeleteProductFromCategories(int productId, int categoryId)
        {
            var cmd = "Delete from ProductCategory where ProductId=@p0 and CategoryId=@p1";
            shopAppContext.Database.ExecuteSqlRaw(cmd, productId, categoryId);
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            return shopAppContext.Categories
                            .Where(c => c.Id == categoryId)
                            .Include(c => c.Products)   // burada productCategory tablosuna geçtik
                            .ThenInclude(pc => pc.Product) // product category tablosundan Category tablosuna geçtik
                            .FirstOrDefault();
        }
    }
}
