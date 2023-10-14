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
        private readonly ShopAppContext shopAppContext;

        public EfCoreProductRepository(ShopAppContext context) : base(context)
        {
            shopAppContext = context;
        }


        public void Add(Product p, int[] categoryIds)
        {
            shopAppContext.Products.Add(p);

            p.Categories = categoryIds.Select(catId => new ProductCategory()
            {
                CategoryId = catId,
                ProductId = p.Id
            }).ToList();

            shopAppContext.SaveChanges();
        }

        public int GetCountByCategory(string category)
        {
            var products = shopAppContext.Products.Where(p => p.IsApproved == true).AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(p => p.Categories).ThenInclude(pc => pc.Category)
                .Where(p => p.Categories.Any(c => c.Category.Url == category));
            }
            return products.Count();
        }

        public int GetCountBySearchResult(string stringForSearch)
        {
            var products = shopAppContext.Products.Where(p => p.IsApproved == true).AsQueryable();

            if (!string.IsNullOrEmpty(stringForSearch))
            {
                products = products.Where(i => i.Name.ToLower().Contains(stringForSearch) || i.Description.Contains(stringForSearch));
            }

            return products.Count();
        }

        public List<Product> GetHomePageProducts()
        {
            return shopAppContext.Products.Where(p => p.IsApproved == true && p.IsHome == true).ToList();
        }

        public Product GetProductByIdWithCategories(int id)
        {
            return shopAppContext.Products.Where(p => p.Id == id).Include(p => p.Categories).ThenInclude(pc => pc.Category).FirstOrDefault();
        }

        public Product GetProductDetails(string url)
        {
            return shopAppContext.Products
                      .Where(p => p.Url == url)
                      .Include(p => p.Categories)   // ProductCategori tablosuna geçtik
                      .ThenInclude(pc => pc.Category) // Product Category'den Category.tablosuna geçiş yapıyoruz
                      .FirstOrDefault();
        }

        public List<Product> GetSearchResult(string stringForSearch, int page, int PageSize)
        {
            var p = shopAppContext.Products.Where(p => p.IsApproved == true).AsQueryable();

            if (!string.IsNullOrEmpty(stringForSearch))
            {
                p = p.Where(i => i.Name.ToLower().Contains(stringForSearch) || i.Description.ToLower().Contains(stringForSearch));
            }

            return p.Skip((page - 1) * PageSize).Take(PageSize).ToList();
        }

        public List<Product> ListProductsByCategory(string name, int page, int pageSize)
        {
            var products = shopAppContext.Products.Where(p => p.IsApproved == true).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Include(p => p.Categories).ThenInclude(pc => pc.Category)
                                    .Where(p => p.Categories.Any(c => c.Category.Url == name));
            }

            return products.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public void Update(Product p, int[] categoryIds)
        {
            var product = shopAppContext.Products.Where(pr => pr.Id == p.Id)
                                          .Include(p => p.Categories)
                                          .FirstOrDefault();

            if (product != null)
            {
                product.Name = p.Name;
                product.Description = p.Description;
                product.Price = p.Price;
                product.ImageUrl = p.ImageUrl;
                product.Url = p.Url;
                product.IsApproved = p.IsApproved;
                product.IsHome = p.IsHome;

                product.Categories = categoryIds.Select(catId => new ProductCategory()
                {
                    ProductId = p.Id,
                    CategoryId = catId,
                }).ToList();

                shopAppContext.SaveChanges();
            }
        }
    }
}
