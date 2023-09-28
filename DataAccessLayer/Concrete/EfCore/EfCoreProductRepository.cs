﻿using DataAccessLayer.Abstract;
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
        public int GetCountByCategory(string category)
        {
            using var context = new ShopAppContext();
            var products = context.Products.Where(p=>p.IsApproved==true).AsQueryable();
            if (!string.IsNullOrEmpty(category))
            {
                products = products.Include(p => p.Categories).ThenInclude(pc => pc.Category)
                .Where(p => p.Categories.Any(c => c.Category.Url == category));
            }
            return products.Count();
        }

        public int GetCountBySearchResult(string stringForSearch)
        {
            using (var context = new ShopAppContext())
            {
                var products = context.Products.Where(p => p.IsApproved == true).AsQueryable();

                if (!string.IsNullOrEmpty(stringForSearch))
                {
                    products = products.Where(i => i.Name.ToLower().Contains(stringForSearch) || i.Description.Contains(stringForSearch));
                }

                return products.Count();
            }
        }

        public List<Product> GetHomePageProducts()
        {
            using var context = new ShopAppContext();
            return context.Products.Where(p => p.IsApproved == true && p.IsHome == true).ToList();
        }

        public List<Product> GetPopulerProducts()
        {
            throw new NotImplementedException();
        }

        public Product GetProductByIdWithCategories(int id)
        {
            using (var context = new ShopAppContext())
            {
                return context.Products.Where(p => p.Id == id).Include(p => p.Categories).ThenInclude(pc => pc.Category).FirstOrDefault();
            };
        }

        public Product GetProductDetails(string url)
        {
            using (var context = new ShopAppContext())
            {
                return context.Products
                          .Where(p => p.Url == url)
                          .Include(p => p.Categories)   // ProductCategori tablosuna geçtik
                          .ThenInclude(pc => pc.Category) // Product Category'den Category.tablosuna geçiş yapıyoruz
                          .FirstOrDefault();
            };
        }

        public List<Product> GetSearchResult(string stringForSearch,int page,int PageSize)
        {
            using (var context = new ShopAppContext())
            {
                var p = context.Products.Where(p => p.IsApproved == true).AsQueryable();

                if (!string.IsNullOrEmpty(stringForSearch))
                {
                    p = p.Where(i=>i.Name.ToLower().Contains(stringForSearch) || i.Description.ToLower().Contains(stringForSearch));
                }

                return p.Skip((page-1)*PageSize).Take(PageSize).ToList(); 
            };
        }

        public List<Product> ListProductsByCategory(string name, int page, int pageSize)
        {
            using (var context =new ShopAppContext())
            {
                var products = context.Products.Where(p => p.IsApproved == true).AsQueryable();

                if (!string.IsNullOrEmpty(name))
                {
                    products = products.Include(p => p.Categories).ThenInclude(pc => pc.Category)
                                        .Where(p => p.Categories.Any(c => c.Category.Url == name));
                }

                return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }

        public void Update(Product p, int[] categoryIds)
        {
            using (var context = new ShopAppContext())
            {
                //var product = context.Products
                //    .Include(p => p.Categories)
                //    .FirstOrDefault(i=>i.Id == p.Id);

                var product = context.Products.Where(pr=>pr.Id ==p.Id)
                                              .Include(p => p.Categories)
                                              .FirstOrDefault();

                if (product != null)
                {
                    product.Name= p.Name;
                    product.Description= p.Description;
                    product.Price= p.Price;
                    product.ImageUrl= p.ImageUrl;
                    product.Url= p.Url;

                    product.Categories = categoryIds.Select(catId => new ProductCategory()
                    {
                        ProductId = p.Id,
                        CategoryId = catId,
                    }).ToList();

                    context.SaveChanges();
                }
            };
        }
    }
}
