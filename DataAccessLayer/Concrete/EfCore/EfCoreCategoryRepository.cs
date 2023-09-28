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
        public void DeleteProductFromCategories(int productId, int categoryId)
        {
            using (var context = new ShopAppContext())
            {
                var cmd = "Delete from ProductCategory where ProductId=@p0 and CategoryId=@p1";
                context.Database.ExecuteSqlRaw(cmd,productId,categoryId);
            }
        }

        public Category GetByIdWithProducts(int categoryId)
        {
            using (var context = new ShopAppContext())
            {
                return context.Categories
                                .Where(c => c.Id == categoryId)
                                .Include(c => c.Products)   // burada productCategory tablosuna geçtik
                                .ThenInclude(pc => pc.Product) // product category tablosundan Category tablosuna geçtik
                                .FirstOrDefault();
            }
        }
    }
}
