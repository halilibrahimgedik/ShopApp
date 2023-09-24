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
    public class EfCoreProductRepository : EfCoreGenericRepository<Product>, IProductRepository
    {
        public List<Product> GetPopulerProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetproductDetails(int id)
        {
            using (var context = new ShopAppContext())
            {
                return context.Products
                          .Where(p => p.Id == id)
                          .Include(p => p.Categories)   // ProductCategori tablosuna geçtik
                          .ThenInclude(c => c.Category) // Product Category'den Category.tablosuna geçiş yapıyoruz
                          .FirstOrDefault();
            };
        }
    }
}
