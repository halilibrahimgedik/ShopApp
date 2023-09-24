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

        public Product GetProductDetails(int id)
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

        public List<Product> ListProductsByCategory(string name)
        {
            using (var context =new ShopAppContext())
            {
                var products = context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    products = products.Include(p => p.Categories).ThenInclude(pc => pc.Category)
                                        .Where(p => p.Categories.Any(c => c.Category.Name.ToLower() == name.ToLower()));
                }

                return products.ToList();
            }
        }
    }
}
